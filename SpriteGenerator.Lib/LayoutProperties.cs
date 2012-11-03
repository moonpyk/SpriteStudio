using System.Collections.Generic;

namespace SpriteGenerator
{
    public class LayoutProperties
    {
        public LayoutProperties()
        {
            Layout = SpriteLayout.None;
            InputFilePaths = null;

            DistanceBetweenImages = 0;
            MarginWidth = 0;

            ImagesInRow = 0;
            ImagesInColumn = 0;

            OutputSpriteFilePath = "";
            OutputCssFilePath = "";
        }

        public IList<string> InputFilePaths
        {
            get;
            set;
        }

        public string OutputSpriteFilePath
        {
            get;
            set;
        }

        public string OutputCssFilePath
        {
            get;
            set;
        }

        public SpriteLayout Layout
        {
            get;
            set;
        }

        public int DistanceBetweenImages
        {
            get;
            set;
        }

        public int MarginWidth
        {
            get;
            set;
        }

        public int ImagesInRow
        {
            get;
            set;
        }

        public int ImagesInColumn
        {
            get;
            set;
        }
    }
}
