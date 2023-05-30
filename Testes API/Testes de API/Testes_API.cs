using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


namespace Testes_de_API
{
    public class Testes_API
    {
        private readonly HttpClient _client;

        public Testes_API()
        {
            // Configurar o HttpClient com o endereço base da sua API
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:44307/") // Substitua pela URL correta da sua API
            };
        }

        [Fact]
        public async Task Post_Endpoint_ReturnsSuccess()
        {
            // Arrange


            /* 
              
            nome": "Produto de teste 01",
            "descricao": "Produto de teste",
            "preco": 100,
            "estoque": 10 
            
             */

            var requestBody = new StringContent("{\"Nome\": \"Teste novo\", \"descricao\": \"Teste novo descricao\",\"preco\": 15 ,\"estoque\": 515}", System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/endpoint", requestBody);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        public void Dispose()
        {
            // Descartar o HttpClient após cada teste
            _client.Dispose();
        }
    }
}
