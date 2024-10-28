using ArhitectEmplosions.Models;
using ArhitectEmplosions.Models.HazarNabeg;

namespace ArhitectEmplosions.ViewModels
{
    public class HazarUsers
    {
        public HazarUsers()
        {
            Users = new List<User>();
        }
        public int HazarNabegsId { get; set; }
        public List<User> Users { get; set; }
    }
}
