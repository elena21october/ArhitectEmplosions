using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Models.HazarNabeg
{
    public class PointHN
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
    public class Emotion
    {
        public string Color { get; set; } = null!;
        public List<PointHN> Points { get; set; } = new List<PointHN>();
    }
    public class Differentiation
    {
        public int Id { get; set; }
        public string Gender { get; set; } = "";
        public string Place { get; set; } = "";
        public string Visiting { get; set; } = "";
        public string Age { get; set; } = "";
        public int QuastionnaireId { get; set; }

    }
    public class Quastionnaire
    {
        public int Id { get; set; }
        public string? Differentiation { get; set; }
        public string? Comm { get; set; }
        public string? Emotions { get; set; } 
        public int HazarNabegId { get; set; }
        public HazarNabeg NazarNabeg { get; set; } = null!;
        public int hazarUserId { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class QuastionnairePoints
    {
        public List<Emotion>? EmotionsList { get; set; }
    }
    public class ListQuest
    {
        public List<Quastionnaire>? Quastionnaires { get; set; }
    }
}
