using System.Diagnostics;
using System.Runtime.CompilerServices;
using System;

namespace MyApplication
{
    class SortingAlgorithms
    {
        public int[] _unsortedArr;

        public SortingAlgorithms()
        {
            AddRandomElements(10000);
        }

        public void AddRandomElements(int size)
        {
            var array = new int[size];
            var rand = new Random();
            var maxNum = 10000;
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(maxNum + 1);

                this._unsortedArr = array;
            }
        }

        public int[] BubbleSort(int[] array) 
        {
            int length = array.Length;

            int temp = array[0];

            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    if (array[i] > array[j])
                    {
                        temp = array[i];

                        array[i] = array[j];

                        array[j] = temp;
                    }
                }
            }

            return array;
        }

        public int[] SelectionSort(int[] _unsortedArr)
        {
            var arrayLength = _unsortedArr.Length;

            for (int i = 0; i < arrayLength - 1; i++)
            {
                var smallestVal = i;

            for (int j = i + 1; j < arrayLength; j++)
            {
                if (_unsortedArr[j] < _unsortedArr[smallestVal])
                {
                    smallestVal = j;
                }
            }

            var tempVar = _unsortedArr[smallestVal];
            _unsortedArr[smallestVal] = _unsortedArr[i];
            _unsortedArr[i] = tempVar;
            }
            return _unsortedArr;
        }

        public int[] QuickSort(int[] array, int leftIndex, int rightIndex)
        {
            var i = leftIndex;
            var j = rightIndex;
            var pivot = array[leftIndex];
            while (i <= j)
            {
                while (array[i] < pivot)
                {
                    i++;
                }

                while (array[j] > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
                QuickSort(array, leftIndex, j);
            if (i < rightIndex)
                QuickSort(array, i, rightIndex);
            return array;
        }

        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        // To check if array is sorted or not 
        public static bool isSorted(int[] a, int n)
        {
            int i = 0;
            while (i < n - 1)
            {
                if (a[i] > a[i + 1])
                    return false;
                i++;
            }
            return true;
        }

        // To generate permutation of the array 
        public static void shuffle(int[] a, int n)
        {
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
                Swap(ref a[i], ref a[rnd.Next(0, n)]);
        }

        // Sorts array a[0..n-1] using Bogo sort 
        public static void bogosort(int[] a, int n)
        {
            // if array is not sorted then shuffle 
            // the array again 
            while (!isSorted(a, n))
                shuffle(a, n);
        }

        // prints the array 
        public static void printArray(int[] a, int n)
        {
            for (int i = 0; i < n; i++)
                Console.Write(a[i] + " ");
            Console.Write("\n");
        }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            const int iterations = 100;
            const int arraySize = 10000;
            SortingAlgorithms sortingAlgorithms = new SortingAlgorithms();
            Stopwatch sw = new Stopwatch();
            TimeSpan totalBubbleSortTime = TimeSpan.Zero;
            TimeSpan totalSelectionSortTime = TimeSpan.Zero;
            TimeSpan totalQuickSortTime = TimeSpan.Zero;
            TimeSpan minBubbleSortTime = TimeSpan.MaxValue;
            TimeSpan minSelectionSortTime = TimeSpan.MaxValue;
            TimeSpan minQuickSortTime = TimeSpan.MaxValue;
            TimeSpan maxBubbleSortTime = TimeSpan.Zero;
            TimeSpan maxSelectionSortTime = TimeSpan.Zero;
            TimeSpan maxQuickSortTime = TimeSpan.Zero;
            
            for (int i = 0; i < iterations; i++)
            {
                sortingAlgorithms.AddRandomElements(arraySize);

                sw.Start();
                sortingAlgorithms.BubbleSort((int[])sortingAlgorithms._unsortedArr.Clone());
                sw.Stop();
                TimeSpan ts = sw.Elapsed;
                totalBubbleSortTime += ts;
                if (ts < minBubbleSortTime) minBubbleSortTime = ts;
                if (ts > maxBubbleSortTime) maxBubbleSortTime = ts;
                sw.Reset();

                sortingAlgorithms.AddRandomElements(arraySize);

                sw.Start();
                sortingAlgorithms.SelectionSort((int[])sortingAlgorithms._unsortedArr.Clone());
                sw.Stop();
                ts = sw.Elapsed;
                totalSelectionSortTime += ts;
                if (ts < minSelectionSortTime) minSelectionSortTime = ts;
                if (ts > maxSelectionSortTime) maxSelectionSortTime = ts;
                sw.Reset();

                sortingAlgorithms.AddRandomElements(arraySize);

                sw.Start();
                sortingAlgorithms.QuickSort((int[])sortingAlgorithms._unsortedArr.Clone(), 0, sortingAlgorithms._unsortedArr.Length - 1);
                sw.Stop();
                ts = sw.Elapsed;
                totalQuickSortTime += ts;
                if (ts < minQuickSortTime) minQuickSortTime = ts;
                if (ts > maxQuickSortTime) maxQuickSortTime = ts;
                sw.Reset();
            }

            Console.WriteLine($"Bubble sort - Avg: {totalBubbleSortTime.TotalMilliseconds / iterations} ms, Min: {minBubbleSortTime.TotalMilliseconds} ms, Max: {maxBubbleSortTime.TotalMilliseconds} ms");
            Console.WriteLine($"Selection sort - Avg: {totalSelectionSortTime.TotalMilliseconds / iterations} ms, Min: {minSelectionSortTime.TotalMilliseconds} ms, Max: {maxSelectionSortTime.TotalMilliseconds} ms");
            Console.WriteLine($"Quick sort - Avg: {totalQuickSortTime.TotalMilliseconds / iterations} ms, Min: {minQuickSortTime.TotalMilliseconds} ms, Max: {maxQuickSortTime.TotalMilliseconds} ms");
        }
    }
}