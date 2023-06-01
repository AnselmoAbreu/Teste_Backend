using System.ComponentModel.DataAnnotations;

namespace Teste_Backend.Models
{

    public class Product
    {

        [Key]

        public int id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public decimal Preco { get; set; }

        public int Estoque { get; set; }
    }
}
