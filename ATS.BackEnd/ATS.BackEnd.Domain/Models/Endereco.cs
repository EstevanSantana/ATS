using System;

namespace ATS.BackEnd.Domain.Models
{
    public class Endereco : Entity
    {
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        /* EF Relation */
        public Candidato Candidato { get; set; }
        public Guid CandidatoId { get; set; }
    }
}
