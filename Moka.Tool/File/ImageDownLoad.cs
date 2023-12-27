using System.Net;

namespace Moka.Tool.File
{
    public class ImageDownLoad
    {
        public static void Download(string imageUrl, string savePath)
        {
            using (WebClient client = new WebClient())
            {
                byte[] imageData = client.DownloadData(imageUrl);
                System.IO.File.WriteAllBytes(savePath, imageData);
            }
        }
    }
}
