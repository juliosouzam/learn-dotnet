using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zoeira.Models
{
  public class User
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Nome")]
    [Required(ErrorMessage = "O {0} é obrigatório")]
    [Column(TypeName = "varchar(50)")]
    public string Name { get; set; }

    [Display(Name = "E-mail")]
    [Required(ErrorMessage = "O {0} é obrigatório")]
    [EmailAddress]
    [Column(TypeName = "varchar(50)")]
    public string Email { get; set; }
  }
}