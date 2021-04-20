using System;
using System.ComponentModel.DataAnnotations;

namespace API.Desafio.DTO
{
    public class EstoqueDto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O {0} campo é obrigatório")]
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "O {0} campo é obrigatório")]
        public int LojaId { get; set; }
        [Required(ErrorMessage = "O {0} campo é obrigatório")]
        public int Quantidade { get; set; }
        [Required(ErrorMessage = "O {0} campo é obrigatório")]
        public DateTime UltimaAtualizacao { get; set; }
    }
}