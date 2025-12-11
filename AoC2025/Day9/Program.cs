using System.Diagnostics;

namespace Day9 {
    public class Program {
        struct Point { 
            public int X; 
            public int Y; 
        }

        static bool IsRectangleValid(int minX, int maxX, int minY, int maxY, List<(Point p1, Point p2)> edges) {
            foreach (var edge in edges) {
                if (DoesEdgeIntersectRectangle(edge.p1, edge.p2, minX, maxX, minY, maxY)) {
                    return false;
                }
            }

            // czy prostokat calkowicie w srodku czy na zewnatrz
            double midX = (minX + maxX) / 2.0;
            double midY = (minY + maxY) / 2.0;

            return IsPointInPolygon(midX, midY, edges);
        }

        static bool DoesEdgeIntersectRectangle(Point e1, Point e2, int minX, int maxX, int minY, int maxY) {

            if (e1.X == e2.X) { // pionowo
                int x = e1.X;
                if (x > minX && x < maxX) {
                    int edgeMinY = Math.Min(e1.Y, e2.Y);
                    int edgeMaxY = Math.Max(e1.Y, e2.Y);

                    if (Math.Max(edgeMinY, minY) < Math.Min(edgeMaxY, maxY))
                        return true;
                }
            } else if (e1.Y == e2.Y) { // poziomo
                int y = e1.Y;
                if (y > minY && y < maxY) {
                    int edgeMinX = Math.Min(e1.X, e2.X);
                    int edgeMaxX = Math.Max(e1.X, e2.X);

                    if (Math.Max(edgeMinX, minX) < Math.Min(edgeMaxX, maxX))
                        return true;
                }
            }
            return false;
        }

        static bool IsPointInPolygon(double x, double y, List<(Point p1, Point p2)> edges) {
            bool inside = false;
            // raycast: parzysta ilosc przeciec - poza wielokatem, nieparzysta - w srodku
            foreach (var edge in edges) {
                double p1x = edge.p1.X, p1y = edge.p1.Y;
                double p2x = edge.p2.X, p2y = edge.p2.Y;

                // rowananie prostej przechodzacej przez 2 punkty (przeksztalcone na x, bo y znane)
                bool intersect = ((p1y > y) != (p2y > y)) &&
                                 (x < (p2x - p1x) * (y - p1y) / (p2y - p1y) + p1x);

                if (intersect)
                    inside = !inside;
            }
            return inside;
        }

        public static void Main() {

            var stopwatch = Stopwatch.StartNew();

            string filePath = @"C:\Users\sonia\Documents\GitHub\AdventOfCode2025\AoC2025\Day9\input.txt";

            if (!File.Exists(filePath))
                return;

            List<Point> redTiles = new List<Point>();

            foreach (string line in File.ReadAllLines(filePath)) {
                var parts = line.Split(',');
                redTiles.Add(new Point { 
                    X = int.Parse(parts[0]), 
                    Y = int.Parse(parts[1]) 
                });
            }
            long maxAreaPart1 = 0;

            for (int i = 0; i < redTiles.Count; i++) {
                for (int j = i + 1; j < redTiles.Count; j++) {
                    long width = Math.Abs(redTiles[i].X - redTiles[j].X) + 1;
                    long height = Math.Abs(redTiles[i].Y - redTiles[j].Y) + 1;
                    long area = width * height;

                    if (area > maxAreaPart1) maxAreaPart1 = area;
                }
            }
            Console.WriteLine($"Part 1: {maxAreaPart1}");



            // PART 2 --------------------------------------------------
            long maxAreaPart2 = 0;
            int n = redTiles.Count;

            var edges = new List<(Point p1, Point p2)>();
            for (int i = 0; i < n; i++) {
                edges.Add((redTiles[i], redTiles[(i + 1) % n]));
            }

            for (int i = 0; i < n; i++) {
                for (int j = i + 1; j < n; j++) {
                    Point p1 = redTiles[i];
                    Point p2 = redTiles[j];

                    long width = Math.Abs(p1.X - p2.X) + 1;
                    long height = Math.Abs(p1.Y - p2.Y) + 1;
                    long area = width * height;

                    if (area <= maxAreaPart2) // nie trzeba sprawdzac
                        continue;

                    int minX = Math.Min(p1.X, p2.X);
                    int maxX = Math.Max(p1.X, p2.X);
                    int minY = Math.Min(p1.Y, p2.Y);
                    int maxY = Math.Max(p1.Y, p2.Y);

                    if (IsRectangleValid(minX, maxX, minY, maxY, edges)) {
                        maxAreaPart2 = area;
                    }
                }
            }

            Console.WriteLine($"Part 2: {maxAreaPart2}");

            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds} ms");
        }
    }
}