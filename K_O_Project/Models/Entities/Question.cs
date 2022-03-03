namespace K_O_Project.Models.Entities
{
    public class Question
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string SelectedAnswer { get; set; }
        public string ExamId { get; set; }

        public Exam Exam { get; set; }

        public List<Option> Options { get; set; }
    }
}