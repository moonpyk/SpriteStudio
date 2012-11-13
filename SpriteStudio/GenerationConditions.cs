using System.ComponentModel;
using SpriteGenerator.Annotations;

namespace SpriteStudio
{
    class GenerationConditions : INotifyPropertyChanged
    {
        private bool _imagePathOK;
        private bool _isLayoutOK;
        private bool _outputCssPathOK;
        private bool _outputImagePathOK;

        public bool OutputImagePathOK
        {
            get
            {
                return _outputImagePathOK;
            }
            set
            {
                if (value.Equals(_outputImagePathOK))
                {
                    return;
                }
                _outputImagePathOK = value;
                OnPropertyChanged("OutputImagePathOK");
            }
        }

        public bool OutputCssPathOK
        {
            get
            {
                return _outputCssPathOK;
            }
            set
            {
                if (value.Equals(_outputCssPathOK))
                {
                    return;
                }
                _outputCssPathOK = value;
                OnPropertyChanged("OutputCssPathOK");
            }
        }

        public bool ImagePathOK
        {
            get
            {
                return _imagePathOK;
            }
            set
            {
                if (value.Equals(_imagePathOK))
                {
                    return;
                }
                _imagePathOK = value;
                OnPropertyChanged("ImagePathOK");
            }
        }

        public bool IsLayoutOK
        {
            get
            {
                return _isLayoutOK;
            }
            set
            {
                if (value.Equals(_isLayoutOK))
                {
                    return;
                }
                _isLayoutOK = value;
                OnPropertyChanged("IsLayoutOK");
            }
        }

        public bool IsOK
        {
            get
            {
                return ImagePathOK && OutputCssPathOK && OutputCssPathOK && IsLayoutOK;
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

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
