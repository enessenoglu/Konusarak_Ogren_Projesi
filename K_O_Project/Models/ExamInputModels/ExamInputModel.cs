using System.ComponentModel.DataAnnotations;

namespace K_O_Project.Models.Exam
{
    public class ExamInputModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public QuestionInputModel[] Questions { get; set; } = new QuestionInputModel[4];

    }
}
