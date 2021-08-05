using System;
using System.ComponentModel.DataAnnotations;

namespace ATS.BackEnd.WebAPI.DTOs
{
    public class CurriculoDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Faculdade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Curso { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string Conclusao { get; set; }

        public string CurriculoImagem { get; set; }
        public string ImagemUpload { get; set; }

        /* EF Relation */
        public Guid CandidatoId { get; set; }


    }
}
