
namespace SeleniumFakeMailTester.SeleniumFakeMailTester.Selenium.Helpers
{
    public static class SystemHelper
    {
        public static async Task SaveOutput(string username, string output, string filename)
        {
            filename = RemoveInvalidChars(filename);

            var outputDirectory = Path.Combine(ConfigManager.outputDirectory, username);
            if (!Directory.Exists(outputDirectory)) { Directory.CreateDirectory(outputDirectory); }

            await using (StreamWriter writer = new StreamWriter(Path.Combine(outputDirectory, filename)))
            {
                writer.Write(output);
            }
        }

        public static int ExtractNumberFromURL(string url)
        {
            Uri uri = new Uri(url);
            string lastSegment = uri.Segments.Last().Trim('/');
            bool isNumber = int.TryParse(lastSegment, out int number);
            if (isNumber)
            {
                return number;
            }
            else
            {
                throw new FormatException("Number not found!");
            }
        }

        public static string RemoveInvalidChars(string input)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            var validChars = input.Where(ch => !invalidChars.Contains(ch)).ToArray();
            return new string(validChars);
        }
    }
}
