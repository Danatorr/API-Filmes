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
        public void AdicionarFilme([FromBody] Filme filme)
        {
            //Faz com que os filmes não tenham o mesmo Id
            filme.Id = id++;

            filmes.Add(filme);
            Console.WriteLine(filme.Titulo);
        }

        //O ver "Get" desce do banco de dados
        //Note que usamos IEnumerable<Filme> ao invés de uma lista de filmes, porque isso possibilita o uso desse método por outras classes que derivem dessa.
        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes()
        {
            return filmes;
        }
        /*
         * Isto v é a mesma coisa do que isso ^
         * [HttpGet]
        public List<Filme> RecuperaFilme()
        {
            return filmes;
        }
        */

        //Perceba que este HttpGet diferencia-se do outro pelo fato de pedir um parâmetro "id".
        [HttpGet("{id}")]
        public Filme RecuperaFilmesPorId(int id)
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
            return filmes.FirstOrDefault(filme => filme.Id == id);
        }
    }
}
