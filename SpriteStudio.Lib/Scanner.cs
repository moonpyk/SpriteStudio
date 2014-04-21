using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SpriteStudio
{
    public class Scanner
    {
        public static readonly string[] Filter =
        {
            ".png", ".jpg", ".jpeg", ".gif",
        };

        /// <summary>
        /// </summary>
        /// <exception cref="Exception">The given path does not exists</exception>
        /// <exception cref="System.IO.IOException">
        /// - Path est un nom de fichier.
        /// – Une erreur réseau s'est produite. 
        /// </exception>
        /// <exception cref="System.UnauthorizedAccessException">L'appelant n'a pas l'autorisation requise. </exception>
        /// <exception cref="System.OutOfMemoryException">
        /// - Le format d'image du fichier n'est pas valide.
        /// – GDI+ ne prend pas en charge le format de pixel du fichier.
        /// </exception>
        public static ScannerResult ScanDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                throw new IOException("The given path does not exists");
            }

            var images = (
                from filter in Filter
                from file in Directory.GetFiles(directory).AsParallel()
                where file.EndsWith(filter)
                select file
            ).ToList();

            if (images.Count == 0)
            {
                throw new IOException("No image file was found during directory scan");
            }

            return ScanImages(images);
        }

        /// <summary>
        /// </summary>
        /// <exception cref="System.OutOfMemoryException">
        /// - Le format d'image du fichier n'est pas valide.
        /// – GDI+ ne prend pas en charge le format de pixel du fichier.
        /// </exception>
        public static ScannerResult ScanImages(IList<string> images)
        {
            int width, height;

            using (var firstImage = Image.FromFile(images.First()))
            {
                width  = firstImage.Width;
                height = firstImage.Height;
            }

            bool canVertical = true,
                 canHorizontal = true;

            Parallel.ForEach(images.Skip(1), (path, s) =>
            {
                using (var i = Image.FromFile(path))
                {
                    // Horizontal layout is enabled only when all image heights are the same.                    
                    canHorizontal &= i.Height == height;

                    // Vertical layout is enabled only when all image widths are the same.
                    canVertical &= i.Width == width;

                    if (!canHorizontal || !canVertical)
                    {
                        s.Break(); // We can stop immediately
                    }
                }
            });

            var availableLayouts = new List<SpriteLayoutEnum> {
                SpriteLayoutEnum.Automatic
            };

            if (canHorizontal)
            {
                availableLayouts.Add(SpriteLayoutEnum.Horizontal);
            }

            if (canVertical)
            {
                availableLayouts.Add(SpriteLayoutEnum.Vertical);
            }

            if (canVertical && canHorizontal)
            {
                availableLayouts.Add(SpriteLayoutEnum.Rectangular);
            }

            return new ScannerResult
            {
                AvailableLayouts = availableLayouts,
                ImagesWidth      = canVertical ? width : ScannerResult.NoCommonImageSize,
                ImagesHeight     = canHorizontal ? height : ScannerResult.NoCommonImageSize,
                FileList         = images,
            };
        }
    }
}
