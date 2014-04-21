using System;
using System.Collections.Generic;

namespace SpriteStudio.Packer
{
    internal class ImageNodeComparer : IComparer<ImageNode>
    {
        private static readonly Comparer<int> C = Comparer<int>.Default;

        public int Compare(ImageNode a, ImageNode b)
        {
            var diff = C.Compare(
                Math.Max(b.Width, b.Height),
                Math.Max(a.Width, a.Height)
            );

            if (diff == 0)
            {
                diff = C.Compare(
                    Math.Min(b.Width, b.Height),
                    Math.Min(a.Width, a.Height)
               );
            }

            if (diff == 0)
            {
                diff = C.Compare(b.Height, a.Height);
            }

            if (diff == 0)
            {
                diff = C.Compare(b.Width, a.Width);
            }

            return diff;
        }
    }
}
