using System.Collections.Generic;
using System.Linq;

namespace SpriteGenerator.Utility
{
    public class Graph
    {
        protected Dictionary<int, GraphNode> Nodes;

        /// <summary>
        /// Directed graph with weigted edges.
        /// </summary>
        /// <param name="nodes">List of node labels.</param>
        public Graph(IEnumerable<int> nodes)
        {
            //Nodes of the graph.
            Nodes = new Dictionary<int, GraphNode>();

            //Initializing list of edges for all nodes.
            foreach (var node in nodes)
            {
                var gn = new GraphNode();
                gn.InitializeEdges();
                Nodes.Add(node, gn);
            }
        }

        /// <summary>
        /// Adds edge to the graph.
        /// </summary>
        /// <param name="from">Source node of the edge.</param>
        /// <param name="to">Sink node of the edge.</param>
        /// <param name="weight">Weight of the edge.</param>
        public void AddEdge(int from, int to, int weight)
        {
            Nodes[from].OutgoingEdges.Add(to, weight);
            Nodes[to].IncomingEdges.Add(from, weight);
        }

        /// <summary>
        /// Ordered visit, neighbors with lower weight have higher preference. If the graph is horizontal, weight 
        /// means x-coordinate of the module, which is represented by the sink node of the edge. If the graph is 
        /// vetrical, weight means the y-coordinate.
        /// </summary>
        /// <param name="node">Actual node.</param>
        /// <param name="visitedNodes">Visited nodes.</param>
        /// <param name="topologicalOrder">Topological order of the graph.</param>
        /// <param name="dfsSequence">0-1 sequence representing of the DFS traversing. 0 means forth step,
        /// 1 means back step.</param>
        private void VisitNode(int node, Dictionary<int, bool> visitedNodes, List<int> topologicalOrder, List<Bit> dfsSequence)
        {
            //If node is not visited yet
            if (!visitedNodes[node])
            {
                visitedNodes[node] = true;

                // Adding node to topological order and adding a 0 to DFS sequense strores the forth step belongs 
                // to the module
                topologicalOrder.Add(node);
                dfsSequence.Add(0);

                // Ordering neighbors by their weights
                var orderedNeighbors = from neighbor in Nodes[node].OutgoingEdges.Keys
                                       orderby Nodes[node].OutgoingEdges[neighbor]
                                       select neighbor;

                // Ordered list of neighbors
                var neighborsOfNode = new List<int>(orderedNeighbors);

                // Visiting nodes in the afore calculated order.
                foreach (int neighbor in neighborsOfNode)
                {
                    VisitNode(neighbor, visitedNodes, topologicalOrder, dfsSequence);
                }
            }

            //If node and all of its DFS descendants are visited, it's needed to store the back step 
            //belonging to the module.
            dfsSequence.Add(1);
        }

        /// <summary>
        /// Ordered depth first search. Calculates module sequence and DFS sequence belonging to the graph. 
        /// These determines the O-Tree with the same orientation what the graph has.
        /// </summary>
        /// <param name="dfsSequence">O-Tree DFSSequence.</param>
        /// <returns>O-Tree ModuleSequence.</returns>
        public List<int> DepthFirstSearch(List<Bit> dfsSequence)
        {
            //Clearing DFS sequence and creating an empty list of module labels.
            dfsSequence.Clear();
            var nodeOrder = new List<int>();
            //Every node is unvisited yet.
            var visitedNodes = Nodes.Keys.ToDictionary(item => item, item => false);

            //Visit starts at root module representing by label -1.
            VisitNode(-1, visitedNodes, nodeOrder, dfsSequence);

            //Removing root module.
            nodeOrder.RemoveAt(0);
            dfsSequence.RemoveAt(0);
            dfsSequence.RemoveAt(dfsSequence.Count - 1);

            return nodeOrder;
        }
    }
}
