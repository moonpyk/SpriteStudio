using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SpriteGenerator.Utility
{
    static class Algorithm
    {
        /// <summary>
        /// Greedy algorithm.
        /// </summary>
        /// <param name="modules">List of modules that represent the images that need to be inserted into the sprite.</param>
        /// <returns>Near optimal placement.</returns>
        public static Placement Greedy(List<Module> modules)
        {
            // Empty O-Tree code.
            var oTree = new OTree();
            OT finalOt = null;

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

                // Try all insertation point.
                Debug.Assert(oTree != null, "oTree != null");

                foreach (var insertationPoint in oTree.InsertationPoints())
                {
                    var ot = oTree.Copy();
                    ot.Insert(module.Name, insertationPoint);
                    var oT = new OT(ot, moduleList);
                    var pm = oT.Placement;

                    // Choose the one with the minimum perimeter.
                    if (pm.Perimeter >= minPerimeter)
                    {
                        continue;
                    }

                    finalOt = oT;
                    bestOTree = ot;
                    minPerimeter = pm.Perimeter;
                }
                oTree = bestOTree;
            }

            Debug.Assert(finalOt != null, "finalOt != null");
            return finalOt.Placement;
        }
    }
}
