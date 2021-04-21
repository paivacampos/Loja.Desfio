using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Desafio.DTO
{
    public class ProdutoDto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O {0} campo é obrigatório")]
        [StringLength(150, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
        public decimal ValorCompra { get; set; }

        
    }
}