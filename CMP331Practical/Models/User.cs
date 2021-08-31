using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP331Practical.Models
{
    public class User : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleId { get; set; }

        public User() { }
        public User(string Firstname, string Lastname, string Email, string Password, string RoleId)
        {
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.Email = Email;
            this.Password = Password;
            this.RoleId = RoleId;
        }
    }
}
