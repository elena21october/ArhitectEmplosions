using ArhitectEmplosions.Database;

namespace ArhitectEmplosions.Services
{
    public class OverlapService
    {
        private readonly ApplicationContext _context;
        public OverlapService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task GetOverlapGeometryAsync()
        {
            throw new NotImplementedException();
        }
    }
}
