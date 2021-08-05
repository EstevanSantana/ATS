using ATS.BackEnd.Domain.Interfaces;
using ATS.BackEnd.Domain.Models;
using ATS.BackEnd.Infrastructure.Data.ContextConfig;

namespace ATS.BackEnd.Infrastructure.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(ApplicationDbContext context) : base(context) { }
    }
}
