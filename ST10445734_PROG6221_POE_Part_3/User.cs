using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10445734_Prog6221_POE_Part1
{
    public class User
    {
        public string Name { get; set; }
        public string FavoriteTopic { get; set; }

        public User(string name) 
        {
            Name = name;
            FavoriteTopic = "";
        }
    }
}
