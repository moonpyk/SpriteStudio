using System;
using System.Collections.Generic;
using System.Linq;

namespace SpriteGenerator.Packer
{
    public class ImageNodePacker
    {
        private readonly List<ImageNode> _blocks;
        private ImageNode _root;

        public ImageNodePacker(IEnumerable<ImageNode> blocks)
        {
            _blocks = new List<ImageNode>(blocks);
            _blocks.Sort(new ImageNodeComparer());

            var len = _blocks.Count();

            var width = 0;
            var height = 0;

            if (len > 0)
            {
                var firstImage = _blocks.First();

                width = firstImage.Width;
                height = firstImage.Height;
            }

            _root = new ImageNode
            {
                X = 0,
                Y = 0,
                Width = width,
                Height = height
            };
        }

        public int Width
        {
            get
            {
                return _root.Width;
            }
        }

        public int Height
        {
            get
            {
                return _root.Height;
            }
        }

        /// <summary>
        /// Arrages 
        /// </summary>
        /// <exception cref="System.Exception">Can't fit a block into root - this should not happen if images are pre-sorted correctly</exception>
        public IList<ImageNode> Generate()
        {
            foreach (var block in _blocks)
            {
                PlaceBlock(block);
            }

            return _blocks;
        }

        /// <exception cref="System.Exception">Can't fit a block into root - this should not happen if images are pre-sorted correctly</exception>
        private void PlaceBlock(ImageNode block)
        {
            var node = FindNode(_root, block.Width, block.Height);

            if (node != null)
            {
                SplitNode(node, block.Width, block.Height);
                block.X = node.X;
                block.Y = node.Y;
                return;
            }

            _root = Grow(_root, block.Width, block.Height);
            PlaceBlock(block);
        }

        private static ImageNode FindNode(ImageNode root, int width, int height)
        {
            if (root.Used)
            {
                var rightNode = FindNode(root.Right, width, height);
                if (rightNode != null)
                {
                    return rightNode;
                }

                var downNode = FindNode(root.Down, width, height);

                if (downNode != null)
                {
                    return downNode;
                }
            }
            else if (width <= root.Width && height <= root.Height)
            {
                return root;
            }

            return null;
        }

        private static void SplitNode(ImageNode n, int w, int h)
        {
            n.Used = true;

            n.Down = new ImageNode
            {
                X = n.X,
                Y = n.Y + h,
                Width = n.Width,
                Height = n.Height - h
            };

            n.Right = new ImageNode
            {
                X = n.X + w,
                Y = n.Y,
                Width = n.Width - w,
                Height = h
            };
        }

        /// <exception cref="Exception">Can't fit a block into root - this should not happen if images are pre-sorted correctly</exception>
        private static ImageNode Grow(ImageNode root, int w, int h)
        {
            var canGrowDown = w <= root.Width;
            var canGrowRight = h <= root.Height;

            // Attempt to keep square-ish by growing right when height is much greater than width
            var shouldGrowRight = canGrowRight && (root.Height >= (root.Width + w));

            // Attempt to keep square-ish by growing down  when width  is much greater than height
            var shouldGrowDown = canGrowDown && (root.Width >= (root.Height + h));

            if (shouldGrowRight)
            {
                return GrowRight(root, w, h);
            }

            if (shouldGrowDown)
            {
                return GrowDown(root, w, h);
            }

            if (canGrowRight)
            {
                return GrowRight(root, w, h);
            }

            if (canGrowDown)
            {
                return GrowDown(root, w, h);
            }

            throw new Exception(string.Format("Can't fit {0}x{1} block into root {2}x{3} - this should not happen if images are pre-sorted correctly",
                w, h, root.Width, root.Height
            ));
        }

        private static ImageNode GrowRight(ImageNode root, int w, int h)
        {
            return new ImageNode
            {
                Used = true,
                X = 0,
                Y = 0,
                Width = root.Width + w,
                Height = root.Height,
                Down = root,
                Right = new ImageNode
                {
                    X = root.Width,
                    Y = 0,
                    Width = w,
                    Height = root.Height
                }
            };
        }

        private static ImageNode GrowDown(ImageNode root, int w, int h)
        {
            return new ImageNode
            {
                Used = true,
                X = 0,
                Y = 0,
                Width = root.Width,
                Height = root.Height + h,
                Down = new ImageNode
                {
                    X = 0,
                    Y = root.Height,
                    Width = root.Width,
                    Height = h
                },
                Right = root
            };
        }
    }
}
