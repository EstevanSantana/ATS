using ATS.BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATS.BackEnd.Domain.Interfaces
{
    public interface ICandidatoRepository : IRepository<Candidato>
    {
        Task<Candidato> GetCandidatoById(Guid id);
        Task<IEnumerable<Candidato>> GetCandidato();
    }
}
