﻿using System.Drawing;
using System.Linq;

namespace SpriteStudio.Layouts
{
    internal class HorizontalBuilder : LayoutBuilderBase
    {
        public HorizontalBuilder(ImageCssMap map, LayoutProperties props)
            : base(map, props)
        {
        }

        public override void Generate()
        {
            var padding = Properties.Padding;
            var margin  = Properties.Margin;

            // Calculating result image dimension.
            var width = Images.Values.Sum(_ => _.Width + padding);

            width = width - padding + 2 * margin;

            var height = Properties.ImagesHeight + 2 * margin;

            // Creating an empty result image.
            ResultImage  = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(ResultImage))
            {
                var actualXCoordinate = margin;
                var yCoordinate       = margin;

                // Drawing images into the result image, writing CSS lines and increasing X coordinate.
                foreach (var i in Images.Keys)
                {
                    var currentImage = Images[i];

                    var rectangle = new Rectangle(
                        actualXCoordinate,
                        yCoordinate,
                        currentImage.Width,
                        currentImage.Height
                        );

                    graphics.DrawImage(currentImage, rectangle);
                    CssBuilder.AppendLine(CssLine(CssClassNames[i], rectangle));
                    actualXCoordinate += currentImage.Width + padding;
                }
            }
        }
    }
}
