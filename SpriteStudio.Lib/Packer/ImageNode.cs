using System;
using System.Drawing;

namespace SpriteStudio.Packer
{
    public class ImageNode
    {
        private readonly Image _image;
        private readonly int _margin;
        private readonly int _name;

        public ImageNode()
        {
            _image = null;
            _name = int.MinValue;
            _margin = 0;
        }

        /// <summary>
        /// </summary>
        /// <exception cref="ArgumentNullException">image</exception>
        public ImageNode(int name, Image image, int padding, int margin)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            _image = image;
            _name = name;
            _margin = margin;

            var addPadding = (2 * padding);

            Width = image.Width + addPadding;
            Height = image.Height + addPadding;
        }

        public bool Used
        {
            get;
            set;
        }

        public int X
        {
            get;
            set;
        }

        public int Y
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public ImageNode Down
        {
            get;
            set;
        }

        public ImageNode Right
        {
            get;
            set;
        }

        public int Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Draws the module into a graphics object.
        /// </summary>
        /// <param name="graphics">The <see cref="Graphics"/> instance to draw on</param>
        public Rectangle Draw(Graphics graphics)
        {
            var realX      = X + _margin;
            var realY      = Y + _margin;
            var realWidth  = _image.Width;
            var realHeight = _image.Height;

            graphics.DrawImage(
                _image,
                realX,
                realY,
                realWidth,
                realHeight
            );

            return new Rectangle(
                realX,
                realY,
                realWidth,
                realHeight
            );
        }
    }
}
