using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models
{
    public class Anexo
    {
        public string? NomeArquivo { get; set; }
        [Required]
        public IFormFile ArquivoUpload { get; set; }
        public string? Arquivo { get; set; }
    }
}
