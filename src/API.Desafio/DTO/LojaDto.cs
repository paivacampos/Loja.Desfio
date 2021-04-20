using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Desafio.DTO
{
    public class LojaDto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O {0} campo é obrigatório")]
        [StringLength(255, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
        public string NomeFantasia { get; set; }
        [Required(ErrorMessage = "O {0} campo é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter  {1} caracteres")]
        public string Cnpj { get; set; }
        [Required(ErrorMessage = "O {0} campo é obrigatório")]
        [StringLength(8, ErrorMessage = "O campo {0} precisa ter  {1} caracteres")]
        public string Cep { get; set; }
        [Required(ErrorMessage = "O {0} campo é obrigatório")]
        [StringLength(255, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "O {0} campo é obrigatório")]
        [StringLength(10, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Numero { get; set; }
        public string Complemento { get; set; }
        [Required(ErrorMessage = "O {0} campo é obrigatório")]
        [StringLength(150, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "O {0} campo é obrigatório")]
        [StringLength(10, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "O {0} campo é obrigatório")]
        [StringLength(2, ErrorMessage = "O campo {0} precisa ter  {1} caracteres")]
        public string Uf { get; set; }

        public IEnumerable<EstoqueDto> EstoqueList { get; set; }
    }
}
