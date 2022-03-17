﻿using HelenSposa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelenSposa.Entities.Dtos.User
{
    public class UserAddDto:IDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Email { get; set; }
    }
}
