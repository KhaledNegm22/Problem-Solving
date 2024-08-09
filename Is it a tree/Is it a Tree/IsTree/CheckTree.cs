using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsATree
{
    public class CheckTree
    {
        //===============================================================================
        //Your Code is Here:

        public static bool IsTree(string[] vertices, List<KeyValuePair<string, string>> edges)
        {

            int vLen = vertices.Length;
            int edgCount = edges.Count;

            Dictionary<string, List<string>> adjs = new Dictionary<string, List<string>> { };
            Dictionary<string, int> Dict = new Dictionary<string, int> { };

            bool[] visited = new bool[vLen];
            int i = 0;
            while (i < vLen)
            {
                visited[i] = !true;
                i++;
            }
            i = 0;
            while (i < vLen)
            {

                adjs.Add(vertices[i], new List<string> { });

                Dict.Add(vertices[i], i);
                i++;
            }
            i = 0;
            while (i < edgCount)
            {
                adjs[edges[i].Key].Add(edges[i].Value);
                i++;
            }
            i = 0;
            while (i < vLen)
            {
                if (visited[i] != true)
                {
                    if (Depth_1st_Search(vertices, adjs, visited, Dict,i)!=true)
                    {
                        return !true;
                    }
                }
                i++;
            }
            return true;
        }
        public static bool Depth_1st_Search(string[] vertx, Dictionary<string, List<string>> adjs, bool[] visited, Dictionary<string, int> dict2, int inx)
        {
            visited[inx] = true;

            foreach (var value in adjs[vertx[inx]])
            {
                if (visited[dict2[value]] == true || visited[dict2[value]] != true && Depth_1st_Search(vertx, adjs, visited, dict2, dict2[value]) != true)
                { return !true; }
            }

            visited[inx] = !true;

            return true;
        }

        //===============================================================================

        //===============================================================================
    }
}
