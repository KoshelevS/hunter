namespace Hunter6.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Project Project { get; set; }
    }
}