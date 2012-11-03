using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteGenerator.Utility
{
    public class HorizontalContour : ContourAbstract
    {
        /// <summary>
        /// Contour class for quick computation of y-coordinates during working with horizontal O-Tree.
        /// </summary>
        /// <param name="root">First element of the contour.</param>
        public HorizontalContour(Module root)
        {
            Construct(root);
        }

        /// <summary>
        /// Finds the minimum y-coordinate where the module can be inserted.
        /// </summary>
        /// <param name="to">Maximum x-coordinate until modules below the actual module need to be checked.</param>
        /// <returns></returns>
        public override int FindMax(int to)
        {
            var max = 0;

            // Actual module does not need to be checked.
            var indexFrom = InsertationIndex + 1;

            // Checking modules in contour.
            while (indexFrom < ModuleSequence.Count && ModuleSequence[indexFrom].X < to)
            {
                // Overwriting maximum.
                if (max < ModuleSequence[indexFrom].Y + ModuleSequence[indexFrom].Height)
                {
                    max = ModuleSequence[indexFrom].Y + ModuleSequence[indexFrom].Height;
                    WhereMax = ModuleSequence[indexFrom];
                }

                // Removing modules, which are covered by the module will be isnserted.
                if (ModuleSequence[indexFrom].X + ModuleSequence[indexFrom].Width <= to)
                {
                    ModuleSequence.RemoveAt(indexFrom);
                }
                else
                {
                    indexFrom++;
                }
            }

            return max;
        }
    }
}
