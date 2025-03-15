using System.Drawing.Imaging;

namespace LohikaBackend.Helpers
{
    public class InitStaticFiles
    {
        public static string CreateImageByFileName(IWebHostEnvironment env,
                                                  IConfiguration configuration,
                                                  string[] settingsFolder,
                                                  string fileName, string base64,
                                                  bool watermark = true, bool transperent = false)
        {
            string[] imageSizes = ((string)configuration.GetValue<string>("ImageSizes")).Split(" ");
            bool fileBeginCreated = false;
            string fileDestDir = env.ContentRootPath;

            try
            {
                foreach (var pathConfig in settingsFolder)
                {
                    fileDestDir = Path.Combine(fileDestDir, configuration.GetValue<string>(pathConfig));
                    if (!Directory.Exists(fileDestDir))
                    {
                        Directory.CreateDirectory(fileDestDir);
                    }
                }

                fileBeginCreated = true;
                if (base64.Contains(","))
                {
                    base64 = base64.Split(',')[1];
                }
                using (var bmp = base64.FromBase64StringToImage())
                {
                    //long jpegByteSize;
                    //using (var ms = new MemoryStream()) // estimatedLength can be original fileLength
                    //{
                    //    bmp.Save(ms, ImageFormat.Png); // save image to stream in Png format
                    //    jpegByteSize = ms.Length;
                    //}     
                    foreach (var imagePrefix in imageSizes)
                    {
                        int size = int.Parse(imagePrefix);
                        string fileSave = Path.Combine(fileDestDir, $"{imagePrefix}_{fileName}");
                        //Console.WriteLine("Image save path")
                        if (bmp != null)
                        {
                            using (var image = ImageWorker.CompressImage(bmp, size, size, watermark, transperent))
                            {
                                if (image == null)
                                    throw new Exception("В процесі створення фото виникли проблеми");

                                image.Save(fileSave, ImageFormat.Jpeg);
                            }
                        }
                        else
                        {
                            throw new Exception("В процесі створення фото виникли проблеми");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("----------Save image problem------- {0}", ex.Message);
                if (fileBeginCreated)
                {
                    foreach (var imagePrefix in imageSizes)
                    {
                        string fileImage = Path.Combine(fileDestDir, $"{imagePrefix}_{fileName}");
                        if (File.Exists(fileImage))
                        {
                            File.Delete(fileImage);
                        }
                    }
                }
                return null;
            }

            return fileDestDir;
        }
    }
}
