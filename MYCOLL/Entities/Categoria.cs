using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace MYCOLL.Entities
{     
 
    public class Categoria
    {
        
        public int Id { get; set; }
        [Required]
        public string? Nome { get; set; }

        public int? Ordem { get; set; } = 1;
        public string? URLImagem { get; set; }
        public byte[]? Imagem { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
