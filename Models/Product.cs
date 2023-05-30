using System.ComponentModel.DataAnnotations;

namespace TesteEfx.Models
{ 

    public class Product
    {

        [Key]

        public int id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int Estoque { get; set; }
    }
}
