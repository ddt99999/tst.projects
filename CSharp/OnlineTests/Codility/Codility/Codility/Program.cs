using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility
{
    class Program
    {
        public static List<List<int>> listPermutations(List<int> list)
        {

            if (list.Count == 0)
            {
                List<List<int>> result = new List<List<int>>();
                result.Add(new List<int>());
                return result;
            }

            List<List<int>> returnMe = new List<List<int>>();

            int firstElement = list[0];
            list.RemoveAt(0);

            List<List<int>> recursiveReturn = listPermutations(list);
            foreach (List<int> li in recursiveReturn)
            {

                for (int index = 0; index <= li.Count; index++)
                {
                    List<int> temp = new List<int>(li) {firstElement};
                    returnMe.Add(temp);
                }

            }
            return returnMe;
        }

        public static void Permutation(<List<int> arr)
        {
            int time = 0;
            Permutation(new List<int>(), arr, ref time);
        }

        private static int minTime = int.MaxValue;

        private static void Permutation(List<int> prefix, List<int> arr, ref int time)
        {
            int n = arr.Count;
            if (n == 0)
            {
                time += prefix.Count;

                if (time < minTime)
                    minTime = time;
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    var element = arr[i];
                    arr.RemoveAt(i);

                    Permutation(prefix, arr, ref time);
                }
            }     
        }


        static void Main(string[] args)
        {
            var A = new List<int> {100, 250, 1000};
            
            // Permutation(A);



        }
    }
}