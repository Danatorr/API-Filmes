using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIFilmes.Models
{
    public class Filme
    {
        //O required faz com que retorne um 400 (BadRequest) caso esteja vazio, e o ErrorMessage define a mensagem
        [Required(ErrorMessage = "Insira um título válido!")]
        [StringLength(50, ErrorMessage = "O título do filme não pode passar de 50 caracteres!")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Insira um diretor válido!")]
        [StringLength(50, ErrorMessage = "O nome do diretor do filme não pode passar de 50 caracteres!")]
        public string Diretor { get; set; }
        [StringLength(50, ErrorMessage = "O gênero do filme não pode passar de 50 caracteres!")]
        public string Genero { get; set; }
        [Required]
        [Range(1, 530, ErrorMessage = "Insira uma duração válida!")]
        public int Duracao { get; set; }
    }
}
