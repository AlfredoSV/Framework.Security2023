﻿using Framework.Security2023.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Dtos
{
    public class DtoLoginResponse
    {
        public StatusLogin StatusLogin;
        public DtoUserFkw User { get; set; }
    }
}
