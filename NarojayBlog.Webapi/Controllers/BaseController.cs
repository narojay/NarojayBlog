﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace NarojayBlog.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    { 

        protected readonly IMapper Mapper;
        public BaseController(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
