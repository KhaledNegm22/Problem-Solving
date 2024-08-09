using System;
using System.Threading.Tasks;

namespace Problem
{
    public static class PROBLEM_CLASS
    {
        #region YOUR CODE IS HERE 

        //Your Code is Here:
        //==================
        /// <summary>
        /// Sort the given array in ascending order
        /// At least, should beat the default sorting algorithm of the C# (Array.Sort())
        /// </summary>
        /// <param name="arr"> array to be sorted in ascending order </param>
        /// <param name="N"> array size </param>
        /// <returns> sorted array </returns>
        static public float[] RequiredFuntion(float[] arr, int N)
        {
            var sortHelper = new MergeSortHelper();
            sortHelper.Sort(arr, N);
            return arr;
        }
        #endregion
    }

    // Define the MergeSortHelper class
    public class MergeSortHelper
    {
        public void Sort(float[] array, int N)
        {
            var copy = (float[])array.Clone();
            ParallelMergeSort(array, copy, 0, N - 1);
        }

        private void ParallelMergeSort(float[] to, float[] temp, int low, int high)
        {
            const int SEQUENTIAL_THRESHOLD = 2048;

            if (high - low + 1 <= SEQUENTIAL_THRESHOLD)
            {
                SequentialMergeSort(to, temp, low, high);
                return;
            }

            var mid = (low + high) / 2;
            Parallel.Invoke(
                () => ParallelMergeSort(temp, to, low, mid),
                () => ParallelMergeSort(temp, to, mid + 1, high)
                );
            ParallelSequentialMerge(to, temp, low, mid, mid + 1, high, low);
        }

        private void SequentialMergeSort(float[] to, float[] temp, int low, int high)
        {
            if (low >= high)
                return;
            var mid = (low + high) / 2;
            SequentialMergeSort(temp, to, low, mid);
            SequentialMergeSort(temp, to, mid + 1, high);
            SequentialMerge(to, temp, low, mid, mid + 1, high, low);
        }

        private void SequentialMerge(float[] to, float[] temp, int lowX, int highX, int lowY, int highY, int lowTo)
        {
            var highTo = lowTo + highX - lowX + highY - lowY + 1;
            for (; lowTo <= highTo; lowTo++)
            {
                if (lowX > highX)
                    to[lowTo] = temp[lowY++];
                else if (lowY > highY)
                    to[lowTo] = temp[lowX++];
                else
                    to[lowTo] = temp[lowX] < temp[lowY] ? temp[lowX++] : temp[lowY++];
            }
        }

        private void ParallelSequentialMerge(float[] to, float[] temp, int lowX, int highX, int lowY, int highY, int lowTo)
        {
            var highTo = lowTo + highX - lowX + highY - lowY + 1;
            int index = lowTo;
            while (lowX <= highX && lowY <= highY)
            {
                if (temp[lowX] < temp[lowY])
                    to[index++] = temp[lowX++];
                else
                    to[index++] = temp[lowY++];
            }

            while (lowX <= highX)
                to[index++] = temp[lowX++];

            while (lowY <= highY)
                to[index++] = temp[lowY++];
        }
    }
}
