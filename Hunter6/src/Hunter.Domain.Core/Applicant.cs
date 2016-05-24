using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hunter.Domain.Core
{
    public class Applicant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
    }
}
