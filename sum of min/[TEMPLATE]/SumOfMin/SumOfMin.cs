using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    public static class SumOfMin
    {
        #region YOUR CODE IS HERE
        /// <summary>
        /// create visited array 
        /// create adj list
        /// implement dfs to find min value for each component 
        ///  
        /// </summary>
        /// <param name="valuesOfVertices"></param>
        /// <param name="edges"></param>
        /// <returns></returns>
        /// 
        static char WHITE = 'w', GREY ='g';
        static char[] visited ;
        static List<int>[] myList;
        public static int dfs(int vertex, int[] valuesOfVertices)
        {
            int current_min = valuesOfVertices[vertex];
            visited[vertex] = GREY;
           
            for (int i = 0; i < myList[vertex].Count; i++)
            {
                int child = myList[vertex][i];
                if (visited[child]==WHITE)
                {
                    current_min = Math.Min(current_min, dfs(child, valuesOfVertices));
                }

            }

            return current_min;
        }
        public static List<int> [] create_adjlist(KeyValuePair<int, int>[] edges)
        { 
            for (int i =0;i < edges.Length; i++)
            {
                int u, v;
                u= edges[i].Key;
                v= edges[i].Value;
          
                myList[u].Add(v);
                myList[v].Add(u);

            }

            return myList;
        }
    public static int CalcSumOfMinInComps(int[] valuesOfVertices, KeyValuePair<int, int>[] edges)
        {
            int SumOfMini = 0;
           
            // create visited array intialized with white 
            int k = valuesOfVertices.Length;
            visited = new char[k];
            myList = new List<int>[k];
            for (int i = 0; i < k; i++)
            {
                visited[i]= WHITE;
                myList[i] = new List<int>();
            }
            create_adjlist(edges);

           for (int vert = 0;vert < k; vert++)
            {
                if (visited[vert] == WHITE)
                {
                    SumOfMini += dfs(vert, valuesOfVertices);
                }
            }
           return SumOfMini;
        }
  
        #endregion
    }
}
