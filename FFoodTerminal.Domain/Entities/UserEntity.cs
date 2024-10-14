using FFoodTerminal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.Domain.Entities
{
    public class UserEntity
    {
        public long Id { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public Role Role { get; set; }
    }
}
