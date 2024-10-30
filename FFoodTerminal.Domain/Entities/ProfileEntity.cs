using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.Domain.Entities
{
    public class ProfileEntity
    {
        public long Id { get; set; }

        public byte Age { get; set; }

        public string Address { get; set; }

        public long UserEntityId { get; set; }

        public UserEntity? UserEntity { get; set; }
    }
}
