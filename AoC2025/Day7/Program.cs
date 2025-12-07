using System.Diagnostics;

namespace Day7 {

    public class Program {
        public static void Draw(char[][] grid, ulong[][] result) {
            foreach (char[] row in grid) {
                foreach (char symbol in row) {
                    Console.Write(symbol);
                }
                Console.WriteLine();
            }
        }

        public static int SolvePart1(char[][] grid) {

            int splitters = 0;
            for (int i = 1; i < grid.Length; i++) {
                for (int j = 0; j < grid[i].Length; j++) {

                    if (grid[i - 1][j] == 'S' || grid[i - 1][j] == '|') {
                        if (grid[i][j] == '^') {
                            grid[i][j - 1] = '|';
                            grid[i][j + 1] = '|';
                            splitters++;
                        }
                        else grid[i][j] = '|';
                    }
                }
            }
            return splitters;
        }

        public static ulong SolvePart2(char[][] grid) {

            ulong[,] resultArray = new ulong[grid.Length, grid[0].Length];

            for (int i = 1; i < grid.Length; i++) {
                for (int j = 0; j < grid[i].Length; j++) {

                    if (grid[i - 1][j] != '^' && grid[i - 1][j] != '.') {

                        if (grid[i - 1][j] == 'S'){
                            resultArray[i, j] = 1;
                            grid[i][j] = '|';
                        }
                        else if (grid[i][j] == '^') {

                            // rewriting the result from above
                            if (grid[i - 1][j + 1] == '|' && grid[i][j + 1] == '.') {
                                resultArray[i, j + 1] = resultArray[i - 1, j + 1];
                                grid[i][j + 1] = '|';
                            }

                            // left split
                            resultArray[i, j - 1] = resultArray[i, j - 1] + resultArray[i - 1, j];
                            grid[i][j - 1] = '|';

                            // right split
                            resultArray[i, j + 1] = resultArray[i - 1, j] + resultArray[i, j + 1];
                            grid[i][j + 1] = '|';

                        } else if (grid[i][j] == '.'){
                            grid[i][j] = grid[i - 1][j];
                            resultArray[i, j] = resultArray[i - 1, j];
                        }
                    }
                }
            }
            int last = grid.Length - 1;
            ulong result = 0;
            for(int i = 0; i < grid[0].Length; i++) {
                result += resultArray[last, i];
            }
            return result;
        }

        public static void Main() {

            var stopwatch = Stopwatch.StartNew();

            string filePath = @"C:\Users\sonia\Documents\GitHub\AdventOfCode2025\AoC2025\Day7\input.txt";

            if (!File.Exists(filePath))
                return;

            // PART 1 ---------------------------------------------
            char[][] grid = File.ReadAllLines(filePath)
                    .Select(line => line.ToCharArray())
                    .ToArray();

            Console.WriteLine("Part 1: " + SolvePart1(grid));

            // PART 2 ---------------------------------------------
            grid = File.ReadAllLines(filePath)
                    .Select(line => line.ToCharArray())
                    .ToArray();

            Console.WriteLine("Part 2: " + SolvePart2(grid));

            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds} ms");
        }
    }
}