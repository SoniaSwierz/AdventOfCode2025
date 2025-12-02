namespace Day2 {

    internal class Program {

        public static HashSet<ulong> uniqueNumbers = new HashSet<ulong>();

        public static string MakeString(long number, int times) {
            string result = "";
            for(int i = 0; i < times; i++)
                result += number;

            return result;
        }

        public static void CountInvalidIDsInRange(string leftString, string rightString, int times) {

            long leftRange = long.Parse(leftString);
            long rightRange = long.Parse(rightString);

            int left = int.Parse(leftString.Substring(0, (int)Math.Ceiling((double)(leftString.Length) / times)));
            int right = int.Parse(rightString.Substring(0, (int)Math.Ceiling((double)(rightString.Length) / times)));

            if (times % 2 == 1 && left > right)
                (left, right) = (right, left);
            else if (left > right)
                left /= 10;

            string numberString;
            long number;
            
            for (int i = left; i <= right; i++) {
                numberString = MakeString(i, times);
                number = long.Parse(numberString);
                
                if (number <= rightRange && number >= leftRange) {
                    uniqueNumbers.Add((ulong)number);
                    //Console.WriteLine(number + " " + leftRange + " " + rightRange);
                }
            }
        }

        public static void Solve() {
            uniqueNumbers.Clear();
            long resultPart1 = 0;
            long resultPart2 = 0;

            string file = @"C:\Users\sonia\Documents\GitHub\AdventOfCode2025\AoC2025\Day2\input.txt";

            using (StreamReader streamReader = new StreamReader(file)) {
                string line = streamReader.ReadLine();

                if (line == null)
                    return;

                string[] ranges = line.Split(',');
                foreach (string range in ranges) {
                    uniqueNumbers.Clear();

                    string[] parts = range.Split('-');

                    int size = parts[1].Length;

                    // PART 1 SOLUTION -----------------------
                    CountInvalidIDsInRange(parts[0], parts[1], 2);
                    foreach (long number in uniqueNumbers) {
                        resultPart1 += number;
                    }

                    // PART 2 SOLUTION -----------------------
                    for (int i = 3; i <= size; i++) {
                        CountInvalidIDsInRange(parts[0], parts[1], i);
                    }

                    foreach (long number in uniqueNumbers) {
                        resultPart2 += number;
                    }
                }
                Console.WriteLine($"Part 1: {resultPart1}");
                Console.WriteLine($"Part 2: {resultPart2}");
            }
        }

        public static void Main() {
            Solve();
        }
    }
}