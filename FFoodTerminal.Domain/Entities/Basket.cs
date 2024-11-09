using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.Domain.Entities
{
    public class Basket
    {
        public long Id { get; set; }

        public UserEntity UserEntity { get; set; }

        public long UserEntityId { get; set; }

        public List<Order> Orders { get; set; }
    }
}
