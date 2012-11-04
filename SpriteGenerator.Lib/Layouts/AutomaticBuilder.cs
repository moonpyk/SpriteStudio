using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SpriteGenerator.Utility;

namespace SpriteGenerator.Layouts
{
    public class AutomaticBuilder : LayoutBuilderBase
    {
        private readonly IEnumerable<Module> _modules = new List<Module>();

        public AutomaticBuilder(ImageCssMap map, LayoutProperties props)
            : base(map, props)
        {
            _modules = Images.Keys
                .Select(i => new Module(i, Images[i], Properties.DistanceBetweenImages))
                .ToList();
        }

        public override void Generate()
        {
            var sortedByArea = _modules.OrderByDescending(m => m.Width * m.Height);

            var moduleList = sortedByArea.ToList();
            var placement = Algorithm.Greedy(moduleList);

            // Creating an empty result image.
            ResultImage = new Bitmap(
                placement.Width - Properties.DistanceBetweenImages + 2 * Properties.MarginWidth,
                placement.Height - Properties.DistanceBetweenImages + 2 * Properties.MarginWidth
            );

            var graphics = Graphics.FromImage(ResultImage);

            // Drawing images into the result image in the original order and writing CSS lines.
            foreach (var m in placement.Modules)
            {
                m.Draw(graphics, Properties.MarginWidth);

                var rectangle = new Rectangle(
                    m.X + Properties.MarginWidth,
                    m.Y + Properties.MarginWidth,
                    m.Width - Properties.DistanceBetweenImages,
                    m.Height - Properties.DistanceBetweenImages
                );

                CssBuilder.AppendLine(CssLine(CssClassNames[m.Name], rectangle));
            }
        }
    }
}
