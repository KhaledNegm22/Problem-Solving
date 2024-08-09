using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // ***************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // ***************
    public static class PROBLEM_CLASS
    {
        #region YOUR CODE IS HERE 

        //Your Code is Here:
        //==================
        /// <summary>
        /// get the minimum number of coins that is greater that the remaining coins within an array.
        /// </summary>
        /// <param name="arr">array of coin values </param>
        /// <returns>min number of coins to take</returns>
        static public int RequiredFuntion(int[] arr)
        {
            return MinNumberOfCoins(arr);
        }

        public static int MinNumberOfCoins(int[] arr)
        {
            Array.Sort(arr);
            long sum = 0;

            long totalSum = 0;
            foreach (int number in arr)
            {
                totalSum += number;
            }

            long targetSum = totalSum / 2;

            long sumTaken = 0;
            long numCoinsTaken = 0;

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                sumTaken += arr[i];
                numCoinsTaken++;

                if (sumTaken > targetSum)
                {
                    break;
                }
            }

            return (int)numCoinsTaken;
        }
        public static void MergeSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int mid = (low + high) / 2;
                MergeSort(arr, low, mid);
                MergeSort(arr, mid + 1, high);
                Merge(arr, low, mid, high);
            }
        }

        public static void Merge(int[] arr, int low, int mid, int high)
        {
            int n1 = mid - low + 1;
            int n2 = high - mid;

            int[] leftArr = new int[n1];
            int[] rightArr = new int[n2];

            Array.Copy(arr, low, leftArr, 0, n1);
            Array.Copy(arr, mid + 1, rightArr, 0, n2);

            int i = 0, j = 0;
            int k = low;

            while (i < n1 && j < n2)
            {
                if (leftArr[i] >= rightArr[j])
                {
                    arr[k] = leftArr[i];
                    i++;
                }
                else
                {
                    arr[k] = rightArr[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                arr[k] = leftArr[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                arr[k] = rightArr[j];
                j++;
                k++;
            }
        }


        #endregion
    }
}