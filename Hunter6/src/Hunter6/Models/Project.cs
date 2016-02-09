using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hunter6.Models
{
    public class Project
    {
        public int ID { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public List<Vacancy> Vacancies { get; set; }

    }
}
