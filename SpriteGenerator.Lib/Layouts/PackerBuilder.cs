using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SpriteGenerator.Packer;

namespace SpriteGenerator.Layouts
{
    public class PackerBuilder : LayoutBuilderBase
    {
        private readonly List<ImageNode> _nodes;

        public PackerBuilder(ImageCssMap map, LayoutProperties props)
            : base(map, props)
        {
            _nodes = Images.Keys.Select(i => new ImageNode(
                    i,
                    Images[i],
                    Properties.Padding,
                    Properties.Margin)
                )
                .ToList();
        }

        public override void Generate()
        {
            var packer = new Packer.ImageNodePacker(_nodes);
            var final = packer.Generate();

            ResultImage = new Bitmap(packer.Width, packer.Height);
            var graphics = Graphics.FromImage(ResultImage);

            // Drawing images into the result image in the original order and writing CSS lines.
            foreach (var m in final)
            {
                var rect = m.Draw(graphics);

                CssBuilder.AppendLine(CssLine(CssClassNames[m.Name], rect));
            }
        }
    }
}
