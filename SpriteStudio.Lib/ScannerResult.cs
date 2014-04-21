using System;
using System.Collections.Generic;

namespace SpriteStudio
{
    public class ScannerResult
    {
        public const int NoCommonImageSize = -1;

        public IList<SpriteLayoutEnum> AvailableLayouts
        {
            get;
            internal set;
        }

        public int ImagesWidth
        {
            get;
            internal set;
        }

        public int ImagesHeight
        {
            get;
            internal set;
        }

        public IList<string> FileList
        {
            get;
            internal set;
        }
    }
}
