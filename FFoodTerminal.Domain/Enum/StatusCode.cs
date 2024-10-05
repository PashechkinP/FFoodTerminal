using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.Domain.Enum
{
    public enum StatusCode
    {
        ProductNotFound = 0,
        OK = 200,
        InternalServerError = 500
    }
}
