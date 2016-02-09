namespace Hunter6.Models
{
    public class Vacancy
    {
        public int VacancyId { get; set; }
        public string Name { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}