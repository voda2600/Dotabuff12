﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemestreWork.Models
{  
    public class RegisterModel
    {
        public int Id { get; set; }

        public string Nick { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Hero { get; set; }
        public string MMR { get; set; }
        private string role;
        public string Role { get { return role; }  set
        {
            if (value=="Admin" || value=="Default")
            {
                    role = value;   
            }
            else
            {
                    throw new Exception("Ваша роль ошибочна");
                }
        } }
    }
}