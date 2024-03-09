using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Poke
{
    public class PointPoke
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string? Color { get; set; }
        public string? Name { get; set; }
        public double Radius { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }

        public PointPoke() { }
        public PointPoke(double x, double y, string clr)
        {
            X = x;
            Y = y;
            Color = clr;
        }
    }
    public class EmotionPoke
    {
        public List<PointPoke> PositivePoints { get; set; } = null!;
        public List<PointPoke> NegativePoints { get; set; } = null!;
        public List<PointPoke> NeutralPoints { get; set; } = null!;
        public List<PointPoke> ConflictPoints { get; set; } = null!;
    }
    public class UserPoke
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<PointPoke> Points { get; set; } = new();
        public UserPoke() { }
        public UserPoke(int id, string name, List<PointPoke> points)
        {
            Id = id;
            Name = name;
            Points = points;
        }
        public override string ToString()
        {
            return $"id = {Id} Name = {Name} Points = {Points.Count}";
        }
    }
    public class TestPoke
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Value { get; set; }
    }
}
