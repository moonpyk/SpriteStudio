using System;
using System.Collections.Generic;

namespace SpriteStudio
{
    public class ScannerResult : IDisposable {
        public const int NoCommonImageSize = -1;

        public IList<SpriteLayout> AvailableLayouts
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

        #region IDisposable Members

        public void Dispose()
        {
            if (AvailableLayouts != null)
            {
                AvailableLayouts.Clear();
                AvailableLayouts = null;
            }

            if (FileList != null)
            {
                FileList.Clear();
                FileList = null;
            }
        }

        #endregion
    }
}
