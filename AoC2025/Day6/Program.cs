namespace Day6 {
    internal class Program {

        public static string GetNextNumber(string[] lines, int lineNumber, bool modify) {
            int index = 0;
            string result = "";
            foreach (char character in lines[lineNumber]) {
                if (character == ' ' && result != "") {
                    if(modify)
                        lines[lineNumber] = lines[lineNumber].Substring(index + 1);

                    break;
                }
                else if (character != ' ')
                    result += character;

                index++;
            }
            return result;
        }


        public static ulong SolvePart1(string[] lines) {

            ulong result = 0;
            bool add = false;

            string operation = GetNextNumber(lines, lines.Length - 1, true);
            if (operation == "*")
                result = 1;
            else if(operation == "+")
                add = true;

            for (int i = 0; i < lines.Length - 1; i++) {
                string current = GetNextNumber(lines, i, true);

                ulong number = 0;
                if (ulong.TryParse(current, out number)) {
                    if (add)
                        result += number;
                    else
                        result *= number;
                }
            }

            return result;
        }

        public static string GetNextNumberWithSize(string[] lines, int lineNumber, int size) {
            string result = lines[lineNumber].Substring(0, size);
            if (lines[lineNumber].Length > size)
                lines[lineNumber] = lines[lineNumber].Substring(size + 1);
            else
                lines[lineNumber] = "";

            return result;
        }

        public static ulong SolvePart2(string[] lines) {
            ulong result = 0;
            bool add = false;

            string operation = GetNextNumber(lines, lines.Length - 1, true);
            if (operation == "*")
                result = 1;
            else if (operation == "+")
                add = true;

            List<string> numbers = new List<string>();
            int maxSize = 0;
            for (int i = 0; i < lines.Length - 1; i++) {
                string current = GetNextNumber(lines, i, false);
                numbers.Add(current);

                if(current.Trim().Length > maxSize)
                    maxSize = current.Trim().Length;
            }

            string[] numbersCorrectSize = new string[numbers.Count];
            for (int i = 0; i < numbers.Count; i++) {
                numbersCorrectSize[i] = GetNextNumberWithSize(lines, i, maxSize);
            }

            // NEW NUMBERS CREATION ---------------------------------
            ulong[] numberArray = new ulong[maxSize];
            int index = 0;
            for (int i = 0; i < maxSize; i++) {
                string newNumber = "";
                for (int j = 0; j < numbers.Count; j++) {
                    newNumber += numbersCorrectSize[j][i];
                }
                numberArray[index] = ulong.Parse(newNumber);
                index++;
            }

            for(int i = 0; i < maxSize; i++) {
                if(add)
                    result += numberArray[i];
                else
                    result *= numberArray[i];
            }

            return result;
        }

        public static void Main() {

            string filePath = @"C:\Users\sonia\Documents\GitHub\AdventOfCode2025\AoC2025\Day6\input.txt";

            if (!File.Exists(filePath))
                return;

            string[] lines = File.ReadAllLines(filePath);

            ulong result = 0;
            ulong toAdd = 0;
            do {
                toAdd = SolvePart1(lines);
                result += toAdd;
            } while (toAdd > 0);

            Console.WriteLine($"Part 1: {result}");

            lines = File.ReadAllLines(filePath);
            result = 0;
            toAdd = 0;
            do {
                toAdd = SolvePart2(lines);
                result += toAdd;
            } while (toAdd > 0);

            Console.WriteLine($"Part 2: {result}");
        }
    }
}