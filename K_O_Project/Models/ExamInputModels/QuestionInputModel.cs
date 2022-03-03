using System.ComponentModel.DataAnnotations;

namespace K_O_Project.Models.Exam
{
    public class QuestionInputModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Title { get; set; }
        [Required]
        public string SelectedAnswer { get; set; }
        public OptionInputModel[] Options { get; set; } = new OptionInputModel[4];

    }
}
