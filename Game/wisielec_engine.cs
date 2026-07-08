using System;

public class WisielecGame
{
    private const int MaxMistakes = 10;

    private string _wordToGuess = "";
    private char[] _guessedLetters = Array.Empty<char>();
    private char[] _triedLetters = Array.Empty<char>();
    private Mistakes _mistakes = null!;
    private GameResult _gameResult;
    private readonly ConsoleRenderer _renderer = new();
    private WordRepo _wordRepo = null!;

    private string _statusMessage = "";
    private ConsoleColor _statusColor = ConsoleColor.Gray;

    public void WholeGame()
    {
        _renderer.ShowWelcome(MaxMistakes);
        string category = _renderer.AskForCategory();

        while (true)
        {
            PlayRound(category);
            _renderer.ShowResult(_gameResult, _wordToGuess);
        }
    }

    void PlayRound(string category)
    {
        InitGame(category);

        while (_gameResult == GameResult.in_progress)
        {
            _renderer.RenderGame(
                _guessedLetters,
                _triedLetters,
                _mistakes.mistakes_made,
                MaxMistakes,
                _statusMessage,
                _statusColor);

            char letter = _renderer.AskForLetter();
            SprawdzLitere(letter);
            SprawdzStatus();
        }

        _renderer.RenderGame(
            _guessedLetters,
            _triedLetters,
            _mistakes.mistakes_made,
            MaxMistakes,
            _statusMessage,
            _statusColor);
    }

    void InitGame(string category)
    {
        _gameResult = GameResult.in_progress;
        _wordRepo = new WordRepo(category);
        _wordToGuess = _wordRepo.GetRandomWord();
        _guessedLetters = new char[_wordToGuess.Length];
        _triedLetters = new char[10];
        _mistakes = new Mistakes(MaxMistakes);
        _statusMessage = "Good luck!";

        for (int i = 0; i < _wordToGuess.Length; i++)
            _guessedLetters[i] = '_';
    }

    bool SlowaTeSame()
    {
        for (int i = 0; i < _wordToGuess.Length; i++)
        {
            if (_wordToGuess[i] != _guessedLetters[i])
                return false;
        }
        return true;
    }

    void SprawdzLitere(char c)
    {
        if (CzyLiteraBylaUzyta(c))
        {
            _statusMessage = $"Letter '{char.ToUpper(c)}' was already used.";
            _statusColor = ConsoleColor.Yellow;
            return;
        }

        bool czyCosZgadniete = false;

        for (int i = 0; i < _wordToGuess.Length; i++)
        {
            if (c == _wordToGuess[i])
            {
                czyCosZgadniete = true;
                _guessedLetters[i] = c;
            }
        }

        if (czyCosZgadniete)
        {
            _statusMessage = $"Letter '{char.ToUpper(c)}' is in the word!";
            _statusColor = ConsoleColor.Green;
        }
        else
        {
            _triedLetters[_mistakes.mistakes_made] = c;
            _mistakes.add_mistake();
            _statusMessage = $"Letter '{char.ToUpper(c)}' is not in the word.";
            _statusColor = ConsoleColor.Red;
        }
    }

    bool CzyLiteraBylaUzyta(char c)
    {
        for (int i = 0; i < _guessedLetters.Length; i++)
        {
            if (_guessedLetters[i] == c)
                return true;
        }

        for (int i = 0; i < _triedLetters.Length; i++)
        {
            if (_triedLetters[i] == c)
                return true;
        }

        return false;
    }

    void SprawdzStatus()
    {
        if (!_mistakes.is_mistakes_left())
        {
            _gameResult = GameResult.lost;
            _statusMessage = "You used all your wrong guesses.";
            _statusColor = ConsoleColor.Red;
        }

        if (SlowaTeSame())
        {
            _gameResult = GameResult.win;
            _statusMessage = "You guessed the whole word!";
            _statusColor = ConsoleColor.Green;
        }
    }
}
