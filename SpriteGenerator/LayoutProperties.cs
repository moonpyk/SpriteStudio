using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteGenerator
{
    public class LayoutProperties
    {
        public string[] InputFilePaths;
        public string OutputSpriteFilePath;
        public string OutputCssFilePath;
        public string Layout;
        public int DistanceBetweenImages;
        public int MarginWidth;
        public int ImagesInRow;
        public int ImagesInColumn;

        public LayoutProperties()
        {
            InputFilePaths = null;
            OutputSpriteFilePath = "";
            OutputCssFilePath = "";
            Layout = "";
            DistanceBetweenImages = 0;
            MarginWidth = 0;
            ImagesInRow = 0;
            ImagesInColumn = 0;
        }
    }
}
