using ATS.BackEnd.Domain.Interfaces;
using ATS.BackEnd.Domain.Models;
using ATS.BackEnd.Service.Interfaces;
using ATS.BackEnd.Service.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATS.BackEnd.Service.Services
{
    public class CandidatoService : BaseService, ICandidatoService
    {
        private readonly ICandidatoRepository _candidatoRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly ICurriculoRepository _curriculoRepository;

        public CandidatoService(ICandidatoRepository candidatoRepository,
                                IEnderecoRepository enderecoRepository,
                                ICurriculoRepository curriculoRepository,
                                IErrorMessageService errorMessage) : base(errorMessage)
        {
            _candidatoRepository = candidatoRepository;
            _enderecoRepository = enderecoRepository;
            _curriculoRepository = curriculoRepository;
        }

        public async Task<IEnumerable<Candidato>> Get()
        {
            try
            {
                return await _candidatoRepository.GetCandidato();
            }
            catch (Exception)
            { 
                throw new ArgumentException($"Error no services.");
            }
        }

        public async Task<Candidato> GetById(Guid id)
        {
            try
            {
                return await _candidatoRepository.GetCandidatoById(id);
            }
            catch (Exception)
            {
                throw new ArgumentException($"Error no services.");
            }
        }

        public async Task<bool> Remove(Guid id)
        {
            try
            {
                var candidato = await _candidatoRepository.GetById(id);

                if (candidato == null)
                {
                    return false;
                }

                await _candidatoRepository.Remove(candidato);
                return true;
            }
            catch (Exception)
            {
                throw new ArgumentException($"Error ao remover.");
            }
        }

        public async Task<bool> Update(Candidato candidato)
        {
            try
            {
                if (!RunValidation(new CandidatoValidation(), candidato) ||
                       !RunValidation(new EnderecoValidation(), candidato.Endereco) ||
                            !RunValidation(new CurriculoValidation(), candidato.Curriculo))
                                return false;

                await _candidatoRepository.Update(candidato);
                return true;
            }
            catch (Exception)
            {
                throw new ArgumentException($"Erro ao atualizar candidato!");
            }
        }

        public async Task<bool> Add(Candidato candidato)
        {
            try
            {
                if (!RunValidation(new CandidatoValidation(), candidato) ||
                        !RunValidation(new EnderecoValidation(), candidato.Endereco) ||
                            !RunValidation(new CurriculoValidation(), candidato.Curriculo))
                                return false;

                await _candidatoRepository.Add(candidato);
                return true;
            }
            catch (Exception)
            {
                throw new ArgumentException($"Erro ao adicionar candidato!");
            }
        }

        public void Dispose()
        {
            _candidatoRepository?.Dispose();
        }

        public async Task<Endereco> GetEnderecoById(Guid id)
        {
            try
            {
                return await _enderecoRepository.GetById(id);
            }
            catch (Exception)
            {
                throw new ArgumentException($"Error no services.");
            }
        }
        
        public async Task<Curriculo> GetCurriculoById(Guid id)
        {
            try
            {
                return await _curriculoRepository.GetById(id);
            }
            catch (Exception)
            {
                throw new ArgumentException($"Error no services.");
            }
        }

        public async Task<bool> UpdateEndereco(Endereco endereco)
        {
            try
            {
                if (!RunValidation(new EnderecoValidation(), endereco)) return false;

                await _enderecoRepository.Update(endereco);
                return true;
            }
            catch (Exception)
            {
                throw new ArgumentException($"Erro ao atualizar endereço!");
            }
        }

        public async Task<bool> RemoveEndereco(Guid id)
        {
            try
            {
                var endereco = await GetEnderecoById(id);
                if (endereco == null)
                {
                    return false;
                }

                await _enderecoRepository.Remove(endereco);
                return true;
            }
            catch (Exception)
            {
                throw new ArgumentException($"Error ao remover.");
            }
        }
        
        public async Task<bool> RemoveCurriculo(Guid id)
        {
            try
            {
                var curriculo = await GetCurriculoById(id);
                if (curriculo == null)
                {
                    return false;
                }

                await _curriculoRepository.Remove(curriculo);
                return true;
            }
            catch (Exception)
            {
                throw new ArgumentException($"Error ao remover.");
            }
        }

        public async Task<bool> UpdateCurriculo(Curriculo curriculo)
        {
            try
            {
                if (!RunValidation(new CurriculoValidation(), curriculo)) return false;

                await _curriculoRepository.Update(curriculo);
                return true;
            }
            catch (Exception)
            {
                throw new ArgumentException($"Erro ao atualizar curriculo!");
            }
        }
    }
}
