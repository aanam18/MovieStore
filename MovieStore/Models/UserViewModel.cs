using System.ComponentModel;

namespace MovieStore.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("E-mail")]
        public string Email { get; set; }
    }
}
