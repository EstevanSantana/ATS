using System;

namespace ATS.BackEnd.Domain.Models
{
    public class Candidato : Entity
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }


        /* EF Relation */
        public Curriculo Curriculo { get; set; }
        public Endereco Endereco { get; set; }
    }
}
