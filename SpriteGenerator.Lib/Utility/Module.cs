using System.Drawing;

namespace SpriteGenerator.Utility
{
    public class Module
    {
        private readonly int _name;
        private readonly int _width;
        private readonly int _height;
        private readonly int _whiteSpace;
        private readonly Image _image;

        private int _xCoordinate;
        private int _yCoordinate;

        /// <summary>
        /// Module class representing an image and it's size including white space around the image.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="image"></param>
        /// <param name="whiteSpace">Width of white space around the image.</param>
        public Module(int name, Image image, int whiteSpace)
        {
            _name = name;

            if (image != null)
            {
                _width = image.Width + whiteSpace;
                _height = image.Height + whiteSpace;
            }
            //Empty module
            else
            { _width = _height = 0; }

            _whiteSpace = whiteSpace;
            _xCoordinate = 0;
            _yCoordinate = 0;
            _image = image;
        }

        /// <summary>
        /// Gets the width of the module.
        /// </summary>
        public int Width
        {
            get
            {
                return _width;
            }
        }

        /// <summary>
        /// Gets the height of the module.
        /// </summary>
        public int Height
        {
            get
            {
                return _height;
            }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the module's bottom left corner.
        /// </summary>
        public int X
        {
            get
            {
                return _xCoordinate;
            }
            set
            {
                _xCoordinate = value;
            }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the module's bottom left corner.
        /// </summary>
        public int Y
        {
            get
            {
                return _yCoordinate;
            }
            set
            {
                _yCoordinate = value;
            }
        }

        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        public int Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Sets coordinates of module to zero.
        /// </summary>
        public void ClearCoordinates()
        {
            _xCoordinate = 0;
            _yCoordinate = 0;
        }

        /// <summary>
        /// Deep copy.
        /// </summary>
        /// <returns></returns>
        public Module Copy()
        {
            return new Module(_name, _image, _whiteSpace)
            {
                _xCoordinate = _xCoordinate,
                _yCoordinate = _yCoordinate
            };
        }

        /// <summary>
        /// Draws the module into a graphics object.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="marginWidth">Margin width around the sprite.</param>
        public void Draw(Graphics graphics, int marginWidth)
        {
            graphics.DrawImage(
                _image,
                _xCoordinate + marginWidth,
                _yCoordinate + marginWidth,
                _image.Width,
                _image.Height
            );
        }
    }
}
