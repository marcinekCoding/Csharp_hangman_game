public class WordRepo
{
    private readonly List<string> _words;

    public WordRepo()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "Data", "words.txt");
        _words = File.ReadAllLines(path).Select(w => w.Trim().ToLower())
          .Where(w => !string.IsNullOrWhiteSpace(w))
          .ToList();

    }

    public string GetRandomWord()
    {
        int index = Random.Shared.Next(_words.Count);
        return _words[index];
    }
};