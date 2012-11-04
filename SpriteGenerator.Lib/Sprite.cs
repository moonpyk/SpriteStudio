using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteGenerator.Layouts;

namespace SpriteGenerator
{
    public class Sprite : IDisposable
    {
        private bool _disposed;

        private readonly LayoutProperties _properties;
        private ImageCssMap _map;

        public Sprite(LayoutProperties properties)
        {
            _properties = properties;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_disposed || _map == null)
            {
                return;
            }

            if (_map.Images != null)
            {
                Parallel.ForEach(_map.Images, i => i.Value.Dispose());
                _map.Images.Clear();
            }


            if (_map.CssClassesNames != null)
            {
                _map.CssClassesNames.Clear();
            }

            _disposed = true;
        }

        #endregion

        public void Create()
        {
            _map = PopulateData();

            LayoutBuilderBase b = null;

            switch (_properties.Layout)
            {
                case SpriteLayout.Automatic:
                    b = new AutomaticBuilder(_map, _properties);
                    break;

                case SpriteLayout.Horizontal:
                    b = new HorizontalBuilder(_map, _properties);
                    break;

                case SpriteLayout.Vertical:
                    b = new VerticalBuilder(_map, _properties);
                    break;

                case SpriteLayout.Rectangular:
                    b = new RectangularBuilder(_map, _properties);
                    break;
            }

            if (b == null)
            {
                throw new InvalidOperationException("Invalid SpriteLayout mode selected.");
            }

            b.Generate();

            var cssContent = new StringBuilder();

            cssContent.Append(b.GetSpriteDefinition("sprite"));
            cssContent.Append(b.CssCode);

            using (var outCss = File.CreateText(_properties.OutputCssFilePath))
            {
                outCss.Write(cssContent);
                outCss.Close();
            }

            using (var outImage = new FileStream(_properties.OutputSpriteFilePath, FileMode.Create))
            {
                b.ResultImage.Save(outImage, ImageFormat.Png);
                outImage.Close();
            }

            b.Dispose();
        }

        /// <summary>
        /// Creates dictionary of images from the given paths and dictionary of CSS classnames from the image filenames.
        /// </summary> 
        private ImageCssMap PopulateData()
        {
            var images = new ConcurrentDictionary<int, Image>();
            var cssClassNames = new ConcurrentDictionary<int, string>();

            var inputFilePaths = _properties.InputFilePaths;

            Parallel.For(0, inputFilePaths.Count, i =>
            {
                var imgInstance = Image.FromFile(inputFilePaths[i]);
                if (!images.TryAdd(i, imgInstance))
                {
                    throw new Exception("Unable to insert one Image instance");
                }

                var splittedFilePath = inputFilePaths[i].Split(
                    Path.DirectorySeparatorChar
                );

                if (!cssClassNames.TryAdd(i, splittedFilePath.Last().Split('.')[0]))
                {
                    throw new Exception("Unable to insert one cssClassName instance");
                }
            });

            return new ImageCssMap(
                images,
                cssClassNames
            );
        }
    }
}
