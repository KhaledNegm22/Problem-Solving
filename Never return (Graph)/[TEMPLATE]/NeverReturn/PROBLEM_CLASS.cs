using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem
{
    public static class PROBLEM_CLASS
    {
        public class Landmark
        {
            public int Id;
            public int X, Y;
            public bool IsInside;

            public Landmark(int id, int x, int y, bool isInside)
            {
                Id = id;
                X = x;
                Y = y;
                IsInside = isInside;
            }
        }

        public static Dictionary<int, List<int>> CreateGraph(List<Landmark> landmarks, List<Tuple<int, int, int>> trails)
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();

            foreach (var trail in trails)
            {
                int from = trail.Item1;
                int to = trail.Item2;

                if (!graph.ContainsKey(from))
                {
                    graph[from] = new List<int>();
                }
                graph[from].Add(to);

                // Since it's a directed graph, we won't add a reverse edge
                // This ensures the "never return" rule is followed
            }

            // Adding landmarks with no outgoing edges to prevent missing landmarks
            foreach (var landmark in landmarks)
            {
                if (!graph.ContainsKey(landmark.Id))
                {
                    graph[landmark.Id] = new List<int>();
                }
            }

            return graph;
        }

        public static Dictionary<int, long> FindShortestPath(Dictionary<int, List<int>> graph, int start)
        {
            Dictionary<int, long> distances = new Dictionary<int, long>();
            Stack<int> stack = new Stack<int>();
            HashSet<int> visited = new HashSet<int>();

            foreach (var node in graph.Keys)
            {
                distances[node] = long.MaxValue;
            }

            distances[start] = 0;
            stack.Push(start);

            while (stack.Count > 0)
            {
                int current = stack.Pop();

                if (!visited.Contains(current))
                {
                    visited.Add(current);

                    if (graph.ContainsKey(current))
                    {
                        foreach (var neighbor in graph[current])
                        {
                            if (!visited.Contains(neighbor))
                            {
                                stack.Push(neighbor);
                            }
                        }
                    }
                }
            }

            return distances;
        }

        public static int RequiredFunction(List<Landmark> landmarks, List<Tuple<int, int, int>> trails, int N)
        {
            Dictionary<int, List<int>> graph = CreateGraph(landmarks, trails);
            int startNode = 0;

            var distances = FindShortestPath(graph, startNode);

            foreach (var node in graph.Keys)
            {
                if (distances[node] != long.MaxValue)
                {
                    if (graph.ContainsKey(node))
                    {
                        foreach (var neighbor in graph[node])
                        {
                            int weight = trails.FirstOrDefault(t => t.Item1 == node && t.Item2 == neighbor)?.Item3 ?? 0;

                            var newDistance = distances[node] + weight;


                            if (newDistance < distances[neighbor])
                            {
                                distances[neighbor] = newDistance;
                            }
                        }
                    }
                }
            }

            long closestDistance = long.MaxValue;
            foreach (var landmark in landmarks)
            {
                if (!landmark.IsInside && distances.ContainsKey(landmark.Id) && distances[landmark.Id] < closestDistance)
                {
                    closestDistance = distances[landmark.Id];
                }
            }

            return closestDistance == long.MaxValue ? -1 : (int)closestDistance;
        }

        public static bool IsFartherFromGorge(Landmark landmarkA, Landmark landmarkB)
        {
            // Calculate Euclidean distances
            double distanceToGorgeA = CalculateEuclideanDistance(landmarkA, new Landmark(0, 0, 0, false));
            double distanceToGorgeB = CalculateEuclideanDistance(landmarkB, new Landmark(0, 0, 0, false));

            // B is farther from the gorge than A
            return distanceToGorgeB > distanceToGorgeA;
        }

        public static double CalculateEuclideanDistance(Landmark landmark1, Landmark landmark2)
        {
            double deltaX = (double)landmark2.X - landmark1.X;
            double deltaY = (double)landmark2.Y - landmark1.Y;

            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}
