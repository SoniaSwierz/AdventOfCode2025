namespace Day1 { 

    public class Program {

        public static void Main() {
            string filePath = @"C:\Users\sonia\Documents\GitHub\AdventOfCode2025\AoC2025\AoC2025\input.txt";

            if (!File.Exists(filePath))
                return;

            int currentNumber = 50;
            int finalResult = 0;

            string[] lines = File.ReadAllLines(filePath);
            int bonusTimes = 0;
            int bonusSum = 0;

            foreach (string line in lines) {
                int modifyValue = int.Parse(line.Substring(1));
                bonusTimes = 0;

                if (line[0] == 'R') {
                    currentNumber += modifyValue;

                    bonusTimes += currentNumber / 100;
                        
                    currentNumber %= 100;

                    if (currentNumber == 0) {
                        finalResult++;
                        bonusTimes--;
                    }

                } else {
                    if (currentNumber == 0)
                        bonusTimes--;

                    bonusTimes += (modifyValue / 100);
                    modifyValue %= 100;

                    currentNumber -= modifyValue;
    
                    if (currentNumber == 0)
                        finalResult++;

                    if(currentNumber < 0) {
                        currentNumber += 100;
                        bonusTimes++;
                    }

                }
                bonusSum += bonusTimes;
            }
            Console.WriteLine(finalResult);
            Console.WriteLine(finalResult + bonusSum);
        }
    }
}