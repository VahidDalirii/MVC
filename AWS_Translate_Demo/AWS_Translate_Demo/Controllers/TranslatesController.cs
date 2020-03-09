using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Translate;
using Amazon.Translate.Model;
using AWS_Translate_Demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace AWS_Translate_Demo.Controllers
{
    public class TranslatesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult AddComment()
        {
            var model = new TranslateCommentViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult TranslateComment(TranslateCommentViewModel comment)
        {
            var translate = new AmazonTranslateClient("AKIAX3JSZJ36DOCUNIJY", "vTKVVvQs01qY25LczK5uSdD3gev49cdXfKNr7LRT", RegionEndpoint.USEast1);
            var request = new TranslateTextRequest() { Text = comment.CommentText, SourceLanguageCode = "en", TargetLanguageCode = comment.TargetLanguage };

            var task = translate.TranslateTextAsync(request); //.TranslateText(request) // Make the actual call to Amazon Translate
            task.Wait();

            var model = new TranslatedCommentViewModel()
            {
                CommentText = comment.CommentText,
                SubmitterName = comment.SubmitterName,
                TargetLangauge = comment.TargetLanguage,
                TranslateResponse = task.Result
            };
            return View(model);
        }
    }
}