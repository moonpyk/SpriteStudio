using System.Collections.Generic;
using System.Linq;

namespace SpriteGenerator.Utility
{
    /// <summary>
    /// Code class for O-Tree representation. O-Tree encodes a placement by a sequence of 
    /// module labels and a 0-1 sequence describing the tree structure.
    /// </summary>
    public class OTree
    {
        private List<int> _moduleSequence;

        /// <summary>
        /// Creates new instance of O-Tree code with empty sequences.
        /// </summary>
        public OTree()
        {
            _moduleSequence = new List<int>();
            DfsSequence = new List<Bit>();
        }

        /// <summary>
        /// Gets or sets the sequence of module labels.
        /// </summary>
        public List<int> ModuleSequence
        {
            get
            {
                return _moduleSequence;
            }
            set
            {
                _moduleSequence = value;
            }
        }

        /// <summary>
        /// Gets or sets the 0-1 sequence of DFS-order.
        /// </summary>
        public List<Bit> DfsSequence
        {
            get;
            private set;
        }

        /// <summary>
        /// Deep copy.
        /// </summary>
        public OTree Copy()
        {
            var ot = new OTree
            {
                _moduleSequence = new List<int>(_moduleSequence),
                DfsSequence = new List<Bit>(DfsSequence)
            };

            return ot;
        }

        /// <summary>
        /// Calculates insertion positions according to DFS-sequence.
        /// </summary>
        /// <returns>IEnumerable collection of zero based indices in DFS-sequence where new element 
        /// can be inserted.</returns>
        public IEnumerable<int> InsertionPoints()
        {
            for (var i = 0; i <= DfsSequence.Count; i++)
            {
                yield return i;
            }
        }

        /// <summary>
        /// Inserts new element into O-Tree code.
        /// </summary>
        /// <param name="item">Module label.</param>
        /// <param name="dfsIndex">DFS-sequence index.</param>
        public void Insert(int item, int dfsIndex)
        {
            // Inserting 0 and 1 into DFS sequence representing a forth and a back step.
            DfsSequence.InsertRange(dfsIndex, new Bit[] { 0, 1 });

            // Calculating index of module label in moduleSequence.
            // Every forth step in DFS traversing before dfsIndex belong to a previous module in moduleSequence.
            var moduleIndex = DfsSequence.GetRange(0, dfsIndex).Count(b => b == 0);
            _moduleSequence.Insert(moduleIndex, item);
        }
    }
}
