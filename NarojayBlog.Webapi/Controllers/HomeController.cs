﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace NarojayBlog.Webapi.Controllers
{
    public class HomeController :BaseController
    {
        public HomeController(IMapper mapper) : base(mapper)
        {
        }
    }
}
