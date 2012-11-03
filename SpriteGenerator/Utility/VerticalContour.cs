using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteGenerator.Utility
{
    class VerticalContour : ContourAbstract
    {
        /// <summary>
        /// Contour class for quick computation of x-coordinates during working with vertical O-Tree.
        /// </summary>
        /// <param name="root">First element of the contour.</param>
        public VerticalContour(Module root)
        {
            Construct(root);
        }

        /// <summary>
        /// Finds the minimum x-coordinate where the module can be inserted.
        /// </summary>
        /// <param name="to">Maximum y-coordinate until modules on the left of the actual module need to be checked.</param>
        /// <returns></returns>
        public override int FindMax(int to)
        {
            var max = 0;

            //Actual module does not need to be checked.
            var indexFrom = InsertationIndex + 1;

            //Checking modules in contour.
            while (indexFrom < ModuleSequence.Count && ModuleSequence[indexFrom].Y < to)
            {
                //Overwriting maximum.
                if (max < ModuleSequence[indexFrom].X + ModuleSequence[indexFrom].Width)
                {
                    max = ModuleSequence[indexFrom].X + ModuleSequence[indexFrom].Width;
                    WhereMax = ModuleSequence[indexFrom];
                }

                //Removing modules, which are covered by the module will be inserted.
                if (ModuleSequence[indexFrom].Y + ModuleSequence[indexFrom].Height <= to)
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
