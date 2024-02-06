
public static class ConfigManager
{
    public static string outputDirectory { get; internal set; } = Path.Combine(Directory.GetCurrentDirectory(),"Output");
    public static int MaxParalel { get; internal set; } = 3;
}