using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        private readonly LayoutProperties _properties;
        private bool _disposed;
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
                try
                {
                    Parallel.ForEach(_map.Images, i => i.Value.Dispose());
                }
                // ReSharper disable EmptyGeneralCatchClause : What can we do anyway ?
                catch { }
                // ReSharper restore EmptyGeneralCatchClause
                _map.Images.Clear();
            }


            if (_map.CssClassesNames != null)
            {
                _map.CssClassesNames.Clear();
            }

            _disposed = true;
        }

        #endregion

        /// <summary>
        /// Generates the CSS Sprite image and stylesheet using the <see cref="LayoutProperties"/>
        /// given during instanciation.
        /// </summary>
        /// <exception cref="Exception">
        /// If invalid SpriteLayout mode selected (None usually).
        /// If underlaying imaging library (GDI+ on Windows) ecounters an exception
        /// </exception>
        public void Generate()
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
                throw new Exception("Invalid SpriteLayout mode selected.");
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
                try
                {
                    b.ResultImage.Save(outImage, ImageFormat.Png);
                }
                catch (System.Runtime.InteropServices.ExternalException ex)
                {
                    throw new Exception("Underlaying imaging library encountered an error", ex);
                }
                finally
                {
                    outImage.Close();
                    b.Dispose();
                }
            }
        }

        /// <summary>
        /// Creates dictionary of images from the given paths and dictionary of CSS classnames from the image filenames.
        /// </summary> 
        /// <exception cref="System.Exception">Error during dictionaries populating</exception>
        private ImageCssMap PopulateData()
        {
            var images = new ConcurrentDictionary<int, Image>();
            var cssClassNames = new ConcurrentDictionary<int, string>();

            var inputFilePaths = _properties.InputFilePaths;

            Parallel.For(0, inputFilePaths.Count, i => PopulateDictionaries(
                inputFilePaths, i, images, cssClassNames
            ));

            return new ImageCssMap(
                images,
                cssClassNames
            );
        }

        /// <summary>
        /// Fills images and class names dictionnaries with needed data. Depending of the IDictionary type used under the hood
        /// an exception may be thrown.
        /// </summary>
        /// <exception cref="Exception">Error during dictionnarie populating</exception>
        private static void PopulateDictionaries(IList<string> inputFilePaths, int i, IDictionary<int, Image> images, IDictionary<int, string> cssClassNames)
        {
            try
            {
                var imgInstance = Image.FromFile(inputFilePaths[i]);

                images[i] = imgInstance;

                var splittedFilePath = inputFilePaths[i].Split(
                    Path.DirectorySeparatorChar
                );

                cssClassNames[i] = splittedFilePath.Last().Split('.')[0];
            }
            catch (Exception ex)
            {
                throw new Exception("Error during dictionaries populating", ex);
            }
        }
    }
}
