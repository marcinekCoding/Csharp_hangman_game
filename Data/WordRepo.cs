public class WordRepo
{
    private readonly List<string> _words;

    public WordRepo(string category)
    {
        string path = Path.Combine(AppContext.BaseDirectory, "Data", category + ".txt");

        if (!File.Exists(path))
        {
            throw new FileNotFoundException(
                $"Word file not found: {path}. Run 'dotnet build' first.");
        }

        _words = File.ReadAllLines(path)
            .Select(w => w.Trim().ToLower())
            .Where(w => !string.IsNullOrWhiteSpace(w))
            .ToList();

        if (_words.Count == 0)
        {
            throw new InvalidOperationException(
                $"Word file is empty: Data/{category}.txt");
        }
    }

    public string GetRandomWord()
    {
        int index = Random.Shared.Next(_words.Count);
        return _words[index];
    }
}
