using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Model
{
    public class JMessage
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool Error { get; set; }
        public bool Code { get; set; }
    }
}
