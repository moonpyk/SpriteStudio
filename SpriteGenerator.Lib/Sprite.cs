using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SpriteGenerator.Layouts;

namespace SpriteGenerator
{
    public class Sprite : IDisposable
    {
        private bool _disposed;

        private readonly LayoutProperties _properties;
        private Dictionary<int, string> _cssClassNames;
        private Dictionary<int, Image> _images;

        public Sprite(LayoutProperties properties)
        {
            _properties = properties;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            if (_images != null)
            {
                Parallel.ForEach(_images, i => i.Value.Dispose());
                _images.Clear();
                _images = null;
            }


            if (_cssClassNames != null)
            {
                _cssClassNames.Clear();
                _cssClassNames = null;
            }

            _disposed = true;
        }

        #endregion

        public void Create()
        {
            var tuple = PopulateData();

            _images = tuple.Images;
            _cssClassNames = tuple.CssClassesNames;

            LayoutBuilderBase b = null;

            switch (_properties.Layout)
            {
                case SpriteLayout.Automatic:
                    b = new AutomaticBuilder(tuple, _properties);
                    break;

                case SpriteLayout.Horizontal:
                    b = new HorizontalBuilder(tuple, _properties);
                    break;

                case SpriteLayout.Vertical:
                    b = new VerticalBuilder(tuple, _properties);
                    break;

                case SpriteLayout.Rectangular:
                    b = new RectangularBuilder(tuple, _properties);
                    break;
            }

            if (b == null)
            {
                throw new InvalidOperationException("Invalid SpriteLayout mode selected.");
            }

            b.Generate();

            var cssContent = new StringBuilder();

            cssContent.AppendFormat(LayoutBuilderBase.CssSpriteDeclarationFormat + Environment.NewLine,
                RelativeSpriteImagePath(
                    _properties.OutputSpriteFilePath,
                    _properties.OutputCssFilePath
                ),
                "sprite"
            );
            cssContent.Append(b.CssCode);

            using (var cssFile = File.CreateText(_properties.OutputCssFilePath))
            {
                cssFile.Write(cssContent);
                cssFile.Close();
            }

            using (var outputSpriteFile = new FileStream(_properties.OutputSpriteFilePath, FileMode.Create))
            {
                b.ResultImage.Save(outputSpriteFile, ImageFormat.Png);
                outputSpriteFile.Close();
            }

            b.Dispose();
        }

        /// <summary>
        /// Creates dictionary of images from the given paths and dictionary of CSS classnames from the image filenames.
        /// </summary> 
        private ImageCssMap PopulateData()
        {
            var images = new Dictionary<int, Image>();
            var cssClassNames = new Dictionary<int, string>();

            for (var i = 0; i < _properties.InputFilePaths.Count; i++)
            {
                images.Add(i, Image.FromFile(_properties.InputFilePaths[i]));

                var splittedFilePath = _properties.InputFilePaths[i].Split(
                    Path.DirectorySeparatorChar
                );

                cssClassNames.Add(i, splittedFilePath[splittedFilePath.Length - 1].Split('.')[0]);
            }

            return new ImageCssMap(images, cssClassNames);
        }

        // Relative sprite image file path
        private static string RelativeSpriteImagePath(string outputSpriteFilePath, string outputCssFilePath)
        {
            var sep = Path.DirectorySeparatorChar;

            var splittedOutputCssFilePath = outputCssFilePath.Split(sep);
            var splittedOutputSpriteFilePath = outputSpriteFilePath.Split(sep);

            var breakAt = 0;
            for (var i = 0; i < splittedOutputCssFilePath.Length; i++)
            {
                if (i >= splittedOutputSpriteFilePath.Length || splittedOutputCssFilePath[i] == splittedOutputSpriteFilePath[i])
                {
                    continue;
                }

                breakAt = i;
                break;
            }

            var relativePath = "";

            for (var i = 0; i < splittedOutputCssFilePath.Length - breakAt - 1; i++)
            {
                relativePath += "../";
            }

            relativePath += String.Join("/", splittedOutputSpriteFilePath, breakAt, splittedOutputSpriteFilePath.Length - breakAt);

            return relativePath;
        }
    }
}
