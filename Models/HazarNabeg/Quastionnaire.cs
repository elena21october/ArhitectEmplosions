namespace Models.HazarNabeg
{
    public class PointHN
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public int EmotionId { get; set; }
        public PointHN() { }
        public PointHN(double x, double y) { X = x; Y = y; }
    }
    public class Emotion
    {
        public int Id { get; set; }
        public string Color { get; set; } = null!;
        public List<PointHN> Points { get; set; } = new List<PointHN>();
        public int QuastionnaireId { get; set; }
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
        public Differentiation? Differentiation { get; set; }
        public string? PositiveComm { get; set; }
        public string? NegativeComm { get; set; }
        public string? NeutralComm { get; set; }
        public List<Emotion> Emotions { get; set; } = new List<Emotion>();
        public int HazarNabegId { get; set; }
        public HazarNabeg NazarNabeg { get; set; } = null!;
        public int hazarUserId { get; set; }
        public DateTime DateTime { get; set; }
    }
    public class ListQuest
    {
        public List<Quastionnaire>? Quastionnaires { get; set; }
    }
}
