using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Wesley.Lottery.Core.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Wesley.Lottery.Core.Controllers
{
    [Authorize]
    public class AppController:Controller
    {
        
    }
}
