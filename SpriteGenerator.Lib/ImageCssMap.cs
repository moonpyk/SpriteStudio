using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpriteGenerator
{
    internal class ImageCssMap : Tuple<Dictionary<int, Image>, Dictionary<int, string>>
    {
        public ImageCssMap(Dictionary<int, Image> images, Dictionary<int, string> items)
            : base(images, items)
        { }

        public Dictionary<int, Image> Images
        {
            get
            {
                return Item1;
            }
        }

        public Dictionary<int, string> CssClassesNames
        {
            get
            {
                return Item2;
            }
        }
    }
}