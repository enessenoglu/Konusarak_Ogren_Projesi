using System.ComponentModel.DataAnnotations;

namespace K_O_Project.Models.Entities
{
    public class Login
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Kullanıcı Adı Boş Geçilemez")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Parola Boş Geçilemez")]
        public string Password { get; set; }
    }
}
