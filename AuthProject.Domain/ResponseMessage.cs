﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthProject.Domain
{
    public class ResponseMessage
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public string Token { get; set; }
    }
}
