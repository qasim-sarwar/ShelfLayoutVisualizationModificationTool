﻿using Microsoft.AspNetCore.Mvc;
using TexCode.Entities;

namespace TexCode.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        // returns the current authenticated account (null if not logged in)
        public Account Account => (Account)HttpContext.Items["Account"];
    }
}
