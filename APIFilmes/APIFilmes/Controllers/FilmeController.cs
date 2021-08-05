using APIFilmes.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFilmes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();
        private static int id = 1;

        //O verbo "Post" sobe para o banco de dados
        [HttpPost]
        public IActionResult AdicionarFilme([FromBody] Filme filme)
        {
            //Faz com que os filmes não tenham o mesmo Id
            filme.Id = id++;

            filmes.Add(filme);

            //O primeiro parâmetro é a lógica de recuperar que deve ser usada, o segundo é a propriedade que deve ser herdada e o terceiro o objeto que está se referindo
            return CreatedAtAction(nameof(RecuperaFilmesPorId), new {id = filme.Id}, filme);
        }

        //O ver "Get" desce do banco de dados
        //Note que podemos usar IEnumerable<Filme> ao invés de uma lista de filmes ou IActionResult, porque isso possibilita o uso desse método por outras classes que derivem dessa.
        [HttpGet]
        public IActionResult RecuperaFilmes()
        {
            return Ok(filmes);
        }
        /*
         * Isto v é a mesma coisa do que isso ^
         * [HttpGet]
        public List<Filme> RecuperaFilme()
        {
            return filmes;
        }
        */

        //Perceba que este HttpGet diferencia-se do outro pelo fato de pedir um parâmetro "id". Além disso, o IActionResult permite usar o Ok e o NotFound!
        [HttpGet("{id}")]
        public IActionResult RecuperaFilmesPorId(int id)
        {
            /*
            foreach (Filme filme in filmes)
            {
                if (filme.Id == id)
                {
                    return filme;
                }
            }
            return null;
            */

            //Isto v é o mesmo que isso ^
            Filme filme = filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme != null)
            {
                return Ok(filme);
            }
            return NotFound($"O filme com o id {id} não pode ser achado!");
        }
    }
}
