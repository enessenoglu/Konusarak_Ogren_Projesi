namespace K_O_Project.Models.Entities
{
    public class Option
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// A , B , C, D
        /// </summary>
        public string OptionName { get; set; }

        public string Title { get; set; }
        public string QuestionId { get; set; }
        public Question Question { get; set; }

    }
}