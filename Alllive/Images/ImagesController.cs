using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;


namespace API.Controllers
{
    /// <summary>
    /// Controller to manage images taken with the app
    /// </summary>
    public class ImagesController : ApiControllerBase
    {

        /// <summary>
        /// Gets the image content for a particular image
        /// </summary>
        /// <param name="imageID">The image ID</param>
        /// <returns>The image content</returns>
        [Route("Images/{imageID}")]
        [ResponseType(typeof(ProcImages))]
        [HttpGet]
        public HttpResponseMessage GetImageContent(int imageID)
        {
            var image = Dc.Images.FirstOrDefault(i => i.ImageID == imageID);
            if (image != null && File.Exists(image.FilePath))
            {
                var stream = File.OpenRead(image.FilePath);
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MimeMapping.GetMimeMapping(Path.GetExtension(image.FilePath)));
                return result;
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// Gets a thumbnail of the image content for a particular image
        /// </summary>
        /// <param name="imageID">The image ID</param>
        /// <returns>The thumbnail image content</returns>
        [Route("Images/small/{imageID}")]
        [HttpGet]
        public HttpResponseMessage GetImageSmallContent(int imageID)
        {
            var image = Dc.Images.FirstOrDefault(i => i.ImageID == imageID);
            if (image != null)
            {
                FileStream stream;
                if (File.Exists(image.FilePathThumb))
                {
                    stream = File.OpenRead(image.FilePathThumb);
                }
                else if(File.Exists(image.FilePath))
                {
                    var thumbPath = image.FilePath.Replace(ConfigSettings.DeployedRoot, ConfigSettings.DeployedRoot + "\\Thumbs\\");
                    System.Drawing.Image originalImage = System.Drawing.Image.FromFile(image.FilePath);
                    var thumbImage = DataHelper.GetImageThumbnail(originalImage);
                    thumbImage.Save(thumbPath);
                    image.FilePathThumb = thumbPath;
                    stream = File.OpenRead(image.FilePathThumb);
                } else {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MimeMapping.GetMimeMapping(Path.GetExtension(image.FilePath)));
                return result;
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// Uploads a new image to save
        /// </summary>
        /// <param name="userID">The user ID</param>
        /// <param name="metaType">The image type</param>
        /// <returns>The meta data for the new image</returns>
        [ResponseType(typeof(ResultsDTO))]
        [Route("Images/{userID}/{metaType}")]
        [HttpPut]
        public IHttpActionResult UploadNewImage(int userID, string metaType)
        {
            var request = HttpContext.Current.Request;
            DataHelper.EnsureFolders(_agent.Host);
            if (request.Files.Count == 0)
            {
                return Ok(new ResultsDTO { status = "error", message = "No file selected" });
            }
            string baseFile = request.Files[0].FileName;
            string extension = baseFile.Substring(baseFile.LastIndexOf('.'));
            baseFile = baseFile.Substring(0, baseFile.LastIndexOf('.'));

            var filePath = ConfigSettings.DeployedPdfRoot + _agent.Host + "\\Images\\" + procID + baseFile + extension;
            var thumbPath = ConfigSettings.DeployedPdfRoot + _agent.Host + "\\Images\\Thumbs\\" + procID + baseFile + extension;            

            if (File.Exists(filePath) || File.Exists(thumbPath)) {
                var i = DateTime.Now.Ticks;                
                filePath = ConfigSettings.DeployedPdfRoot + _agent.Host + "\\Images\\" + procID + baseFile + string.Format("_{0}", i) + extension;
                thumbPath = ConfigSettings.DeployedPdfRoot + _agent.Host + "\\Images\\Thumbs\\" + procID + baseFile + string.Format("_{0}", i) + extension;
            }
            request.Files[0].SaveAs(filePath);
            using (System.Drawing.Image originalImage = System.Drawing.Image.FromFile(filePath))
            {
                using (var thumbImage = DataHelper.GetImageThumbnail(originalImage))
                {
                    thumbImage.Save(thumbPath);
                }
            }

            var image = new Images()
            {
                UserID = userID, 
                MetaType = metaType,
                CreatedOn = DateTime.Now,
                CreatedBy = _agent.User.UserID,
                LastUpdatedBy = _agent.User.UserID,
                LastUpdatedOn = DateTime.Now,
                FilePath = filePath,
                FilePathThumb = thumbPath
            };


            Dc.Add(image);
            Dc.SaveChanges();
            return Ok(new ResultsDTO { result = new { UserID = image.UserID, MetaType = image.MetaType, ImageID = image.ImageID,
                LastUpdatedBy = image.LastUpdatedBy, LastUpdatedOn = image.LastUpdatedOn,
                Url = BaseUrl + "Images/" + image.ImageID } });
        }

        /// <summary>
        /// Delete an image given it's id
        /// </summary>
        /// <param name="imageID">The image ID</param>
        /// <returns>Empty result</returns>
        [ResponseType(typeof(ResultsDTO))]
        [Route("Images/{imageID}")]
        [HttpDelete]
        public IHttpActionResult Delete(int imageID)
        {
            var img = Dc.Images.Find(imageID);
            if (img != null)
            {
                try
                {
                    if (File.Exists(img.FilePathThumb))
                    {
                        File.Delete(img.FilePathThumb);
                    }
                    if (File.Exists(img.FilePath))
                    {
                        File.Delete(img.FilePath);
                    }
                }
                catch (Exception ex)
                {

                }
                _agent.Json = Newtonsoft.Json.JsonConvert.SerializeObject(img);
                Dc.Images.Remove(img);
                Dc.SaveChanges();
            }
            return Ok(new ResultsDTO { status = ResultsDTO.ok });
        }
    }
}
