namespace Models.HazarNabeg
{
    public class HazarNabeg
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
    public class HazarNabegData : HazarNabeg
    {
        public List<Emotion>? PositiveEmotions { get; set; }
        public List<Emotion>? NegativeEmotions { get; set; }
        public List<Emotion>? NeutralEmotions { get; set; }
        public List<Emotion>? ConflictEmotions { get; set; }
        public List<Quastionnaire>? Quastionnaires { get; set; }
    }
}
