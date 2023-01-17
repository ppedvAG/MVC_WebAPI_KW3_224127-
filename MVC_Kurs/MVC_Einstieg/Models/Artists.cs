using System.ComponentModel;

namespace MVC_Einstieg.Models
{
    public class Artists
    {
        public int Id { get; set; }

   
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
