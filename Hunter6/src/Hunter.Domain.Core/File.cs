namespace Hunter.Domain.Core
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public byte[] FileContent { get; set; } 
    }
}
