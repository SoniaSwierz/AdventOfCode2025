namespace Day5 {

    internal class Program {

        public static Dictionary<ulong, ulong> Merge(string[] lines, out int secondPartIndex) {

            Dictionary<ulong, ulong> map = new Dictionary<ulong, ulong>();

            int index = 0;
            foreach (string line in lines) {
                index++;

                if (line == "")
                    break;

                string[] parts = line.Split('-');
                ulong leftRange = ulong.Parse(parts[0]);
                ulong rightRange = ulong.Parse(parts[1]);

                if (!map.TryAdd(leftRange, rightRange)) {
                    if (map[leftRange] < rightRange)
                        map[leftRange] = rightRange;
                }
            }
            secondPartIndex = index + 1;

            map = map.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

            foreach (ulong leftRange in map.Keys) {
                foreach (ulong leftRange2 in map.Keys) {
                    if (leftRange >= leftRange2)
                        continue;

                    if(map.ContainsKey(leftRange)) {
                        if (map[leftRange] >= leftRange2) {

                            if (map[leftRange] <= map[leftRange2])
                                map[leftRange] = map[leftRange2];

                            map.Remove(leftRange2);
                        }
                    }
                }
            }

            ulong result = 0;
            foreach (ulong leftRange in map.Keys) {
                result += map[leftRange] - leftRange + 1;
            }

            Console.WriteLine($"Part 2: {result}");
            return map;
        }

        public static void Main() {

            string filePath = @"C:\Users\sonia\Documents\GitHub\AdventOfCode2025\AoC2025\Day5\input.txt";

            if (!File.Exists(filePath))
                return;

            string[] lines = File.ReadAllLines(filePath);


            // RANGES PART
            int secondPartIndex = 0;
            Dictionary<ulong, ulong> map = Merge(lines, out secondPartIndex);


            // PRODUCTS PART 
            int goodProducts = 0;
            for (int i = secondPartIndex; i < lines.Length; i++) {

                ulong product = ulong.Parse(lines[i]);
                foreach (ulong leftRange in map.Keys) {
                    if (product >= leftRange && product <= map[leftRange]) {
                        goodProducts++;
                        break;
                    }
                }
            }

            Console.WriteLine($"Part 1: {goodProducts}");
        }
    }
}