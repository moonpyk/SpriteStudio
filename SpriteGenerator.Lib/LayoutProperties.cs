﻿using System;
using System.Collections.Generic;

namespace SpriteGenerator
{
    public class LayoutProperties : IDisposable
    {
        public LayoutProperties()
        {
            Layout = SpriteLayout.None;
            InputFilePaths = null;

            Padding = 0;
            Margin = 0;

            ImagesInRow = 0;
            ImagesInColumn = 0;

            OutputSpriteFilePath = string.Empty;
            OutputCssFilePath = string.Empty;
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

        public int Padding
        {
            get;
            set;
        }

        public int Margin
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

        public void Dispose()
        {
            if (InputFilePaths != null)
            {
                InputFilePaths.Clear();
                InputFilePaths = null;
            }

            OutputSpriteFilePath = null;
            OutputCssFilePath = null;
        }
    }
}
