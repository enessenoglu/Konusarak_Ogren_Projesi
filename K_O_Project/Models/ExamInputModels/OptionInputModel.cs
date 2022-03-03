using System.ComponentModel.DataAnnotations;

namespace K_O_Project.Models.Exam
{
    public class OptionInputModel
    {
        [Required]

        public string OptionName { get; set; }
        [MinLength(2)]
        [MaxLength(40)]
        [Required]
        public string Title { get; set; }

    }
}
