using Album_S3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Album_S3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.AlbumList = GetAlbum();
            return View();
        }

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase uploadFile)
        {
            if (uploadFile.ContentLength > 0)
            {
                string filePath = Path.Combine(HttpContext.Server.MapPath("../Images"),
                                               Path.GetFileName(uploadFile.FileName));
                uploadFile.SaveAs(filePath);
               
                ViewBag.UploadMessage = "Successfully uploaded to server.";
            }
            

            ViewBag.AlbumList = GetAlbum();

            return View("Index");
            
        }

        private List<Album> GetAlbum()
        {
            var result = new List<Album>();
            int i = 0;

            foreach (string s in Directory.GetFiles(HttpContext.Server.MapPath("/Images")).Select(Path.GetFileName))
            {
                var album = new Album()
                {
                    id = i,
                    name = s
                };

                i++;
                result.Add(album);
            }

            return result;

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}