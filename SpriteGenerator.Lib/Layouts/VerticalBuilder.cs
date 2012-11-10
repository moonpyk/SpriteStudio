using System.Drawing;
using System.Linq;

namespace SpriteGenerator.Layouts
{
    public class VerticalBuilder : LayoutBuilderBase
    {
        public VerticalBuilder(ImageCssMap map, LayoutProperties props)
            : base(map, props)
        {
        }

        public override void Generate()
        {
            var padding = Properties.Padding;
            var margin = Properties.Margin;

            // Calculating result image dimension.
            var height = Images.Values.Sum(_ => _.Height + padding);

            height = height - padding + 2 * margin;
            var width = Properties.ImagesWidth + 2 * margin;

            // Creating an empty result image.
            ResultImage = new Bitmap(width, height);
            var graphics = Graphics.FromImage(ResultImage);

            // Initial coordinates.
            var actualYCoordinate = margin;
            var xCoordinate = margin;

            // Drawing images into the result image, writing CSS lines and increasing Y coordinate.
            foreach (var i in Images.Keys)
            {
                var currentImage = Images[i];

                var rectangle = new Rectangle(
                    xCoordinate,
                    actualYCoordinate,
                    currentImage.Width,
                    currentImage.Height
                );

                graphics.DrawImage(currentImage, rectangle);
                CssBuilder.AppendLine(CssLine(CssClassNames[i], rectangle));
                actualYCoordinate += currentImage.Height + padding;
            }
        }
    }
}
