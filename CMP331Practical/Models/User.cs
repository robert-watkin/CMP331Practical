using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMP331Practical.Data;
using CMP331Practical.Contracts;
using Unity;

namespace CMP331Practical.Models
{
    public class User : BaseEntity {

        IRepository<Role> roleContext;

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

        public override string ToString()
        {
            // checks for null option
            if (Firstname == null && Lastname == null && Email == null && RoleId == null) { return "None"; }

            // get all roles
            this.roleContext = ContainerHelper.Container.Resolve<IRepository<Role>>();
            List<Role> roleList = roleContext.Collection().ToList();

            // identify users current role name
            string role = "";
            foreach (Role r in roleList)
            {
                if (r.Id == RoleId)
                {
                    role = r.Name;
                }
            }

            // return string to display in combo box
            return Firstname + " " + Lastname + ": " + role;
        }
    }
}
