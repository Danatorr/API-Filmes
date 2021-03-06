using APIFilmes.Data;
using APIFilmes.Data.Dtos;
using APIFilmes.Models;
using AutoMapper;
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
        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        //O verbo "Post" sobe para o banco de dados
        [HttpPost]
        public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);

            //Adicionando e salvando um objeto
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            //O primeiro parâmetro é a lógica de recuperar que deve ser usada, o segundo é a propriedade que deve ser herdada e o terceiro o objeto que está se referindo
            return CreatedAtAction(nameof(RecuperaFilmesPorId), new {id = filme.Id}, filme);
        }

        //O ver "Get" desce do banco de dados
        //Note que podemos usar IEnumerable<Filme> ao invés de uma lista de filmes ou IActionResult, porque isso possibilita o uso desse método por outras classes que derivem dessa.
        [HttpGet]
        public IActionResult RecuperaFilmes()
        {
            return Ok(_context.Filmes);
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
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme != null)
            {
                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);
                return Ok(filmeDto);
            }
            return NotFound($"O filme com o id {id} não pode ser achado!");
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                _mapper.Map(filmeDto, filme);
                _context.SaveChanges();
                return NoContent();
            }

            return NotFound($"O filme com o id {id} não pode ser achado!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                _context.Remove(filme);
                _context.SaveChanges();
                return NoContent();
            }

            return NotFound($"O filme com o id {id} não pode ser achado!");
        }
    }
}
