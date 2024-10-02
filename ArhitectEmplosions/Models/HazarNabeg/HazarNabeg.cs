using Newtonsoft.Json;

namespace ArhitectEmplosions.Models.HazarNabeg
{
    public class HazarNabeg
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public HazarNabeg() { }
        public HazarNabeg(string? name, double x, double y)
        {
            Name = name;
            X = x;
            Y = y;
        }
    }
    public class HazarNabegData : HazarNabeg
    {
        public List<Emotion> PositiveEmotions { get; set; } = new List<Emotion>();
        public List<Emotion> NegativeEmotions { get; set; } = new List<Emotion>();
        public List<Emotion> NeutralEmotions { get; set; } = new List<Emotion>();
        public HazarNabegData(HazarNabeg hazar)
        {
            Id = hazar.Id;
            Name = hazar.Name;
            X = hazar.X;
            Y = hazar.Y;
        }
        public bool SetQuastionnaires(List<Quastionnaire> quastionnaires)
        {
            try
            {
                List<QuastionnairePoints> quastionnairepoints = new List<QuastionnairePoints>();
                for (int i = 0; i < quastionnaires.Count; i++)
                {
                    try
                    {
                        quastionnairepoints.Add(new QuastionnairePoints
                        {
                            EmotionsList = JsonConvert.DeserializeObject<List<Emotion>>(quastionnaires[i].Emotions!)!,
                            Differentiation = JsonConvert.DeserializeObject<Differentiation>(quastionnaires[i].Differentiation!)!
                        });
                    }
                    catch
                    {
                        continue;
                    }
                }
                for (int i = 0; i < quastionnairepoints.Count; i++)
                {
                    PositiveEmotions.AddRange(quastionnairepoints[i].EmotionsList!.Where(p => p.Color == "#58FF008F"));
                    NegativeEmotions.AddRange(quastionnairepoints[i].EmotionsList!.Where(p => p.Color == "#FF00008F"));
                    NeutralEmotions.AddRange(quastionnairepoints[i].EmotionsList!.Where(p => p.Color == "#FFF200AB"));
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;

        }
    }
    
}
