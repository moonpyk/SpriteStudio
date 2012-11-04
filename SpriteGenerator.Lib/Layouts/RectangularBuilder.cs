﻿using System.Drawing;

namespace SpriteGenerator.Layouts
{
    public class RectangularBuilder : LayoutBuilderBase
    {
        public RectangularBuilder(ImageCssMap map, LayoutProperties props)
            : base(map, props)
        { }

        public override void Generate()
        {
            var distanceBetweenImages = Properties.DistanceBetweenImages;
            var marginWidth = Properties.MarginWidth;

            // Calculating result image dimension.
            var imageWidth = Images[0].Width;
            var imageHeight = Images[0].Height;

            var imagesInColumn = Properties.ImagesInColumn;
            var imagesInRow = Properties.ImagesInRow;

            var width = imagesInRow * (imageWidth + distanceBetweenImages) -
                distanceBetweenImages + 2 * marginWidth;

            var height = imagesInColumn * (imageHeight + distanceBetweenImages) -
                distanceBetweenImages + 2 * marginWidth;

            // Creating an empty result image.
            ResultImage = new Bitmap(width, height);
            var graphics = Graphics.FromImage(ResultImage);

            // Initial coordinates.
            var actualYCoordinate = marginWidth;
            var actualXCoordinate = marginWidth;

            // Drawing images into the result image, writing CSS lines and increasing coordinates.
            for (var i = 0; i < imagesInColumn; i++)
            {
                for (var j = 0; (i * imagesInRow) + j < Images.Count && j < imagesInRow; j++)
                {
                    var rectangle = new Rectangle(
                        actualXCoordinate,
                        actualYCoordinate,
                        imageWidth,
                        imageHeight
                    );

                    graphics.DrawImage(Images[i * imagesInRow + j], rectangle);
                    CssBuilder.AppendLine(CssLine(CssClassNames[i * imagesInRow + j], rectangle));

                    actualXCoordinate += imageWidth + distanceBetweenImages;
                }

                actualYCoordinate += imageHeight + distanceBetweenImages;
                actualXCoordinate = marginWidth;
            }
        }
    }
}