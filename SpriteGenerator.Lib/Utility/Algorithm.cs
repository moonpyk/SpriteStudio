using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpriteGenerator.Utility
{
    public static class Algorithm
    {
        /// <summary>
        /// Greedy algorithm.
        /// </summary>
        /// <param name="modules">List of modules that represent the images that need to be inserted into the sprite.</param>
        /// <returns>Near optimal placement.</returns>
        public static Placement Greedy(List<Module> modules)
        {
            OTHelper finalOt = null;

            // Empty O-Tree code.
            var oTree = new OTree();

            // Empty list of modules.
            var moduleList = new List<Module>();

            // For each module which needs to be inserted.
            foreach (var module in modules)
            {
                OTree bestOTree = null;

                // Add module to the list of already packed modules.
                moduleList.Add(module);

                // Set the minimum perimeter of the placement to high.
                var minPerimeter = Int32.MaxValue;

                // Try all insertion points.
                Debug.Assert(oTree != null, "oTree != null");

                foreach (var insertionPoint in oTree.InsertionPoints())
                {
                    var copy = oTree.Copy();
                    copy.Insert(module.Name, insertionPoint);

                    var ot = new OTHelper(copy, moduleList);
                    var pm = ot.Placement;

                    // Choose the one with the minimum perimeter.
                    if (pm.Perimeter >= minPerimeter)
                    {
                        continue;
                    }

                    finalOt = ot;
                    bestOTree = copy;
                    minPerimeter = pm.Perimeter;
                }

                oTree = bestOTree;
            }

            Debug.Assert(finalOt != null, "finalOt != null");
            return finalOt.Placement;
        }
    }
}
