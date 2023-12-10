namespace Models.HazarNabeg
{
    public class HazarNabeg
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public List<Quastionnaire>? Quastionnaires { get; set; } = null!;
    }
}
