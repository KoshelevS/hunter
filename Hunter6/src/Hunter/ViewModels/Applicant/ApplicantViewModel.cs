using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hunter.ViewModels.Applicant
{
    public class ApplicantViewModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
