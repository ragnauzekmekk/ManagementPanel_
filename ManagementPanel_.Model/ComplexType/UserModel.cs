using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementPanel_.Model.ComplexType
{
    public class UserModel
    {
        public int ID { get; set; }        
        public string Name { get; set; }       
        public string Surname { get; set; }  
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
    }
}
