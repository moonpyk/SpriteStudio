using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteGenerator
{
    public enum SpriteLayout
    {
        None,
        Automatic,
        Horizontal,
        Vertical,
        Rectangular,
    }

    public static class SpriteLayoutUtil
    {
        public static SpriteLayout FromString(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return SpriteLayout.None;
            }

            switch (name.ToLowerInvariant())
            {
                case "automatic":
                    return SpriteLayout.Automatic;

                case "horizontal":
                    return SpriteLayout.Horizontal;

                case "vertical":
                    return SpriteLayout.Vertical;

                case "rectangular":
                    return SpriteLayout.Rectangular;
            }

            return SpriteLayout.None;
        }
    }
}
