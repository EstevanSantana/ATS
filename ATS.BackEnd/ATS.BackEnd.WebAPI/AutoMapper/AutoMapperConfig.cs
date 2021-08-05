using ATS.BackEnd.Domain.Models;
using ATS.BackEnd.WebAPI.DTOs;
using AutoMapper;

namespace ATS.BackEnd.WebAPI.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Candidato, CandidatoDTO>().ReverseMap();
            CreateMap<Endereco, EnderecoDTO>().ReverseMap();
            CreateMap<Curriculo, CurriculoDTO>().ReverseMap();
        }
    }
}
