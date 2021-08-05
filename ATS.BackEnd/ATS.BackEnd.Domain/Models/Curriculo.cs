using System;

namespace ATS.BackEnd.Domain.Models
{
    public class Curriculo : Entity
    {
        public string Faculdade { get; set; }
        public string Curso { get; set; }
        public string Conclusao { get; set; }
        public string CurriculoImagem { get; set; }

        /* EF Relation */
        public Candidato Candidato { get; set; }
        public Guid CandidatoId { get; set; }
    }
}
