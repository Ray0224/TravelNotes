using Blog.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;



namespace Blog.Controllers
{
	
	public class ArticleController : Controller
	{
		int userID = 1;
		private readonly TravelContext _context;
		private readonly IWebHostEnvironment _hostingEnvironment;

		public ArticleController(IWebHostEnvironment hostingEnvironment,TravelContext context)
		{
			_hostingEnvironment = hostingEnvironment;
			_context = context;
		}
		
        public IActionResult Draft()
        {
			var data = _context.Articles.Where(a =>a.UserId == userID).ToList();
            return View(data);
        }

		public IActionResult DraftView(int editId)
		{
			ViewBag.EditId = editId;
            var data = _context.Articles.Where(a => a.UserId == userID).ToList();
            return View(data);
        }
		[HttpPost]
        public IActionResult SaveDraft(int articleId, string title,string subtitle,DateTime travelTime, string content)
		{
			var currentArticle = _context.Articles.FirstOrDefault(x => x.ArticleId == articleId);
			currentArticle!.Title = title;
			currentArticle.Subtitle = subtitle;
			currentArticle.TravelTime = travelTime;
			currentArticle.Contents = content;
			_context.SaveChanges();
			return RedirectToAction("Draft");
        }
        public IActionResult CreateDraft()
        {
			Article article = new Article();
			article.UserId = userID;//之後要改
			article.Title = "";
			article.Subtitle = "";
			article.PublishTime = DateTime.Now;
			article.TravelTime = DateTime.Now;
			article.Contents = "";
			article.Location = "";
			article.Images = "";
			article.LikeCount = 0;
			article.PageView = 0;
			article.ArticleState = "草稿";
			_context.Add(article);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost]
		//文字編輯器上傳圖片
		public IActionResult UploadImage([FromForm] IFormFile file)
		{
			string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
			string filePath = Path.Combine(uploadsFolder, uniqueFileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			// Return URL of saved image
			string imageUrl = uniqueFileName;


			return Json(new { location = imageUrl });
		}

	}
}
