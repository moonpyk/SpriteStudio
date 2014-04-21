using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SpriteStudio.Packer;

namespace SpriteStudio.Layouts
{
    internal class AutomaticBuilder : LayoutBuilderBase
    {
        private readonly List<ImageNode> _nodes;

        public AutomaticBuilder(ImageCssMap map, LayoutProperties props)
            : base(map, props)
        {
            _nodes = Images.Keys.Select(i => new ImageNode(
                i,
                Images[i],
                Properties.Padding,
                Properties.Margin
            ))
            .ToList();
        }

        public override void Generate()
        {
            var packer = new ImageNodePacker(_nodes);
            var final = packer.Generate();

            var addedMargin = Properties.Margin * 2;

            ResultImage = new Bitmap(
                packer.Width + addedMargin,
                packer.Height + addedMargin
            );

            using (var graphics = Graphics.FromImage(ResultImage))
            {
                foreach (var m in final)
                {
                    var rect = m.Draw(graphics);

                    CssBuilder.AppendLine(CssLine(CssClassNames[m.Name], rect));
                }
            }
        }
    }
}
