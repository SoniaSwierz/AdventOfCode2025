namespace Day3 {

    internal class Program {

        public static void Solve(string[] lines, int numberCount) {
            ulong result = 0;

            foreach (string line in lines) {
                char[] maxValues = Enumerable.Repeat('0', numberCount).ToArray();

                int lineSize = line.Length;

                int prevFinish = 0;
                for (int i = 0; i < numberCount; i++) {
                    for (int j = prevFinish; j < lineSize - numberCount + 1 + i; j++) {
                        if (line[j] > maxValues[i]) {
                            maxValues[i] = line[j];
                            prevFinish = j + 1;
                        }
                    }
                }

                string resultString = new string(maxValues);
                result += ulong.Parse(resultString);
            }

            Console.WriteLine(result);
        }

        public static void Main() {

            string filePath = @"C:\Users\sonia\Documents\GitHub\AdventOfCode2025\AoC2025\Day3\input.txt";

            if (!File.Exists(filePath))
                return;

            string[] lines = File.ReadAllLines(filePath);

            Solve(lines, 2);
            Solve(lines, 12);
        }
    }
}