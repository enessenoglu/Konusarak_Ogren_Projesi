using K_O_Project.Filter;
using K_O_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace K_O_Project.Controllers
{
    [UserFilter]
    public class ExamController : Controller
    {
        private readonly AppDbContext _context;
        public ExamController(AppDbContext context)
        {
            _context = context;
           
        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet("Exam/GetExam/{examId}")]
        public IActionResult GetExam(string examId)
        {
            var exam = _context.Exams.Include(x => x.Questions).ThenInclude(x => x.Options).Where(x => x.Id == examId).FirstOrDefault();
            return View(exam);
        }

        [HttpPost]
        public JsonResult CompleteExam([FromBody] CompleteExamInputModel model)
        {

            var exam = _context.Exams.Include(x => x.Questions).ThenInclude(x => x.Options).Where(x => x.Id == model.ExamId).FirstOrDefault();

            var returnModel = new List<CompleteExamViewModel>();
            foreach (var answer in model.ExamAnswers)
            {
                var question = exam.Questions.Find(x => x.Id == answer.QuestionId);

                returnModel.Add(new CompleteExamViewModel
                {
                    QuestionId = question.Id,
                    CorrectAnswer = question.SelectedAnswer,
                    SelectedAnswer = answer.SelectedAnswer,
                    SelectedAnswerId = question.Options.Where(x=>x.OptionName==answer.SelectedAnswer).First().Id,
                });
            }

            return Json(returnModel);
        }
    }
}
