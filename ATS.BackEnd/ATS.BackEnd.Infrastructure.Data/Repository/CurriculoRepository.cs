using ATS.BackEnd.Domain.Interfaces;
using ATS.BackEnd.Domain.Models;
using ATS.BackEnd.Infrastructure.Data.ContextConfig;

namespace ATS.BackEnd.Infrastructure.Data.Repository
{
    public class CurriculoRepository : Repository<Curriculo>, ICurriculoRepository
    {
        public CurriculoRepository(ApplicationDbContext context) : base(context) { }
    }
}
