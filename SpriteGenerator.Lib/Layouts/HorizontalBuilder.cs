using System.Drawing;
using System.Linq;

namespace SpriteGenerator.Layouts
{
    public class HorizontalBuilder : LayoutBuilderBase
    {
        public HorizontalBuilder(ImageCssMap map, LayoutProperties props)
            : base(map, props)
        {
        }

        public override void Generate()
        {
            var distanceBetweenImages = Properties.DistanceBetweenImages;
            var marginWidth = Properties.MarginWidth;

            // Calculating result image dimension.
            var width = Images.Values.Sum(_ => _.Width + distanceBetweenImages);

            width = width - distanceBetweenImages + 2 * marginWidth;

            var height = Images[0].Height + 2 * marginWidth;

            // Creating an empty result image.
            ResultImage = new Bitmap(width, height);
            var graphics = Graphics.FromImage(ResultImage);

            // Initial coordinates.
            var actualXCoordinate = marginWidth;
            var yCoordinate = marginWidth;

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
                actualXCoordinate += currentImage.Width + distanceBetweenImages;
            }
        }
    }
}
