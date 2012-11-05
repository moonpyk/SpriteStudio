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
                .Select(i => new Module(i, Images[i], Properties.Padding))
                .ToList();
        }

        public override void Generate()
        {
            var sortedByArea = _modules.OrderByDescending(m => m.Width * m.Height);

            var moduleList = sortedByArea.ToList();
            var placement = Algorithm.Greedy(moduleList);

            // Creating an empty result image.
            ResultImage = new Bitmap(
                placement.Width - Properties.Padding + 2 * Properties.Margin,
                placement.Height - Properties.Padding + 2 * Properties.Margin
            );

            var graphics = Graphics.FromImage(ResultImage);

            // Drawing images into the result image in the original order and writing CSS lines.
            foreach (var m in placement.Modules)
            {
                m.Draw(graphics, Properties.Margin);

                var rectangle = new Rectangle(
                    m.X + Properties.Margin,
                    m.Y + Properties.Margin,
                    m.Width - Properties.Padding,
                    m.Height - Properties.Padding
                );

                CssBuilder.AppendLine(CssLine(CssClassNames[m.Name], rectangle));
            }
        }
    }
}
