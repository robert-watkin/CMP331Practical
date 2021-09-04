using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP331Practical.Models
{
    public class Notification
    {
        public string address { get; set; }
        public string notification { get; set; }

        public Notification(string address, string notification)
        {
            this.address = address;
            this.notification = notification;
        }
    }
}
