using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TesteEfx.Data;
using TesteEfx.Models;

namespace TesteEfx.AddControllers
{

    [ApiController]
    [Route("v1/Produtos")]
    public class ProdutoController : ControllerBase
    {
        [HttpPost]
        [Route("lerarquivo")]
        public async Task<IActionResult> LerArquivoAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length <= 0)
                    return BadRequest("Nenhum arquivo foi enviado.");

                if (!file.FileName.ToUpper().EndsWith(".CSV"))
                    return BadRequest("O arquivo enviado não é um arquivo CSV válido.");
                var resultado = "";
                int indice = 0;
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        string[] colunas = line.Split(',');
                        indice = 0;
                        var modelo = new Product();
                        foreach (string column in colunas)
                        {
                            resultado = column.Replace("\"", "");
                            if (indice == 0)
                                modelo.Nome = resultado;
                            if (indice == 1)
                                modelo.Descricao = resultado;
                            if (indice == 2)
                                modelo.Preco = Convert.ToDecimal(resultado);
                            if (indice == 3)
                                modelo.Estoque = Convert.ToInt32(resultado);
                            indice++;
                        }
                        if (modelo != null)
                        {
                            DataContext dataContext = new DataContext("Database");
                            dataContext.Products.Add(modelo);
                            await dataContext.SaveChangesAsync();
                        }
                    }
                }

                return Ok("Dados gravados com sucesso.");
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post([FromServices] DataContext context
        , [FromBody] Product model)
        {
            if (ModelState.IsValid)
            {
                context.Products.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            var produtos = await context.Products.OrderBy(i => i.Preco).ToListAsync();
            return produtos;
        }

        [HttpGet]
        [Route("id:int")]
        public async Task<ActionResult<List<Product>>> GetById([FromServices] DataContext context, int id)
        {
            var produtos = await context.Products.AsNoTracking().Where(x => x.id == id).ToListAsync();
            return produtos;
        }

        [HttpGet]
        [Route("listarporpreco")]
        public async Task<ActionResult<List<Product>>> ListarPorPreco([FromServices] DataContext context, [FromQuery] decimal valori, decimal valorf)
        {
            var produtos = await context.Products.OrderBy(i => i.Preco).AsNoTracking().Where(x => x.Preco >= valori && x.Preco <= valorf).ToListAsync();
            return produtos;
        }


        [HttpDelete]
        [Route("excluir")]
        public async Task<ActionResult<Product>> Excluir([FromServices] DataContext context, [FromQuery] int id)
        {
            var produto = await context.Products.AsNoTracking().Where(x => x.id == id).FirstAsync();
            context.Products.Remove(produto);
            await context.SaveChangesAsync();
            return Ok();

        }

        [HttpPut]
        [Route("alterar")]
        public async Task<ActionResult<Product>> alterar([FromServices] DataContext context, [FromBody] Product model)
        {
            context.Products.Update(model);
            await context.SaveChangesAsync();
            return model;

        }

    }
}