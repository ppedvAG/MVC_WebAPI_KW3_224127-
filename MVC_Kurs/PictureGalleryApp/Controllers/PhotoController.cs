using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace PictureGalleryApp.Controllers
{
    public class PhotoController : Controller
    {
        public IActionResult Index()
        {
            string imgDirectory = AppDomain.CurrentDomain.GetData("BildVerzeichnis") + @"\images\";

            string[] bilder = Directory.GetFiles(imgDirectory);

            return View(bilder);
        }


        [HttpGet]
        public async Task<IActionResult> UploadPicture()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadPicture(IFormFile file)
        {
            if (file == null)
                ModelState.AddModelError("datei", "Bitte eine Datei auswählen, bevor wir Upload klicken");


            if (ModelState.IsValid)
            {
                FileInfo fileInfo = new FileInfo(file.FileName);

                string savePath = AppDomain.CurrentDomain.GetData("BildVerzeichnis") + @"\images\" + fileInfo.Name;

                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return RedirectToAction("Index");
        }




    }
}
