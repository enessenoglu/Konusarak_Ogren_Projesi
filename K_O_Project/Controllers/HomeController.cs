using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using K_O_Project.Filter;
using K_O_Project.Models;
using K_O_Project.Models.Entities;
using K_O_Project.Models.Exam;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics;

namespace K_O_Project.Controllers
{
    [UserFilter]
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public HomeController(AppDbContext context)
        {
            _appDbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(ExamInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var exam = new Models.Entities.Exam();
            exam.Title = model.Title;
            exam.Content = model.Content;
            _appDbContext.Exams.Add(exam);

            foreach (var question in model.Questions)
            {
                var _question = new Question();
                _question.ExamId = exam.Id;
                _question.Title = question.Title;
                _question.SelectedAnswer = question.SelectedAnswer;
                _appDbContext.Questions.Add(_question);
                foreach (var option in question.Options)
                {
                    var _option = new Option();
                    _option.QuestionId = _question.Id;
                    _option.Title = option.Title;
                    _option.OptionName = option.OptionName;
                    _appDbContext.Options.Add(_option);
                }
            }

            _appDbContext.SaveChanges();

            return RedirectToAction("ExamList");
        }
      public IActionResult ExamList()
        {
            var data = _appDbContext.Exams.ToList();
            return View(data);
        }

        [HttpGet]
        public IActionResult ExamDelete(string id)
        {
            var data = _appDbContext.Exams.FirstOrDefault(x=> x.Id==id);
            _appDbContext.Set<Exam>().Remove(data);
            _appDbContext.SaveChanges();
            return RedirectToAction("ExamList");
        }

        /// <summary>
        /// wired.com adresindeki son 5 makalenin başlıklarını ve içeriklerini döndürür
        /// </summary>
        /// <returns></returns>
        public JsonResult GetContentData()
        {
            var url = "https://www.wired.com/";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            IList<HtmlNode> nodes = doc.QuerySelectorAll("div.summary-list__items")[1].QuerySelectorAll("div.SummaryItemWrapper-gdEuvf");
            var counter = 0;
            var data = nodes.Select(node =>
            {
                var contentUrl = node.QuerySelector("div.SummaryItemContent-gYA-Dbp a ").GetAttributeValue("href", "");

                counter++;
                //IList<HtmlNode> nodescontent = doc.QuerySelectorAll("div.GridWrapper-vNBSO");
                return new TitleContentViewModel
                {
                    Id = counter,
                    Title = node.QuerySelector("div.SummaryItemContent-gYA-Dbp a h2").InnerText,
                    Content = $"https://www.wired.com{contentUrl}",

                    // Content= node.QuerySelector("div.SummaryItemContent-gYA-Dbp div.BaseWrap-sc-TURhJ").InnerText

                };
            }).ToList();



            foreach (var item in data)
            {
                
                var contenturl = item.Content;
                var docContent = web.Load(contenturl);
                IList<HtmlNode> nodesContent = docContent.QuerySelectorAll("div.BaseWrap-sc-TURhJ")[0].QuerySelectorAll("div.BaseWrap-sc-TURhJ");
                if (nodesContent==null)
                {
                    
                }
                var contentData = nodesContent.Select(node =>
                {
                    return node.QuerySelector("div.GridWrapper-vNBSO div div div div p").InnerText;
                }).FirstOrDefault();

                if (contentData != null)
                    item.Content = contentData;


            }

            return Json(data);
        }
       


    }
}