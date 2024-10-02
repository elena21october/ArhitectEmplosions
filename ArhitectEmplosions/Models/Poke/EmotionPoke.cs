using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;

namespace ArhitectEmplosions.Models.Poke
{
    public class PointPoke
    {
        public double X { get; set; }
        public double Y { get; set; }
        public string? Color { get; set; }
        public string? Name { get; set; }
        public double Radius { get; set; }
        public DateTime CreationDate { get; set; }
    }

    public class PokeData
    {
        public List<PointPoke> PositivePoints { get; set; } = null!;
        public List<PointPoke> NegativePoints { get; set; } = null!;
        public List<PointPoke> NeutralPoints { get; set; } = null!;
        public List<PointPoke> ConflictPoints { get; set; } = null!;
        public PokeData()
        {
            PositivePoints = new List<PointPoke>();
            NegativePoints = new List<PointPoke>();
            NeutralPoints = new List<PointPoke>();
            ConflictPoints = new List<PointPoke>();
        }
        public bool SetPoints(List<EmotionPoke> users)
        {
            try
            {
                foreach (var item in users)
                {
                    if (string.IsNullOrEmpty(item.Points))
                    { 
                        List<PointPoke> pointPokes = JsonConvert.DeserializeObject<List<PointPoke>>(item.Points!)!;
                        PositivePoints.AddRange(pointPokes.Where(p => p.Color == "#58FF008F"));
                        NegativePoints.AddRange(pointPokes.Where(p => p.Color == "#FF00008F"));
                        NeutralPoints.AddRange(pointPokes.Where(p => p.Color == "#FFF200AB"));
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    public class EmotionPoke
    {
        public int Id { get; set; }
        public string? Points { get; set; }
        public EmotionPoke() { }
        public EmotionPoke(string points)
        {
            Points = points;
        }
    }
}
