using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fs = System.IO;

namespace Alllive.Controllers
{
    public class ImagesController : AllLiveControllerBase
    {
        // GET: Images
        [HttpGet]
        public ActionResult Index(int imageID)
        {
            var image= Dc.Images.Find(imageID);
            if(image!=null && fs.File.Exists(image.FilePath))
            {
                return File(image.FilePath,MimeMapping.GetMimeMapping(fs.Path.GetExtension(image.FilePath)));
            }
            return HttpNotFound();

            
        }

    }
}