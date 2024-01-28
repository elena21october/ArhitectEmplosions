using Models.HazarNabeg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HazarNabeg
{
    public static class EmotionsIntersection
    {
        public static Emotion FindIntersection(Emotion firstEmotion, Emotion secondEmotion)
        {
            Emotion result = new Emotion();
            result.Color = "#9300FF8F";
            for (int i = 0; i < secondEmotion.Points.Count; i++)
            {
                if (PointIntersection(firstEmotion, secondEmotion.Points[i]))
                {
                    result.Points.Add(firstEmotion.Points[i]);
                }
            }
            return result;
        }
        private static bool PointIntersection(Emotion emotion, PointHN point)
        {
            if (point.X >= emotion.Points[2].X && point.X <= emotion.Points[0].X && point.Y >= emotion.Points[1].Y && point.Y <= emotion.Points[3].Y)
            {
                return true;
            }
            return false;
        }
    }
}
