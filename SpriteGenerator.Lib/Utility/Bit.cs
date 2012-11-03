namespace SpriteGenerator.Utility
{
    // Bit struct. In reference, DFS-order is stored in a 0-1 sequence, 
    // this struct has been created to follow terminology.
    public struct Bit
    {
        private readonly bool _bit;

        private Bit(int n)
        {
            _bit = (n == 1);
        }

        public static implicit operator Bit(int n)
        {
            return new Bit(n);
        }

        public override bool Equals(object obj)
        {
            if (obj is Bit)
            {
                return this == ((Bit)obj);
            }
            if (obj is int)
            {
                return this == (int)obj;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return _bit.GetHashCode();
        }

        public static bool operator ==(Bit b, int n)
        {
            return (b._bit && n == 1) || (b._bit == false && n == 0);
        }

        public static bool operator !=(Bit b, int n)
        {
            return !((b._bit && n == 1) || (b._bit == false && n == 0));
        }

        public static bool operator ==(Bit b1, Bit b2)
        {
            return (b1._bit && b2._bit) || (b1._bit == false && b2._bit == false);
        }

        public static bool operator !=(Bit b1, Bit b2)
        {
            return !((b1._bit && b2._bit) || (b1._bit == false && b2._bit == false));
        }
    }
}
