using System.Drawing;

namespace PictureGalleryApp.Middleware
{
    public class ThumbnailGen
    {
        private readonly RequestDelegate _next;

        public ThumbnailGen(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string fileNameOfPicture = httpContext.Request.Query["img"][0];

            string absolutePicturePath = AppDomain.CurrentDomain.GetData("BildVerzeichnis") + @"\images\" + fileNameOfPicture;

            using (var sr = new FileStream(absolutePicturePath, FileMode.Open))
            {
                //Orginal Bitmap aus wwwroot\images
                using (var image = new Bitmap(sr))
                {
                    var resized = new Bitmap(300, 200);

                    using (var graphics = Graphics.FromImage(resized))
                    {
                        graphics.DrawImage(image, 0, 0, 300, 200);

                        MemoryStream ms = new MemoryStream();

                        resized.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                        await httpContext.Response.Body.WriteAsync(ms.ToArray());
                    }
                }
            }
        }
    }



    public static class ThumbnailGenExtensions
    {
        public static IApplicationBuilder UseThumbnailGen(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ThumbnailGen>();
        }
    }
}
