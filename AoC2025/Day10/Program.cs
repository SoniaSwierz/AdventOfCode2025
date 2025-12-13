namespace Day10 {

    public class Program {

        public static string ReadPattern(string line, bool[] pattern, ref int currentIndex) {

            while (true) {
                if (line[currentIndex] == ']')
                    return line.Substring(currentIndex + 2);
                else if (line[currentIndex] == '#') {
                    pattern[currentIndex - 1] = true;
                }
                currentIndex++;
            }
        }

        public static int FindCombination(int buttonsToClick, int startNode, List<int> currentCombination, List<bool[]> buttons, bool[] targetPattern) {
            if (currentCombination.Count == buttonsToClick) {
                bool[] currentState = new bool[targetPattern.Length];

                foreach (int buttonIndex in currentCombination) {
                    bool[] button = buttons[buttonIndex];
                    for (int i = 0; i < currentState.Length; i++) {
                        if (button[i]) {
                            currentState[i] = !currentState[i]; // xor
                        }
                    }
                }

                bool isMatch = true;
                for (int i = 0; i < targetPattern.Length; i++) {
                    if (currentState[i] != targetPattern[i]) {
                        isMatch = false;
                        break;
                    }
                }

                if (isMatch) {
                    int result = currentCombination.Count;
                    return result;
                }

                return 0;
            }

            for (int i = startNode; i < buttons.Count; i++) {
                currentCombination.Add(i);
                int result = FindCombination(buttonsToClick, i + 1, currentCombination, buttons, targetPattern);
                if (result > 0)
                    return result;

                currentCombination.RemoveAt(currentCombination.Count - 1);
            }

            return 0;
        }
        public static void Main() {
            int finalResult = 0;
            string filePath = @"C:\Users\sonia\Documents\GitHub\AdventOfCode2025\AoC2025\Day10\input.txt";

            if (!File.Exists(filePath))
                return;

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines) {

                bool[] pattern = new bool[20];
                int currentIndex = 1;
                string remainingLine = ReadPattern(line, pattern, ref currentIndex);

                int patternLength = currentIndex - 1;
                bool[] finalPattern = new bool[patternLength];
                Array.Copy(pattern, finalPattern, patternLength);

                string[] parts = remainingLine.Split(' ');
                List<bool[]> buttons = new List<bool[]>();

                foreach (string part in parts) {
                    if (part.StartsWith("{"))
                        break;

                    string cleanPart = part.Replace("(", "").Replace(")", "");

                    bool[] button = new bool[patternLength];

                    var indices = cleanPart.Split(',');
                    foreach (var indexStr in indices) {
                        if (int.TryParse(indexStr, out int index)) {
                            if (index < patternLength)
                                button[index] = true;
                        }
                    }
                    buttons.Add(button);
                }

                int result = 0;
                for (int i = 1; i <= buttons.Count; i++) {
                    result = FindCombination(i, 0, new List<int>(), buttons, finalPattern);
                    if (result > 0) {
                        break;
                    }
                }
                finalResult += result;
            }
            Console.Write($"Part 1: {finalResult}");
        }
    }
}