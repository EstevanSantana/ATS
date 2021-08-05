using ATS.BackEnd.Domain.Models;
using ATS.BackEnd.Service.Interfaces;
using ATS.BackEnd.WebAPI.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ATS.BackEnd.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatoController : BaseController
    {
        private readonly ICandidatoService _candidatoService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CandidatoController(ICandidatoService candidatoService,
                                   IMapper mapper,
                                   IWebHostEnvironment env,
                                   IErrorMessageService errorMessage) : base(errorMessage)
        {
            _candidatoService = candidatoService;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidatoDTO>>> Get()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<CandidatoDTO>>(await _candidatoService.Get()));
            }
            catch (Exception ex)
            {
                if (ex.Message == "Error no services.")
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Atenção! Houve um erro na conexão.");
                }

                return StatusCode(StatusCodes.Status500InternalServerError,
                           "Atenção! Caso esse erro persista, entre em contato com o suporte.");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CandidatoDTO>> Get(Guid id)
        {
            try
            {
                var candidato = await GetCandidato(id);
                if (candidato == null) 
                    return BadRequest(new { success = false, errors = "Atenção! Informe um Id valido." });

                return Ok(_mapper.Map<CandidatoDTO>(await _candidatoService.GetById(id)));
            }
            catch (Exception ex)
            {
                if (ex.Message == "Error no services.")
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Atenção! Houve um erro na conexão.");
                }

                return StatusCode(StatusCodes.Status500InternalServerError,
                            "Atenção! Caso esse erro persista, entre em contato com o suporte.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CandidatoDTO>> Post([FromBody] CandidatoDTO candidatoDTO)
        {
            try
            {
                if (!ModelState.IsValid) return BaseView(ModelState);

                var imagem = Guid.NewGuid() + "_" + candidatoDTO.Curriculo.CurriculoImagem;
                if (!UploadCurriculo(candidatoDTO.Curriculo.ImagemUpload, imagem))
                {
                    return BaseView(candidatoDTO);
                }

                candidatoDTO.Curriculo.CurriculoImagem = imagem;

                await _candidatoService.Add(_mapper.Map<Candidato>(candidatoDTO));
                return BaseView(candidatoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            "Atenção! Caso esse erro persista, entre em contato com o suporte.");
            }
        }

        // PUT api/<CandidatoController>/5
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CandidatoDTO>> Put(Guid id, [FromBody] CandidatoDTO candidatoDTO)
        {
            try
            {
                if (id != candidatoDTO.Id)
                {
                    NotifyError("Atenção! Os ids são inferentes.");
                    return BaseView();
                }

                var candidato = await GetCandidato(id);
                if (!ModelState.IsValid) return BaseView(ModelState);

                if (candidatoDTO.Curriculo.ImagemUpload != null)
                {
                    var imagem = Guid.NewGuid() + "_" + candidatoDTO.Curriculo.CurriculoImagem;
                    if (!UploadCurriculo(candidatoDTO.Curriculo.ImagemUpload, imagem))
                    {
                        return BaseView(candidatoDTO);
                    }

                    candidatoDTO.Curriculo.CurriculoImagem = imagem;
                }
                else
                {
                    candidatoDTO.Curriculo.CurriculoImagem = candidato.Curriculo.CurriculoImagem;
                }

                candidato.Cpf = candidatoDTO.Cpf;
                candidato.DataNascimento = candidatoDTO.DataNascimento;
                candidato.Nome = candidatoDTO.Nome;
                candidato.SobreNome = candidatoDTO.SobreNome;
                candidato.Telefone = candidatoDTO.Telefone;
                
                candidato.Endereco.Bairro = candidatoDTO.Endereco.Bairro;
                candidato.Endereco.Cep = candidatoDTO.Endereco.Cep;
                candidato.Endereco.Cidade = candidatoDTO.Endereco.Cidade;
                candidato.Endereco.Estado = candidatoDTO.Endereco.Estado;
                candidato.Endereco.Numero = candidatoDTO.Endereco.Numero;
                candidato.Endereco.Rua = candidatoDTO.Endereco.Rua;

                candidato.Curriculo.Conclusao = candidatoDTO.Curriculo.Conclusao;
                candidato.Curriculo.CurriculoImagem = candidatoDTO.Curriculo.CurriculoImagem;
                candidato.Curriculo.ImagemUpload = candidatoDTO.Curriculo.ImagemUpload;
                candidato.Curriculo.Curso = candidatoDTO.Curriculo.Curso;
                candidato.Curriculo.Faculdade = candidatoDTO.Curriculo.Faculdade;

                await _candidatoService.Update(_mapper.Map<Candidato>(candidato));
                return BaseView(candidatoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                           "Atenção! Caso esse erro persista, entre em contato com o suporte.");
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CandidatoDTO>> Delete(Guid id)
        {
            try
            {
                var candidato = await GetCandidato(id);
                if (candidato == null)
                {
                    NotifyError("O id informado não existe na base de dados!");
                    return BaseView(candidato);
                }
                
                await _candidatoService.Remove(id);
                return BaseView(candidato);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                           "Atenção! Caso esse erro persista, entre em contato com o suporte.");
            }
        }
        
        [HttpDelete("endereco/{id:guid}")]
        public async Task<ActionResult<CandidatoDTO>> DeleteEndereco(Guid id)
        {
            try
            {
                var endereco = await _candidatoService.GetEnderecoById(id);
                if (endereco == null)
                {
                    NotifyError("O id informado não existe na base de dados!");
                    return BaseView(endereco);
                }
                
                await _candidatoService.RemoveEndereco(id);
                return BaseView(endereco);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                           "Atenção! Caso esse erro persista, entre em contato com o suporte.");
            }
        }
        
        [HttpDelete("curriculo/{id:guid}")]
        public async Task<ActionResult<CandidatoDTO>> DeleteCurriculo(Guid id)
        {
            try
            {
                var curriculo = await _candidatoService.GetCurriculoById(id);
                if (curriculo == null)
                {
                    NotifyError("O id informado não existe na base de dados!");
                    return BaseView(curriculo);
                }
                
                await _candidatoService.RemoveCurriculo(id);
                return BaseView(curriculo);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                           "Atenção! Caso esse erro persista, entre em contato com o suporte.");
            }
        }

        [HttpPut("endereco/{id:guid}")]
        public async Task<ActionResult<EnderecoDTO>> UpdateEndereco(Guid id, [FromBody] EnderecoDTO enderecoDTO)
        {
            try
            {
                if (id != enderecoDTO.Id)
                {
                    NotifyError("Os ids informado são diferentes!");
                    return BaseView(enderecoDTO);
                }

                if (!ModelState.IsValid) return BaseView(ModelState);

                await _candidatoService.UpdateEndereco(_mapper.Map<Endereco>(enderecoDTO));
                return BaseView(enderecoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                           "Atenção! Caso esse erro persista, entre em contato com o suporte.");
            }
        }
        
        [HttpPut("curriculo/{id:guid}")]
        public async Task<ActionResult<EnderecoDTO>> UpdateCurriculo(Guid id, [FromBody] CurriculoDTO curriculoDTO)
        {
            try
            {
                if (id != curriculoDTO.Id)
                {
                    NotifyError("Os ids informado são diferentes!");
                    return BaseView(curriculoDTO);
                }

                if (!ModelState.IsValid) return BaseView(ModelState);

                await _candidatoService.UpdateCurriculo(_mapper.Map<Curriculo>(curriculoDTO));
                return BaseView(curriculoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                           "Atenção! Caso esse erro persista, entre em contato com o suporte.");
            }
        }

        private bool UploadCurriculo(string file, string imagem)
        {
            if (string.IsNullOrEmpty(file))
            {
                NotifyError("Por favor, inserir um documento de curriculo");
                return false;
            }
            var byteArray = Convert.FromBase64String(file);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", imagem);

            System.IO.File.WriteAllBytesAsync(filePath, byteArray);
            return true;
        }

        private async Task<CandidatoDTO> GetCandidato(Guid id)
        {
            return _mapper.Map<CandidatoDTO>(await _candidatoService.GetById(id));
        }
    }
}
