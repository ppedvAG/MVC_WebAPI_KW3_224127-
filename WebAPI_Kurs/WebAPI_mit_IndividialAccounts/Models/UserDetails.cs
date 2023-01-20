using System.ComponentModel.DataAnnotations;

namespace WebAPI_mit_IndividialAccounts.Models
{

    //Zum Erstellen eines User, nehmen wir die UserDetails
    public class UserDetails
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }   
    }
}
