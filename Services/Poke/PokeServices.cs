using Models.Poke;

namespace Services.Poke
{
    public static class PokeServices
    {
        
        public static double Distance(PointPoke p1, PointPoke p2)
        {
            double distance = Math.Sqrt(Math.Pow(Math.Abs(p1.X - p2.X), 2) +
                        Math.Pow(Math.Abs(p1.Y - p2.Y), 2));
            Console.WriteLine(distance);
            return distance;
        }
        public static List<PointPoke>? Conflict(List<PointPoke> points)
        {
            List<PointPoke> conflicts = new List<PointPoke>();
            for (int j = 0; j < points.Count - 1; j++)
            {
                for (int c = j + 1; c < points.Count; c++)
                {
                    if (Distance(points[j], points[c]) < 0.0022266776581074685)
                    {
                        double centerX = (points[j].X + points[c].X) / 2;
                        double centerY = (points[j].Y + points[c].Y) / 2;
                        conflicts.Add(new PointPoke(centerX, centerY, "#9C27B073"));
                    }
                }
            }
            if (conflicts.Count > 0)
            {
                return conflicts;
            }
            return null;
        }
    }
}
