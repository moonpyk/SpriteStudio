using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace SpriteStudio.Layouts
{
    public abstract class LayoutBuilderBase : IDisposable
    {
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
            var spritePath = RelativeSpriteImagePath(
                _properties.OutputSpriteFilePath,
                _properties.OutputCssFilePath
            );

            var props = new Dictionary<string, string> {
                {"background-image", string.Format("url('{0}')", spritePath)},
                {"background-repeat", "no-repeat"}
            };

            int height = Properties.ImagesHeight,
                width = Properties.ImagesWidth;

            // Images have a common width, let's put the width definition on the top
            if (width != ScannerResult.NoCommonImageSize)
            {
                props.Add("width", CssPixels(width));
            }

            // Images have a common height, let's put the height definition on the top
            if (height != ScannerResult.NoCommonImageSize)
            {
                props.Add("height", CssPixels(height));
            }

            return GenerateCss("." + baseClass, props);
        }

        protected virtual string CssLine(string cssClassName, Rectangle rect)
        {
            var props = new Dictionary<string, string> {
                {"background-position", string.Format("{0} {1}", CssPixels(-1 * rect.X), CssPixels(-1 * rect.Y))}
            };

            int commonHeight = Properties.ImagesHeight,
                commonWidth = Properties.ImagesWidth;

            // No common image height, we need to define it on the subclass
            if (commonHeight == ScannerResult.NoCommonImageSize)
            {
                props.Add("height", CssPixels(rect.Height));
            }

            // No common image width, we need to define it on the subclass
            if (commonWidth == ScannerResult.NoCommonImageSize)
            {
                props.Add("width", CssPixels(rect.Width));
            }

            return GenerateCss("." + cssClassName, props);
        }

        protected virtual string GenerateCss(string selector, IDictionary<string, string> properties)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} {{", selector);

            if (properties != null)
            {
                foreach (var g in properties.OrderBy(_ => _.Key))
                {
                    if (string.IsNullOrWhiteSpace(g.Value))
                    {
                        sb.AppendFormat("{0};", g.Key);
                    }
                    else
                    {
                        sb.AppendFormat("{0}:{1};", g.Key, g.Value);
                    }
                }
            }

            sb.Append("}");
            return sb.ToString();
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
