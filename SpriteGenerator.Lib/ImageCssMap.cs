using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpriteGenerator
{
    public class ImageCssMap : Tuple<IDictionary<int, Image>, IDictionary<int, string>>
    {
        public ImageCssMap(IDictionary<int, Image> images, IDictionary<int, string> items)
            : base(images, items)
        { }

        public IDictionary<int, Image> Images
        {
            get
            {
                return Item1;
            }
        }

        public IDictionary<int, string> CssClassesNames
        {
            get
            {
                return Item2;
            }
        }
    }
}