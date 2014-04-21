using System;
using System.ComponentModel;
using System.Collections.Generic;
using SpriteStudio.Properties;

namespace SpriteStudio
{
    public class LayoutProperties : IDisposable, INotifyPropertyChanged
    {
        private int _imagesHeight;
        private int _imagesInColumn;
        private int _imagesInRow;
        private int _imagesWidth;
        private SpriteLayoutEnum _layout;
        private int _margin;
        private string _outputCssFilePath;
        private string _outputSpriteFilePath;
        private int _padding;

        public LayoutProperties()
        {
            Layout = SpriteLayoutEnum.None;
            InputFilePaths = null;
            AdditionalCss = null;

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
            get { return _outputSpriteFilePath; }
            set
            {
                if (value == _outputSpriteFilePath)
                {
                    return;
                }

                _outputSpriteFilePath = value;
                OnPropertyChanged("OutputSpriteFilePath");
            }
        }

        public string OutputCssFilePath
        {
            get { return _outputCssFilePath; }
            set
            {
                if (value == _outputCssFilePath)
                {
                    return;
                }

                _outputCssFilePath = value;
                OnPropertyChanged("OutputCssFilePath");
            }
        }

        public SpriteLayoutEnum Layout
        {
            get { return _layout; }
            set
            {
                if (value == _layout)
                {
                    return;
                }

                _layout = value;
                OnPropertyChanged("Layout");
            }
        }

        public int Padding
        {
            get { return _padding; }
            set
            {
                if (value == _padding)
                {
                    return;
                }

                _padding = value;
                OnPropertyChanged("Padding");
            }
        }

        public int Margin
        {
            get { return _margin; }
            set
            {
                if (value == _margin)
                {
                    return;
                }

                _margin = value;
                OnPropertyChanged("Margin");
            }
        }

        public int ImagesInRow
        {
            get { return _imagesInRow; }
            set
            {
                if (value == _imagesInRow)
                {
                    return;
                }

                _imagesInRow = value;
                OnPropertyChanged("ImagesInRow");
            }
        }

        public int ImagesInColumn
        {
            get { return _imagesInColumn; }
            set
            {
                if (value == _imagesInColumn)
                {
                    return;
                }

                _imagesInColumn = value;
                OnPropertyChanged("ImagesInColumn");
            }
        }

        public int ImagesWidth
        {
            get { return _imagesWidth; }
            set
            {
                if (value == _imagesWidth)
                {
                    return;
                }

                _imagesWidth = value;
                OnPropertyChanged("ImagesWidth");
            }
        }

        public int ImagesHeight
        {
            get { return _imagesHeight; }
            set
            {
                if (value == _imagesHeight)
                {
                    return;
                }

                _imagesHeight = value;
                OnPropertyChanged("ImagesHeight");
            }
        }

        public IDictionary<string, string> AdditionalCss
        {
            get;
            set;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (InputFilePaths != null)
            {
                InputFilePaths.Clear();
                InputFilePaths = null;
            }

            if (AdditionalCss != null)
            {
                AdditionalCss.Clear();
                AdditionalCss = null;
            }

            OutputSpriteFilePath = null;
            OutputCssFilePath = null;
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
