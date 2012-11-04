using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using SpriteGenerator.Utility;

namespace SpriteGenerator
{
    public class Sprite : IDisposable
    {
        private const string CssSpriteDeclarationFormat = ".{1} {{ background-image: url('{0}'); background-color: transparent; background-repeat: no-repeat; }}";
        private const string CssLineDeclarationFormat = ".{0} {{ width: {1}; height: {2}; background-position: {3} {4}; }}";

        private readonly LayoutProperties _layoutProp;
        private Dictionary<int, string> _cssClassNames;

        private bool _disposed;
        private Dictionary<int, Image> _images;

        public Sprite(LayoutProperties layoutProp)
        {
            _layoutProp = layoutProp;
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
                foreach (var i in _images)
                {
                    i.Value.Dispose();
                }
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

            Image resultSprite;

            var cssContent = new StringBuilder();

            switch (_layoutProp.Layout)
            {
                case SpriteLayout.Automatic:
                    resultSprite = GenerateAutomaticLayout(cssContent);
                    break;

                case SpriteLayout.Horizontal:
                    resultSprite = GenerateHorizontalLayout(cssContent);
                    break;

                case SpriteLayout.Vertical:
                    resultSprite = GenerateVerticalLayout(cssContent);
                    break;

                case SpriteLayout.Rectangular:
                    resultSprite = GenerateRectangularLayout(cssContent);
                    break;

                default:
                    throw new InvalidOperationException("Invalid SpriteLayout mode selected.");
            }

            cssContent.Insert(0, string.Format(CssSpriteDeclarationFormat + Environment.NewLine,
                RelativeSpriteImagePath(
                    _layoutProp.OutputSpriteFilePath,
                    _layoutProp.OutputCssFilePath
                ),
                "sprite"
                )
            );

            using (var cssFile = File.CreateText(_layoutProp.OutputCssFilePath))
            {
                cssFile.Write(cssContent);
                cssFile.Close();
            }

            using (var outputSpriteFile = new FileStream(_layoutProp.OutputSpriteFilePath, FileMode.Create))
            {
                resultSprite.Save(outputSpriteFile, ImageFormat.Png);
                outputSpriteFile.Close();
            }

            resultSprite.Dispose();
        }

        /// <summary>
        /// Creates dictionary of images from the given paths and dictionary of CSS classnames from the image filenames.
        /// </summary> 
        private ImageCssMap PopulateData()
        {
            var images = new Dictionary<int, Image>();
            var cssClassNames = new Dictionary<int, string>();

            for (var i = 0; i < _layoutProp.InputFilePaths.Count; i++)
            {
                images.Add(i, Image.FromFile(_layoutProp.InputFilePaths[i]));

                var splittedFilePath = _layoutProp.InputFilePaths[i].Split('\\');
                cssClassNames.Add(i, splittedFilePath[splittedFilePath.Length - 1].Split('.')[0]);
            }

            return new ImageCssMap(images, cssClassNames);
        }

        private IEnumerable<Module> CreateModules()
        {
            return _images.Keys
                .Select(i => new Module(i, _images[i], _layoutProp.DistanceBetweenImages))
                .ToList();
        }



        // Relative sprite image file path
        private static string RelativeSpriteImagePath(string outputSpriteFilePath, string outputCssFilePath)
        {
            var splittedOutputCssFilePath = outputCssFilePath.Split('\\');
            var splittedOutputSpriteFilePath = outputSpriteFilePath.Split('\\');

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

        // Automatic layout
        private Image GenerateAutomaticLayout(StringBuilder cssFile)
        {
            var sortedByArea = CreateModules().OrderByDescending(m => m.Width * m.Height);

            var moduleList = sortedByArea.ToList();
            var placement = Algorithm.Greedy(moduleList);

            // Creating an empty result image.
            Image resultSprite = new Bitmap(
                placement.Width - _layoutProp.DistanceBetweenImages + 2 * _layoutProp.MarginWidth,
                placement.Height - _layoutProp.DistanceBetweenImages + 2 * _layoutProp.MarginWidth
            );

            var graphics = Graphics.FromImage(resultSprite);

            // Drawing images into the result image in the original order and writing CSS lines.
            foreach (var m in placement.Modules)
            {
                m.Draw(graphics, _layoutProp.MarginWidth);

                var rectangle = new Rectangle(
                    m.X + _layoutProp.MarginWidth,
                    m.Y + _layoutProp.MarginWidth,
                    m.Width - _layoutProp.DistanceBetweenImages,
                    m.Height - _layoutProp.DistanceBetweenImages
                );

                cssFile.AppendLine(CssLine(_cssClassNames[m.Name], rectangle));
            }

            return resultSprite;
        }

        // Horizontal layout
        private Image GenerateHorizontalLayout(StringBuilder cssFile)
        {
            var distanceBetweenImages = _layoutProp.DistanceBetweenImages;
            var marginWidth = _layoutProp.MarginWidth;

            // Calculating result image dimension.
            var width = _images.Values.Sum(_ => _.Width + distanceBetweenImages);

            width = width - distanceBetweenImages + 2 * marginWidth;

            var height = _images[0].Height + 2 * marginWidth;

            // Creating an empty result image.
            var resultSprite = new Bitmap(width, height);
            var graphics = Graphics.FromImage(resultSprite);

            // Initial coordinates.
            var actualXCoordinate = marginWidth;
            var yCoordinate = marginWidth;

            // Drawing images into the result image, writing CSS lines and increasing X coordinate.
            foreach (var i in _images.Keys)
            {
                var rectangle = new Rectangle(
                    actualXCoordinate,
                    yCoordinate,
                    _images[i].Width,
                    _images[i].Height
                );

                graphics.DrawImage(_images[i], rectangle);
                cssFile.AppendLine(CssLine(_cssClassNames[i], rectangle));
                actualXCoordinate += _images[i].Width + distanceBetweenImages;
            }

            return resultSprite;
        }

        // Vertical layout
        private Image GenerateVerticalLayout(StringBuilder cssFile)
        {
            var distanceBetweenImages = _layoutProp.DistanceBetweenImages;
            var marginWidth = _layoutProp.MarginWidth;

            // Calculating result image dimension.

            var height = _images.Values.Sum(image => image.Height + distanceBetweenImages);

            height = height - distanceBetweenImages + 2 * marginWidth;
            var width = _images[0].Width + 2 * marginWidth;

            // Creating an empty result image.
            var resultSprite = new Bitmap(width, height);
            var graphics = Graphics.FromImage(resultSprite);

            // Initial coordinates.
            var actualYCoordinate = marginWidth;
            var xCoordinate = marginWidth;

            // Drawing images into the result image, writing CSS lines and increasing Y coordinate.
            foreach (var i in _images.Keys)
            {
                var rectangle = new Rectangle(
                    xCoordinate,
                    actualYCoordinate,
                    _images[i].Width,
                    _images[i].Height
                );

                graphics.DrawImage(_images[i], rectangle);
                cssFile.AppendLine(CssLine(_cssClassNames[i], rectangle));
                actualYCoordinate += _images[i].Height + distanceBetweenImages;
            }

            return resultSprite;
        }

        private Image GenerateRectangularLayout(StringBuilder cssFile)
        {
            var distanceBetweenImages = _layoutProp.DistanceBetweenImages;
            var marginWidth = _layoutProp.MarginWidth;

            // Calculating result image dimension.
            var imageWidth = _images[0].Width;
            var imageHeight = _images[0].Height;
            var imagesInColumn = _layoutProp.ImagesInColumn;
            var imagesInRow = _layoutProp.ImagesInRow;

            var width = imagesInRow * (imageWidth + distanceBetweenImages) -
                distanceBetweenImages + 2 * marginWidth;

            var height = imagesInColumn * (imageHeight + distanceBetweenImages) -
                distanceBetweenImages + 2 * marginWidth;

            // Creating an empty result image.
            var resultSprite = new Bitmap(width, height);
            var graphics = Graphics.FromImage(resultSprite);

            // Initial coordinates.
            var actualYCoordinate = marginWidth;
            var actualXCoordinate = marginWidth;

            // Drawing images into the result image, writing CSS lines and increasing coordinates.
            for (var i = 0; i < imagesInColumn; i++)
            {
                for (var j = 0; (i * imagesInRow) + j < _images.Count && j < imagesInRow; j++)
                {
                    var rectangle = new Rectangle(
                        actualXCoordinate,
                        actualYCoordinate,
                        imageWidth,
                        imageHeight
                    );

                    graphics.DrawImage(_images[i * imagesInRow + j], rectangle);
                    cssFile.AppendLine(CssLine(_cssClassNames[i * imagesInRow + j], rectangle));

                    actualXCoordinate += imageWidth + distanceBetweenImages;
                }

                actualYCoordinate += imageHeight + distanceBetweenImages;
                actualXCoordinate = marginWidth;
            }

            return resultSprite;
        }

        // CSS line
        private static string CssLine(string cssClassName, Rectangle rect)
        {
            var line = string.Format(CssLineDeclarationFormat,
                cssClassName,
                CssPixels(rect.Width),
                CssPixels(rect.Height),
                CssPixels(-1 * rect.X),
                CssPixels(-1 * rect.Y)
            );

            return line;
        }

        private static string CssPixels(int val)
        {
            if (val == 0)
            {
                return "0";
            }

            return val.ToString(CultureInfo.InvariantCulture) + "px";
        }
    }
}
