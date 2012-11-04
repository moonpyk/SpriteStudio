using System.Collections.Generic;

namespace SpriteGenerator.Utility
{
    public class GraphNode
    {
        public IDictionary<int, int> IncomingEdges;
        public IDictionary<int, int> OutgoingEdges;

        public void InitializeEdges()
        {
            IncomingEdges = new Dictionary<int, int>();
            OutgoingEdges = new Dictionary<int, int>();
        }
    }
}
