using ATS.BackEnd.Domain.Interfaces;
using ATS.BackEnd.Domain.Models;
using ATS.BackEnd.Infrastructure.Data.ContextConfig;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATS.BackEnd.Infrastructure.Data.Repository
{
    public class CandidatoRepository : Repository<Candidato>, ICandidatoRepository
    {
        public CandidatoRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Candidato>> GetCandidato()
        {
            return await _context.Candidatos
                                 .AsNoTracking()
                                 .Include(x => x.Endereco)
                                 .Include(x => x.Curriculo)
                                 .ToListAsync();
        }

        public async Task<Candidato> GetCandidatoById(Guid id)
        {
            return await _context.Candidatos
                                 .AsNoTracking()
                                 .Include(x => x.Curriculo)
                                 .Include(x => x.Endereco)
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
