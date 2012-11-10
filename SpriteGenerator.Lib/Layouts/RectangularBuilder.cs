using System.Drawing;

namespace SpriteGenerator.Layouts
{
    public class RectangularBuilder : LayoutBuilderBase
    {
        public RectangularBuilder(ImageCssMap map, LayoutProperties props)
            : base(map, props)
        { }

        public override void Generate()
        {
            var padding = Properties.Padding;
            var margin = Properties.Margin;

            // Calculating result image dimension.
            var imageWidth = Properties.ImagesWidth;
            var imageHeight = Properties.ImagesHeight;

            var imagesInColumn = Properties.ImagesInColumn;
            var imagesInRow = Properties.ImagesInRow;

            var width = imagesInRow * (imageWidth + padding) - padding + 2 * margin;
            var height = imagesInColumn * (imageHeight + padding) - padding + 2 * margin;

            // Creating an empty result image.
            ResultImage = new Bitmap(width, height);
            var graphics = Graphics.FromImage(ResultImage);

            // Initial coordinates.
            var actualYCoordinate = margin;
            var actualXCoordinate = margin;

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

                    actualXCoordinate += imageWidth + padding;
                }

                actualYCoordinate += imageHeight + padding;
                actualXCoordinate = margin;
            }
        }
    }
}
