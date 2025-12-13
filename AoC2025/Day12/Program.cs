namespace Day12 {

    public class Program {

        public static void Main() {
            string filePath = @"C:\Users\sonia\Documents\GitHub\AdventOfCode2025\AoC2025\Day12\input.txt";

            if (!File.Exists(filePath))
                return;

            string[] lines = File.ReadAllLines(filePath);

            long result = 0;
            for (int i = 30; i < lines.Length; i++) {

                string[] parts = lines[i].Split(':');
                string[] dimensions = parts[0].Split("x");
                int x = int.Parse(dimensions[0]);
                int y = int.Parse(dimensions[1]);

                string[] indexes = parts[1].Trim().Split(' ');

                int sum = 0;
                foreach (string index in indexes) {
                    sum += int.Parse(index) * 7; // ~ sum of #
                }
                if (x * y >= sum)
                    result++;
            }
            Console.WriteLine(result);
        }
    }
}