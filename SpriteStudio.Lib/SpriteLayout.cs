namespace SpriteStudio
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
        /// <summary>
        /// Parses a string and returns a <see cref="SpriteLayout"/> value.
        /// </summary>
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

        /// <summary>
        /// Convenience method to get a sprite layout from a <see cref="string"/> instance.
        /// </summary>
        public static SpriteLayout ToSpriteLayout(this string s)
        {
            return FromString(s);
        }
    }
}
