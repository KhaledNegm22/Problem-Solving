using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange
{
    // *****************************************
    // DON"T CHANGE CLASS NAME OR FUNCTION NAME
    // *****************************************
    public static class CurrencyExchange
    {
        //Your Code is Here:
        //==================
        /// <summary>
        /// find the index of the USD Dollar $ with the smallest number of questions.
        /// </summary>
        /// <param name="N">Number of customers (N)</param>
        /// <param name="M">Number of currencies (M)</param>
        /// <param name="knows">N*M Matrix indicating whether customer i knows currency j or not</param>
        /// <returns>index of US Dollar</returns>
        public static int CheckUSD(int N, int M, bool[,] knows)
        {
            //knows[P,C]=true if person P knows currency C and knows[P,C]=false if person P doesn't know Currency C.
            int j = 0;
            int res = 0;
            int inx=0;
               for (int i = 0; i < M; i++)//rows
               {
                   if (knows[j, i] == true && res == 0)
                   {
                       res ++;
                       inx = i;
                   }
                   else if (knows[j, i] == true && res == 1)
                   {
                    j++;
                    res = 0;
                    i = -1;
                    inx = 0;
                    continue;
                    }
                   if (i == M - 1 && res == 1)
                   {
                       return inx;
                   }
                   if (i == M - 1 && j < N - 1)
                   {
                       j++;
                       res = 0;
                       i = -1;
                       inx = 0;
                   }
               }
        
            return -1;
        }
    }
}

