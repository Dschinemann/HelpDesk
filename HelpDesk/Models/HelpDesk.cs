using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models
{
    public class HelpDesk
    {
        [Required]
        [Display(Name = "Nome do Usuário")]
        public string Nome { get;set; }
        [Required]
        public string Filial { get; set; } = "16519945000154";
        [Required]
        public string Categoria { get; set; }
        [Required]
        public int Tipo { get; set; }
        [Required]
        public int Modulo { get; set; }
        [Required]
        public int Departamento { get; set; }
        [Required]
        public string Assunto { get; set; }
        [Required]
        [Display(Name ="Descrição")]
        public string Descricao { get; set; }
        [Required]
        public int Severidade { get;set; }
        [Required]
        public Anexo Anexo { get; set; }

        public int Sistema { get; set; } = 4;

    }
}
