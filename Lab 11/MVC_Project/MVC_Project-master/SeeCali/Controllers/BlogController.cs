using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeeCali.Models;

namespace SeeCali.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly BlogDataContext _db;

        public BlogController(BlogDataContext db)
        {
            _db = db;
        }
        /*  [Route("")]
          public IActionResult Index()
          {
              var posts = _db.Posts.OrderByDescending(x => x.Posted).Take(5).ToArray();

              return View(posts);
          }*/

        [Route("")]
        public IActionResult Index(int page = 0)
        {
            var pageSize = 2;
            var totalPosts = _db.Posts.Count();
            var totalPages = totalPosts / pageSize;
            var previousPage = page - 1;
            var nextPage = page + 1;

            ViewBag.PreviousPage = previousPage;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.NextPage = nextPage;
            ViewBag.HasNextPage = nextPage < totalPages;

            var posts =
                _db.Posts
                    .OrderByDescending(x => x.Posted)
                    .Skip(pageSize * page)
                    .Take(pageSize)
                    .ToArray();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(posts);

            return View(posts);
        }

        [Route("{year:min(2000)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {
            var post = _db.Posts.FirstOrDefault(x => x.Key == key);
            return View(post);
        }
        /*
        [Route("{year:min(2010)}/{month:range(1, 12)}/{key}")]
        public IActionResult Post()
        {
            var post = new Post
            {
                Title = "my blog post",
                Posted = DateTime.Now,
                Author = "Matthew McFarlane",
                Body = "This is a great blog post, don't you think?"
            };
            return View(post); 
            // new ContentResult { Content = string.Format("Year: {0}; Month: {1}; Key: {2}", year, month, key)};
        }*/
        [Authorize]
        [HttpGet, Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost, Route("create")]
        public IActionResult Create(Post p)
        {
            if (!ModelState.IsValid)
                return View();
            p.Author = User.Identity.Name;
            p.Posted = DateTime.Now;

            _db.Posts.Add(p);
            _db.SaveChanges();

            return RedirectToAction("Post", "Blog", new
            {
                year = p.Posted.Year,
                month = p.Posted.Month,
                key = p.Key
            });
        }

        
        //return new ContentResult { Content = null };


        /*  public IActionResult Post(int? id)
          {
              if (id == null)
                  return new ContentResult { Content = null };
              else
                  return new ContentResult { Content = id.ToString() };
          }*/
    }
}