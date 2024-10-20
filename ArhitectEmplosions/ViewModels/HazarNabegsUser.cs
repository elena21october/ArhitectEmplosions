using ArhitectEmplosions.Models.HazarNabeg;

namespace ArhitectEmplosions.ViewModels
{
    public class HazarNabegsUser
    {
        public HazarNabegsUser()
        {
            HazarNabegs = new List<HazarNabeg>();
            Role = string.Empty;
        }
        public List<HazarNabeg> HazarNabegs { get; set; }
        public string Role { get; set; }
    }
}
