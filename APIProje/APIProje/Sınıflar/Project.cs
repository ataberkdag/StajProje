using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIProje
{
    class Project
    {
        public virtual int ID { get; set; }
        public virtual string Sender { get; set; }
        public virtual string Data { get; set; }
    }
}
