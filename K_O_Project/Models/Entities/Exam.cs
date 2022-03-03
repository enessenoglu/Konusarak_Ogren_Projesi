namespace K_O_Project.Models.Entities
{
    public class Exam
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public List<Question> Questions { get; set; }
    }
}
