using System.Diagnostics;

namespace Day7 {

    public class Program {

        public static double countDistance(int[,] coordinates, int point1, int point2) {
            long x = Math.Abs(coordinates[point2, 0] - coordinates[point1, 0]);
            long y = Math.Abs(coordinates[point2, 1] - coordinates[point1, 1]);
            long z = Math.Abs(coordinates[point2, 2] - coordinates[point1, 2]);

            return Math.Sqrt((x*x) + (y*y) + (z*z));
        }

        public static long MultiplyThreeBiggest(HashSet<int>[] sets) {
            long result = 1;

            var top3 = sets
                .Select(s => s.Count)
                .OrderByDescending(x => x)
                .Take(3)
                .ToList();

            foreach (var size in top3)
                result *= size;

            return result;
        }

        public static void MakeSetsAndCoordinates(string[] lines, HashSet<int>[] sets, int[,] coordinates) {

            int numbersInFile = lines.Length;

            for (int i = 0; i < numbersInFile; i++) {
                string[] numbers = lines[i].Split(',');

                for (int j = 0; j < numbers.Length; j++) {
                    coordinates[i, j] = int.Parse(numbers[j]);
                }

                sets[i] = new HashSet<int> { i };
            }
        }

        public static void MakeEdges(int numbersInFile, List<(int p1, int p2, double dist)> edges, int[,] coordinates) {
            
            for (int i = 0; i < numbersInFile; i++) {
                for (int j = i + 1; j < numbersInFile; j++) {
                    double d = countDistance(coordinates, i, j);
                    edges.Add((i, j, d));
                }
            }
        }
        public static void Main() {

            var stopwatch = Stopwatch.StartNew();

            string filePath = @"C:\Users\sonia\Documents\GitHub\AdventOfCode2025\AoC2025\Day8\input.txt";

            if (!File.Exists(filePath))
                return;

            string[] lines = File.ReadAllLines(filePath);
            int numbersInFile = lines.Length;


            HashSet<int>[] sets = new HashSet<int>[numbersInFile];
            int[,] coordinates = new int[numbersInFile, 3];
            MakeSetsAndCoordinates(lines, sets, coordinates);

            var edges = new List<(int p1, int p2, double dist)>();
            MakeEdges(numbersInFile, edges, coordinates);

            int[] ownerMap = new int[numbersInFile];
            for (int i = 0; i < numbersInFile; i++)
                ownerMap[i] = i;

            var edgesByDistance = edges.OrderBy(e => e.dist).ToList();

            long x1 = 0;
            long x2 = 0;
            int counter = 0;
            foreach (var edge in edgesByDistance) {

                if (counter++ == 1000) {
                    Console.WriteLine($"Part 1: {MultiplyThreeBiggest(sets)}");
                }

                int root1 = ownerMap[edge.p1];
                int root2 = ownerMap[edge.p2];

                if (root1 == root2) 
                    continue;

                sets[root1].UnionWith(sets[root2]);

                foreach (int member in sets[root2]) {
                    ownerMap[member] = root1;
                }

                sets[root2].Clear();

                x1 = coordinates[edge.p1, 0];
                x2 = coordinates[edge.p2, 0];
            }
            Console.WriteLine($"Part 2: {(x1 * x2)}");

            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds} ms");
        }
    }
}