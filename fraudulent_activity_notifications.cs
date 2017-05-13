// 9 5
// 2 3 4 2 3 6 8 4 5


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Solution {
    public static void Main(String[] args) {
        // test();

        string[] nd = Console.ReadLine().Split(' ');
        int n = int.Parse(nd[0]);
        int d = int.Parse(nd[1]);
        List<int> expenses = Console.ReadLine().Split(' ').Select<string, int>(s => int.Parse(s)).ToList();
        Console.WriteLine(getNumAlerts(expenses, d));
    }

    private static void test() {
        List<int> nums = new List<int>() { 2 };
        Console.WriteLine("Adding {0}", 3);
        binaryAdd(nums, 3);
        Console.WriteLine("{{ {0} }}", string.Join(", ", nums));
        Console.WriteLine("Adding {0}", 1);
        binaryAdd(nums, 1);
        Console.WriteLine("{{ {0} }}", string.Join(", ", nums));
        Console.WriteLine("Adding {0}", 7);
        binaryAdd(nums, 7);
        Console.WriteLine("{{ {0} }}", string.Join(", ", nums));
        // Console.WriteLine("Adding {0}", 4);
        // binaryAdd(nums, 4);
        // Console.WriteLine("{{ {0} }}", string.Join(", ", nums));
        Console.WriteLine();

        Console.WriteLine("median is {0}", getMedian(nums));

        // Console.WriteLine("{{ {0} }}", string.Join(", ", nums));
        // Console.WriteLine("removing {0}", 1);
        // binaryRemove(nums, 1);
        // Console.WriteLine("{{ {0} }}", string.Join(", ", nums));
        // Console.WriteLine("removing {0}", 3);
        // binaryRemove(nums, 3);
        // Console.WriteLine("{{ {0} }}", string.Join(", ", nums));
        // Console.WriteLine("removing {0}", 4);
        // binaryRemove(nums, 4);
        // Console.WriteLine("{{ {0} }}", string.Join(", ", nums));
    }

    private static int getNumAlerts(List<int> expenses, int d) {
        if (expenses.Count <= d) {
            return 0;
        }
        List<int> history = new List<int>(d);
        for (int i = 0; i < d; i++) {
            binaryAdd(history, expenses[i]);
        }
        int noti = 0;
        for (int i = d; i < expenses.Count; i++) {
            double median = getMedian(history);
            if (expenses[i] >= median * 2) {
                noti++;
            }
            binaryRemove(history, expenses[i - d]);
            binaryAdd(history, expenses[i]);
        }
        return noti;
    }

    private static double getMedian(List<int> sortedNums) {
        int mid = sortedNums.Count / 2;
        if (sortedNums.Count % 2 == 0) {
            return (sortedNums[mid] + sortedNums[mid - 1]) / 2.0;
        } else {
            return sortedNums[mid];
        }
    }

    private static void binaryAdd(List<int> sortedNums, int x) {
        if (sortedNums.Count == 0) {
            sortedNums.Add(x);
            return;
        }
        binaryAddAux(sortedNums, x, 0, sortedNums.Count - 1);
    }

    private static void binaryAddAux(List<int> sortedNums, int x, int start, int end) {
        if (start == end) {
            if (x < sortedNums[start]) {
                sortedNums.Insert(start, x);
            } else {
                sortedNums.Insert(start + 1, x);
            }
            return;
        }

        // Console.WriteLine("adding {0} between {1} and {2}", x, start, end);
        int mid = start + ((end - start) / 2);
        // Console.WriteLine("mid is {0}", mid);
        if (x < sortedNums[mid]) {
            binaryAddAux(sortedNums, x, start, mid);
        } else {
            binaryAddAux(sortedNums, x, mid + 1, end);
        }
    }

    private static void binaryRemove(List<int> sortedNums, int x) {
        binaryRemoveAux(sortedNums, x, 0, sortedNums.Count - 1);
    }

    private static void binaryRemoveAux(List<int> sortedNums, int x, int start, int end) {
        if (start == end) {
            sortedNums.RemoveAt(start);
            return;
        }

        int mid = start + ((end - start) / 2);
        if (sortedNums[mid] == x) {
            sortedNums.RemoveAt(mid);
            return;
        } else if (x < sortedNums[mid]) {
            binaryRemoveAux(sortedNums, x, start, mid - 1);
        } else {
            binaryRemoveAux(sortedNums, x, mid + 1, end);
        }
    }
}
