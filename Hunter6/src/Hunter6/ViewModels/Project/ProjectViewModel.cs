using System;
using System.ComponentModel.DataAnnotations;

namespace Hunter.ViewModels.Project
{
    public class ProjectViewModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string FirstVacancy { get; set; }
    }
}