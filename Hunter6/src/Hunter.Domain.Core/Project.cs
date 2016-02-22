using System.Collections.Generic;

namespace Hunter.Domain.Core
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Vacancy> Vacancies { get; set; }

    }
}
