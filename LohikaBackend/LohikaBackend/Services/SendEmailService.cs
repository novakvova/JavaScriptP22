using LohikaBackend.Helpers;

namespace LohikaBackend.Services
{
    public static class SendEmailService
    {
        public static void SendInfoStartApp(IConfiguration configuration)
        {
            try
            {
                string adminEmail = configuration.GetValue<string>("AdminEmail");
                string name = configuration.GetValue<string>("AdminName");
                string text = "Ваш сайт запущено. Можете тестувати.";
                string title = "Запуск сайту";
                string fileDestDir = Directory.GetCurrentDirectory();
                fileDestDir = Path.Combine(fileDestDir, "EmailForms");
                string fileName = Path.Combine(fileDestDir, "InfoStartApp.html");

                string body = string.Empty;
                using (StreamReader reader = new StreamReader(fileName))
                {
                    var str = string.Empty;
                    do
                    {
                        str = reader.ReadLine();
                        body += str + " ";
                    }
                    while (str != null);
                    //body = reader.ReadToEnd();
                }
                body = body.Replace("{UserName}", name);
                body = body.Replace("{Title}", "Cайт бек для форонта");
                body = body.Replace("{Url}", "https://vovalohika.tk/");
                body = body.Replace("{Description}", text);

                string command = $"echo '{body}' | " +
                    $"mail " +
                    $"-a \"Content-type: text/html;\" " +
                    $"-s \"{title} - https://vovalohika.tk/\" " +
                    $"{adminEmail}  -aFrom:noreply@vovalohika.tk";

                Console.WriteLine($"Send email to Admin {command}");
                File.WriteAllLines("message.txt", new string[] { command });
                var output = command.Bash();
            }
            catch (Exception ex)
            {
                Console.WriteLine("---problem message send Ф---" + ex.Message);
            }

        }
    }
}
