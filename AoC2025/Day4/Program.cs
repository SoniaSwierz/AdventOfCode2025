namespace Day4 {

    internal class Program {

        public static bool IsWithinBorders(int x, int y, int linesNumber, int lineLength) {
            return x >= 0 && x < linesNumber && y >= 0 && y < lineLength;
        }

        public static int MarkRollsToDelete(char[][] grid) {

            int[][] DIRECTIONS = {
                new int[] {-1, -1}, new int[] {-1, 0}, new int[] {-1, 1},
                new int[] {0, -1},  new int[] {0, 1},
                new int[] {1, -1},  new int[] {1, 0},  new int[] {1, 1}
            };

            int result = 0;

            int linesNumber = grid.Length;
            int lineLength = grid[0].Length;

            for (int i = 0; i < linesNumber; i++) {
                for (int j = 0; j < lineLength; j++) {

                    int paperRolls = 0;
                    if (grid[i][j] == '@') {

                        foreach (var dir in DIRECTIONS) {
                            int x = i + dir[0];
                            int y = j + dir[1];
                            
                            if (IsWithinBorders(x, y, linesNumber, lineLength)) {
                                if (grid[x][y] == '@' || grid[x][y] == 'X') {
                                    paperRolls++;
                                }
                            }
                        }
                        if (paperRolls < 4) {
                            result++;
                            grid[i][j] = 'X';
                        }
                    }
                }
            }
            return result;
        }

        public static void RemoveRolls(char[][] grid) {
            int linesNumber = grid.Length;
            int lineLength = grid[0].Length;

            for (int i = 0; i < linesNumber; i++) {
                for (int j = 0; j < lineLength; j++) {
                    if (grid[i][j] == 'X')
                        grid[i][j] = '.';
                }
            }
        }

        public static void Main() {

            string filePath = @"C:\Users\sonia\Documents\GitHub\AdventOfCode2025\AoC2025\Day4\input.txt";

            if (!File.Exists(filePath))
                return;

            char[][] grid = File.ReadAllLines(filePath)
                    .Select(line => line.ToCharArray())
                    .ToArray();

            int result = 0;
            int iterationResult = 0;
            bool part1 = true;

            do {
                iterationResult = MarkRollsToDelete(grid);
                if (part1) {
                    Console.WriteLine($"Part 1: {iterationResult}");
                    part1 = false;
                }
                result += iterationResult;

                RemoveRolls(grid);

            } while (iterationResult > 0);

            Console.WriteLine($"Part 2: {result}");
        }
    }
}