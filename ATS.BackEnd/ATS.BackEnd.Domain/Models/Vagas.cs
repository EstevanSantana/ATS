using System;

namespace ATS.BackEnd.Domain.Models
{
    public class Vagas : Entity
    {
        public int Total { get; set; }
        public DateTime Data { get; set; }

        /* EF Relation */
        public Candidato Candidato { get; set; }
        public Guid CandidatoId { get; set; }
    }
}
