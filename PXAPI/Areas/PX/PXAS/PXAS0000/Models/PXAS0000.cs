using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace PXAPI.Areas.PX.PXAS.PXAS0000.Models
{
    public class PXAS0000
    {
        private HttpContext currentContext;

        public PXAS0000(HttpContext currentContext)
        {
            this.currentContext = currentContext;
        }
    }
}
