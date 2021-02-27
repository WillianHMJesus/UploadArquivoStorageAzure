namespace UploadFotosFiap11NetService.Models
{
    public class ImageFile
    {
        public ImageFile()
        {

        }

        public ImageFile(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string Name { get; private set; }
        public string Url { get; private set; }
    }
}