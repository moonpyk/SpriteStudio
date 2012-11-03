using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using SpriteGenerator.Utility;

namespace SpriteGenerator
{
    public class Sprite
    {
        private Dictionary<int, Image> _images;
        private Dictionary<int, string> _cssClassNames;
        private readonly LayoutProperties _layoutProp;

        public Sprite(LayoutProperties layoutProp)
        {
            _images = new Dictionary<int, Image>();
            _cssClassNames = new Dictionary<int, string>();
            _layoutProp = layoutProp;
        }

        public void Create()
        {
            GetData(out _images, out _cssClassNames);

            var cssFile = File.CreateText(_layoutProp.OutputCssFilePath);

            Image resultSprite = null;

            cssFile.WriteLine(".sprite {{ background-image: url('{0}'); background-color: transparent; background-repeat: no-repeat; }}", RelativeSpriteImagePath(_layoutProp.OutputSpriteFilePath, _layoutProp.OutputCssFilePath));

            switch (_layoutProp.Layout)
            {
                case "Automatic":
                    resultSprite = GenerateAutomaticLayout(cssFile);
                    break;

                case "Horizontal":
                    resultSprite = GenerateHorizontalLayout(cssFile);
                    break;

                case "Vertical":
                    resultSprite = GenerateVerticalLayout(cssFile);
                    break;

                case "Rectangular":
                    resultSprite = GenerateRectangularLayout(cssFile);
                    break;
            }

            cssFile.Close();
            var outputSpriteFile = new FileStream(_layoutProp.OutputSpriteFilePath, FileMode.Create);

            // FIXME : ReThrow
            Debug.Assert(resultSprite != null, "resultSprite != null");

            resultSprite.Save(outputSpriteFile, ImageFormat.Png);
            outputSpriteFile.Close();
        }

        /// <summary>
        /// Creates dictionary of images from the given paths and dictionary of CSS classnames from the image filenames.
        /// </summary> 
        /// <param name="images">Dictionary of images to be inserted into the output sprite.</param>
        /// <param name="cssClassNames">Dictionary of CSS classnames.</param>
        private void GetData(out Dictionary<int, Image> images, out Dictionary<int, string> cssClassNames)
        {
            images = new Dictionary<int, Image>();
            cssClassNames = new Dictionary<int, string>();

            for (var i = 0; i < _layoutProp.InputFilePaths.Length; i++)
            {
                var img = Image.FromFile(_layoutProp.InputFilePaths[i]);
                images.Add(i, img);
                var splittedFilePath = _layoutProp.InputFilePaths[i].Split('\\');
                cssClassNames.Add(i, splittedFilePath[splittedFilePath.Length - 1].Split('.')[0]);
            }
        }

        private IEnumerable<Module> CreateModules()
        {
            return _images.Keys
                .Select(i => new Module(i, _images[i], _layoutProp.DistanceBetweenImages))
                .ToList();
        }

        // CSS line
        private static string CssLine(string cssClassName, Rectangle rectangle)
        {
            var line = string.Format(".{0} {{ width: {1}px; height: {2}px; background-position: {3}px {4}px; }}",
                cssClassName,
                rectangle.Width.ToString(CultureInfo.InvariantCulture),
                rectangle.Height.ToString(CultureInfo.InvariantCulture),
                (-1 * rectangle.X).ToString(CultureInfo.InvariantCulture),
                (-1 * rectangle.Y).ToString(CultureInfo.InvariantCulture)
            );
            return line;
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
        private Image GenerateAutomaticLayout(StreamWriter cssFile)
        {
            var sortedByArea = from m in CreateModules()
                               orderby m.Width * m.Height descending
                               select m;

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

                cssFile.WriteLine(CssLine(_cssClassNames[m.Name], rectangle));
            }

            return resultSprite;
        }

        // Horizontal layout
        private Image GenerateHorizontalLayout(StreamWriter cssFile)
        {
            // Calculating result image dimension.
            var width = _images.Values.Sum(_ => _.Width + _layoutProp.DistanceBetweenImages);

            width = width - _layoutProp.DistanceBetweenImages + 2 * _layoutProp.MarginWidth;

            var height = _images[0].Height + 2 * _layoutProp.MarginWidth;

            //Creating an empty result image.
            var resultSprite = new Bitmap(width, height);
            var graphics = Graphics.FromImage(resultSprite);

            //Initial coordinates.
            var actualXCoordinate = _layoutProp.MarginWidth;
            var yCoordinate = _layoutProp.MarginWidth;

            //Drawing images into the result image, writing CSS lines and increasing X coordinate.
            foreach (var i in _images.Keys)
            {
                var rectangle = new Rectangle(
                    actualXCoordinate,
                    yCoordinate,
                    _images[i].Width,
                    _images[i].Height
                );

                graphics.DrawImage(_images[i], rectangle);
                cssFile.WriteLine(CssLine(_cssClassNames[i], rectangle));
                actualXCoordinate += _images[i].Width + _layoutProp.DistanceBetweenImages;
            }

            return resultSprite;
        }

        // Vertical layout
        private Image GenerateVerticalLayout(TextWriter cssFile)
        {
            // Calculating result image dimension.
            var height = _images.Values.Sum(image => image.Height + _layoutProp.DistanceBetweenImages);

            height = height - _layoutProp.DistanceBetweenImages + 2 * _layoutProp.MarginWidth;
            var width = _images[0].Width + 2 * _layoutProp.MarginWidth;

            // Creating an empty result image.
            var resultSprite = new Bitmap(width, height);
            var graphics = Graphics.FromImage(resultSprite);

            // Initial coordinates.
            var actualYCoordinate = _layoutProp.MarginWidth;
            var xCoordinate = _layoutProp.MarginWidth;

            // Drawing images into the result image, writing CSS lines and increasing Y coordinate.
            foreach (var i in _images.Keys)
            {
                var rectangle = new Rectangle(xCoordinate, actualYCoordinate, _images[i].Width, _images[i].Height);
                graphics.DrawImage(_images[i], rectangle);
                cssFile.WriteLine(CssLine(_cssClassNames[i], rectangle));
                actualYCoordinate += _images[i].Height + _layoutProp.DistanceBetweenImages;
            }

            return resultSprite;
        }

        private Image GenerateRectangularLayout(TextWriter cssFile)
        {
            // Calculating result image dimension.
            var imageWidth = _images[0].Width;
            var imageHeight = _images[0].Height;

            var width = _layoutProp.ImagesInRow * (imageWidth + _layoutProp.DistanceBetweenImages) -
                _layoutProp.DistanceBetweenImages + 2 * _layoutProp.MarginWidth;

            var height = _layoutProp.ImagesInColumn * (imageHeight + _layoutProp.DistanceBetweenImages) -
                _layoutProp.DistanceBetweenImages + 2 * _layoutProp.MarginWidth;

            // Creating an empty result image.
            var resultSprite = new Bitmap(width, height);
            var graphics = Graphics.FromImage(resultSprite);

            // Initial coordinates.
            var actualYCoordinate = _layoutProp.MarginWidth;
            var actualXCoordinate = _layoutProp.MarginWidth;

            // Drawing images into the result image, writing CSS lines and increasing coordinates.
            for (var i = 0; i < _layoutProp.ImagesInColumn; i++)
            {
                for (var j = 0; (i * _layoutProp.ImagesInRow) + j < _images.Count && j < _layoutProp.ImagesInRow; j++)
                {
                    var rectangle = new Rectangle(actualXCoordinate, actualYCoordinate, imageWidth, imageHeight);
                    graphics.DrawImage(_images[i * _layoutProp.ImagesInRow + j], rectangle);
                    cssFile.WriteLine(CssLine(_cssClassNames[i * _layoutProp.ImagesInRow + j], rectangle));

                    actualXCoordinate += imageWidth + _layoutProp.DistanceBetweenImages;
                }

                actualYCoordinate += imageHeight + _layoutProp.DistanceBetweenImages;
                actualXCoordinate = _layoutProp.MarginWidth;
            }

            return resultSprite;
        }
    }
}
