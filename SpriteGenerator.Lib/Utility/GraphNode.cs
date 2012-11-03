using System.Collections.Generic;

namespace SpriteGenerator.Utility
{
    public class GraphNode
    {
        public Dictionary<int, int> IncomingEdges;
        public Dictionary<int, int> OutgoingEdges;

        public void InitializeEdges()
        {
            IncomingEdges = new Dictionary<int, int>();
            OutgoingEdges = new Dictionary<int, int>();
        }
    }
}
