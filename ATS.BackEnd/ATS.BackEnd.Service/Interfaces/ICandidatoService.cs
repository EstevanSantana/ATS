using ATS.BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATS.BackEnd.Service.Interfaces
{
    public interface ICandidatoService : IDisposable
    {
        Task<IEnumerable<Candidato>> Get();
        Task<Candidato> GetById(Guid id);
        Task<bool> Add(Candidato candidato);
        Task<bool> Remove(Guid id);
        Task<bool> RemoveEndereco(Guid id);
        Task<bool> RemoveCurriculo(Guid id);
        Task<bool> Update(Candidato candidato);
        Task<bool> UpdateEndereco(Endereco endereco);
        Task<bool> UpdateCurriculo(Curriculo curriculo);
        Task<Endereco> GetEnderecoById(Guid id);
        Task<Curriculo> GetCurriculoById(Guid id);
    }
}
