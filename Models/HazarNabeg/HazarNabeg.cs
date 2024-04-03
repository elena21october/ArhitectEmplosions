using Newtonsoft.Json;

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
        public List<Emotion> PositiveEmotions { get; set; } = new List<Emotion>();
        public List<Emotion> NegativeEmotions { get; set; } = new List<Emotion>();
		public List<Emotion> NeutralEmotions { get; set; } = new List<Emotion>();
		public List<Emotion> ConflictEmotions { get; set; } = new List<Emotion>();
        public List<QuastionnairePoints>? Quastionnaires { get; set; }
        public HazarNabegData(HazarNabeg hazar)
        {
            Id = hazar.Id;
            Name = hazar.Name;
            X = hazar.X;
            Y = hazar.Y;
        }
        public void SetQuastionnaires(List<Quastionnaire> quastionnaires)
        {
            try
            {
                List<QuastionnairePoints> quastionnairepoints = new List<QuastionnairePoints>();
                for (int i = 0; i < quastionnaires.Count; i++)
                {
                    quastionnairepoints.Add(new QuastionnairePoints { 
                        EmotionsList = JsonConvert.DeserializeObject<List<Emotion>>(quastionnaires[i].Emotions!)!, 
                        Differentiation = JsonConvert.DeserializeObject<Differentiation>(quastionnaires[i].Differentiation!)! 
                    });
                }
                for (int i = 0; i < quastionnairepoints.Count; i++)
                {
                    PositiveEmotions.AddRange(quastionnairepoints[i].EmotionsList!.Where(p => p.Color == "#58FF008F"));
					NegativeEmotions.AddRange(quastionnairepoints[i].EmotionsList!.Where(p => p.Color == "#FF00008F"));
					NeutralEmotions.AddRange(quastionnairepoints[i].EmotionsList!.Where(p => p.Color == "#FFF200AB"));
                }
                Quastionnaires = quastionnairepoints;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
    public class TestHazar
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Value { get; set; }
    }
}
