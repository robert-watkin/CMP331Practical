using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP331Practical.Models
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public Role() { }
        public Role(string Name)
        {
            this.Name = Name;
        }
    }
}
