namespace SpriteGenerator
{
    public class LayoutProperties
    {
        public string[] InputFilePaths;
        public string OutputSpriteFilePath;
        public string OutputCssFilePath;
        public SpriteLayout Layout;
        public int DistanceBetweenImages;
        public int MarginWidth;
        public int ImagesInRow;
        public int ImagesInColumn;

        public LayoutProperties()
        {
            InputFilePaths = null;
            OutputSpriteFilePath = "";
            OutputCssFilePath = "";
            Layout = SpriteLayout.None;
            DistanceBetweenImages = 0;
            MarginWidth = 0;
            ImagesInRow = 0;
            ImagesInColumn = 0;
        }
    }
}
