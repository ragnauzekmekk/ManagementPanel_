using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagementPanel_.Models
{
    public static class LoginUser
    {
        public static int ID { get; set; }
        public static string Name { get; set; }
        public static string Surname { get; set; }
        public static string Password { get; set; }
        public static string Email { get; set; }
        public static string Phone { get; set; }
        public static DateTime Date { get; set; }
    }
}