using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Bases
{
    public abstract class BaseEntity
    {
        public abstract DateTime? CreatedDateTime { get; set; }
        public abstract DateTime? UpdatedDateTime { get; set; }
    }
}
