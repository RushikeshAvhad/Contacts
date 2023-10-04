using SQLite;
using System.ComponentModel.DataAnnotations;

namespace Contacts.CoreBusiness
{
    public class Contact
    {
        [Required]
        [PrimaryKey, AutoIncrement]
        public int ContactId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}