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
        public List<PointHN> Points { get; set; } = null!;
        public int QuastionnaireId { get; set; }
    }
    public class Differentiation
    {
        public string Gender { get; set; } = null!;
        public string Place { get; set; } = null!;
        public string Visiting { get; set; } = null!;
        public string Age { get; set; } = null!;

    }
    public class Quastionnaire
    {
        public int Id { get; set; }
        public Differentiation Differentiation { get; set; } = null!;
        public string PositiveComm { get; set; } = null!;
        public string NegativeComm { get; set; } = null!;
        public string NeutralComm { get; set; } = null!;
        public List<Emotion> Emotions { get; set; } = null!;
        public int HazarNabegId { get; set; }
        public HazarNabeg NazarNabeg { get; set; } = null!;
        public DateTime DateTime { get; set; }

    }
}
