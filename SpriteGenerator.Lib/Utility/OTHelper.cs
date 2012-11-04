using System.Collections.Generic;
using System.Linq;
using SpriteGenerator.Utility.Contour;

namespace SpriteGenerator.Utility
{
    public class OTHelper
    {
        private readonly OTree _oTree;
        private readonly IDictionary<int, Module> _modules;
        private Placement _placement;

        /// <summary>
        /// Algoritms class for O-Tree representation.
        /// </summary>
        /// <param name="ot">The O-tree code which describes the placement.</param>
        /// <param name="modules">Modules to be packed.</param>
        public OTHelper(OTree ot, IEnumerable<Module> modules)
        {
            _oTree = ot;
            _modules = modules.ToDictionary(_ => _.Name, _ => _);
            _placement = null;
        }

        /// <summary>
        /// Gets the calculated placement.
        /// </summary>
        public Placement Placement
        {
            get
            {
                ToCompact();
                return _placement;
            }
        }

        /// <summary>
        /// Operation of horizontal O-tree. Calculates coordinates and takes one compaction step.
        /// </summary>
        /// <returns>Vertical constraint graph</returns>
        private Graph ToVerticalConstraintGraph()
        {
            // Empty module representing the root of the tree
            var root = new Module(-1, null, 0);
            _modules.Add(-1, root);
            _oTree.ModuleSequence.Insert(0, -1);

            // Stack containing module labels for parent module calculation
            var stack = new Stack<int>();
            stack.Push(-1);

            // Vertical contsraint graph
            var constraintGraph = new Graph(_oTree.ModuleSequence);

            // Actual parent and child module
            var parent = root;

            // Horizontal contour for quick y-coordinate calculation
            ContourAbstract contour = new HorizontalContour(root);

            // Index of child module in ModuleSequence
            var childIndex = 0;

            foreach (var bit in _oTree.DfsSequence)
            {
                // Forth step in DFS traversing, coordinates need to be calcuted
                if (bit == 0)
                {
                    var child = _modules[_oTree.ModuleSequence[++childIndex]];

                    // In horizontal O-tree, child module is on the rigth side of the parent module and adjacent with it
                    child.X = parent.X + parent.Width;
                    // Finding the minimum y-coordinate
                    child.Y = contour.FindMax(child.X + child.Width);

                    // There is an egde in the vertical constraint graph, where the minimum is found
                    constraintGraph.AddEdge(contour.WhereMax.Name, child.Name, child.X);

                    // Updating contour
                    contour.Update(child);

                    // Now child module is the actual parent
                    parent = child;
                    stack.Push(parent.Name);
                }
                else // Back step in DFS traversing
                {
                    // Updating parent module and the insertation index of the contour
                    stack.Pop();
                    parent = _modules[stack.Peek()];
                    contour.InsertationIndex = contour.ModuleSequence.IndexOf(parent);
                }

            }

            //Removing root module
            _modules.Remove(-1);
            _oTree.ModuleSequence.RemoveAt(0);

            return constraintGraph;
        }

        /// <summary>
        /// Operation of vertical O-tree. Calculates coordinates and takes one compaction step.
        /// </summary>
        /// <returns>Horizontal constraint graph</returns>
        private Graph ToHorizontalConstraintGraph()
        {
            // Empty module representing the root of the tree
            var root = new Module(-1, null, 0);
            _modules.Add(-1, root);
            _oTree.ModuleSequence.Insert(0, -1);

            // Stack containing module labels for parent module calculation
            var stack = new Stack<int>();
            stack.Push(-1);

            // Vertical contsraint graph
            var constraintGraph = new Graph(_oTree.ModuleSequence);

            // Actual parent and child module
            var parent = root;

            //Vertical contour for quick x-coordinate calculation
            ContourAbstract contour = new VerticalContour(root);

            //Index of child module in ModuleSequence
            var childIndex = 0;

            foreach (var bit in _oTree.DfsSequence)
            {
                //Forth step in DFS traversing, coordinates need to be calcuted
                if (bit == 0)
                {
                    var child = _modules[_oTree.ModuleSequence[++childIndex]];

                    //In vertical O-tree, child module is on the top of parent module and adjacent with it
                    child.Y = parent.Y + parent.Height;
                    //Finding the minimum x-coordinate
                    child.X = contour.FindMax(child.Y + child.Height);

                    //There is an egde in the horizontal constraint graph, where the minimum is found
                    constraintGraph.AddEdge(contour.WhereMax.Name, child.Name, child.Y);

                    //Updating contour
                    contour.Update(child);

                    //Now child module is the actual parent
                    parent = child;
                    stack.Push(parent.Name);
                }

                // Back step in DFS traversing
                else
                {
                    //Updating parent module and insertation index of the contour
                    stack.Pop();
                    parent = _modules[stack.Peek()];
                    contour.InsertationIndex = contour.ModuleSequence.IndexOf(parent);
                }

            }

            //Removing root module
            _modules.Remove(-1);
            _oTree.ModuleSequence.RemoveAt(0);
            return constraintGraph;
        }

        /// <summary>
        /// Creates LB-compact placement with sequence of compaction steps.
        /// </summary>
        private void ToCompact()
        {
            // Stop condition of while loop, true if compaction steps change the O-tree
            var changed = true;

            // Copy of components of the actual O-tree. If compaction steps does not change them, placement is compact.
            var dfsSequenceCopy = new List<Bit>(_oTree.DfsSequence);
            var moduleSequenceCopy = new List<int>(_oTree.ModuleSequence);

            while (changed)
            {
                changed = false;

                //Vertical constraint graph from horizontal O-tree.
                var gVertical = ToVerticalConstraintGraph();

                //Vertical O-tree from vertical constraint graph.
                _oTree.ModuleSequence = gVertical.DepthFirstSearch(_oTree.DfsSequence);

                // Horizontal constraint graph from vertical O-tree.
                var gHorizontal = ToHorizontalConstraintGraph();

                // Horizontal O-tree from horizontal constraint graph.
                _oTree.ModuleSequence = gHorizontal.DepthFirstSearch(_oTree.DfsSequence);

                // Checking the changes of the O-Tree after compaction steps.
                // If the O-tree has changed, more compaction steps could be needed on the changed tree.
                if (_oTree.DfsSequence.SequenceEqual(dfsSequenceCopy) && _oTree.ModuleSequence.SequenceEqual(moduleSequenceCopy))
                {
                    continue;
                }

                moduleSequenceCopy = new List<int>(_oTree.ModuleSequence);
                dfsSequenceCopy = new List<Bit>(_oTree.DfsSequence);
                changed = true;
            }

            // Compact placement.
            _placement = new Placement(_modules.Values.ToList());
        }
    }
}
