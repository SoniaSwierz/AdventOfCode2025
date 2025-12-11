namespace Day11 {

    public class Program {

        public static int counter = 0;

        static Dictionary<(string, bool, bool), long> cache = new Dictionary<(string, bool, bool), long>();

        public static void SolvePart1(string node, Dictionary<string, List<string>> graph) {
            foreach (var output in graph[node]) {
                if (output == "out") { // node -> out
                    counter++;
                    break;
                } else {
                    SolvePart1(output, graph);
                }
            }
        }

        public static long SolvePart2(string node, Dictionary<string, List<string>> graph, bool hasFft, bool hasDac) {

            bool currentFft = hasFft || (node == "fft");
            bool currentDac = hasDac || (node == "dac");

            var cacheKey = (node, currentFft, currentDac);
            if (cache.ContainsKey(cacheKey)) {
                return cache[cacheKey];
            }

            if (node == "out") {
                return (currentFft && currentDac) ? 1 : 0;
            }

            long count = 0;
            foreach (var child in graph[node]) {
                count += SolvePart2(child, graph, currentFft, currentDac);
            }

            cache[cacheKey] = count;

            return count;
        }

        public static void Main() {
            string filePath = @"C:\Users\sonia\Documents\GitHub\AdventOfCode2025\AoC2025\Day11\input.txt";

            if (!File.Exists(filePath))
                return;

            string[] lines = File.ReadAllLines(filePath);

            Dictionary<string, List<string>> devicesAndOutputs = new Dictionary<string, List<string>>();
            foreach (string line in lines) {
                string lineModifed = line;
                string key = line.Substring(0, line.IndexOf(':'));
                lineModifed = line.Substring(line.IndexOf(':') + 1);
                devicesAndOutputs.Add(key, lineModifed.Trim().Split(' ').ToList());
            }

            SolvePart1("you", devicesAndOutputs);
            Console.WriteLine($"Part1: {counter}");

            long validPaths = SolvePart2("svr", devicesAndOutputs, false, false);
            Console.WriteLine($"Part2: {validPaths}");
        }
    }
}