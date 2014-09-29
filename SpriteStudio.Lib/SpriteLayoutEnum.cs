namespace SpriteStudio
{
    public enum SpriteLayoutEnum
    {
        None,
        Automatic,
        Horizontal,
        Vertical,
        Rectangular,
    }

    public static class SpriteLayoutExtensions
    {
        /// <summary>
        /// Parses a string and returns a <see cref="SpriteLayoutEnum"/> value.
        /// </summary>
        public static SpriteLayoutEnum FromString(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return SpriteLayoutEnum.None;
            }

            switch (name.ToLowerInvariant())
            {
                case "automatic":
                    return SpriteLayoutEnum.Automatic;

                case "horizontal":
                    return SpriteLayoutEnum.Horizontal;

                case "vertical":
                    return SpriteLayoutEnum.Vertical;

                case "rectangular":
                    return SpriteLayoutEnum.Rectangular;
            }

            return SpriteLayoutEnum.None;
        }

        /// <summary>
        /// Convenience method to get a sprite layout from a <see cref="string"/> instance.
        /// </summary>
        public static SpriteLayoutEnum ToSpriteLayout(this string s)
        {
            return FromString(s);
        }
    }
}
