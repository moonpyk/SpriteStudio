using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;

namespace SpriteGenerator.Layouts
{
    public abstract class LayoutBuilderBase : IDisposable
    {
        public const string CssSpriteDeclarationFormat = ".{1} {{ background-image: url('{0}'); background-color: transparent; background-repeat: no-repeat; }}";
        public const string CssLineDeclarationFormat = ".{0} {{ width: {1}; height: {2}; background-position: {3} {4}; }}";

        private readonly StringBuilder _cssBuilder = new StringBuilder();

        private readonly IDictionary<int, string> _cssClassNames;
        private readonly IDictionary<int, Image> _images;
        private readonly LayoutProperties _properties;

        private bool _disposed;

        protected LayoutBuilderBase(ImageCssMap map, LayoutProperties props)
        {
            _images = map.Images;
            _cssClassNames = map.CssClassesNames;
            _properties = props;
        }

        public LayoutProperties Properties
        {
            get
            {
                return _properties;
            }
        }

        public IDictionary<int, string> CssClassNames
        {
            get
            {
                return _cssClassNames;
            }
        }

        public IDictionary<int, Image> Images
        {
            get
            {
                return _images;
            }
        }

        public string CssCode
        {
            get
            {
                return _cssBuilder.ToString();
            }
        }

        protected StringBuilder CssBuilder
        {
            get
            {
                return _cssBuilder;
            }
        }

        public Image ResultImage
        {
            get;
            protected set;
        }

        #region IDisposable Members

        public virtual void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _cssBuilder.Clear();

            if (ResultImage != null)
            {
                ResultImage.Dispose();
                ResultImage = null;
            }

            _disposed = true;
        }

        #endregion

        public abstract void Generate();

        public virtual string GetSpriteDefinition(string baseClass)
        {
            return string.Format(
                CssSpriteDeclarationFormat + Environment.NewLine,
                RelativeSpriteImagePath(
                    _properties.OutputSpriteFilePath,
                    _properties.OutputCssFilePath
                ),
                baseClass
            );
        }

        protected virtual string CssLine(string cssClassName, Rectangle rect)
        {
            var line = string.Format(CssLineDeclarationFormat,
                cssClassName,
                CssPixels(rect.Width),
                CssPixels(rect.Height),
                CssPixels(-1 * rect.X),
                CssPixels(-1 * rect.Y)
            );

            return line;
        }

        protected static string CssPixels(int val)
        {
            if (val == 0)
            {
                return "0";
            }

            return val.ToString(CultureInfo.InvariantCulture) + "px";
        }

        // Relative sprite image file path
        protected static string RelativeSpriteImagePath(string outputSpriteFilePath, string outputCssFilePath)
        {
            var sep = Path.DirectorySeparatorChar;

            var spltOutputCssFilePath = outputCssFilePath.Split(sep);
            var spltOutputSpriteFilePath = outputSpriteFilePath.Split(sep);

            var breakAt = 0;

            for (var i = 0; i < spltOutputCssFilePath.Length; i++)
            {
                if (i >= spltOutputSpriteFilePath.Length || spltOutputCssFilePath[i] == spltOutputSpriteFilePath[i])
                {
                    continue;
                }

                breakAt = i;
                break;
            }

            var relativePath = "";

            for (var i = 0; i < spltOutputCssFilePath.Length - breakAt - 1; i++)
            {
                relativePath += "../";
            }

            relativePath += String.Join("/", spltOutputSpriteFilePath, breakAt, spltOutputSpriteFilePath.Length - breakAt);

            return relativePath;
        }
    }
}
