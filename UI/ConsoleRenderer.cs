using System;

public class ConsoleRenderer
{
    private static readonly string[][] HangmanStages =
    {
        new[]
        {
            "  +---+",
            "  |   |",
            "      |",
            "      |",
            "      |",
            "      |",
            "========="
        },
        new[]
        {
            "  +---+",
            "  |   |",
            "  O   |",
            "      |",
            "      |",
            "      |",
            "========="
        },
        new[]
        {
            "  +---+",
            "  |   |",
            "  O   |",
            "  |   |",
            "      |",
            "      |",
            "========="
        },
        new[]
        {
            "  +---+",
            "  |   |",
            "  O   |",
            " /|   |",
            "      |",
            "      |",
            "========="
        },
        new[]
        {
            "  +---+",
            "  |   |",
            "  O   |",
            " /|\\  |",
            "      |",
            "      |",
            "========="
        },
        new[]
        {
            "  +---+",
            "  |   |",
            "  O   |",
            " /|\\  |",
            " /    |",
            "      |",
            "========="
        },
        new[]
        {
            "  +---+",
            "  |   |",
            "  O   |",
            " /|\\  |",
            " / \\  |",
            "      |",
            "========="
        }
    };

    public void ShowWelcome(int maxMistakes)
    {
        Console.Clear();
        WriteBanner();
        WriteLineColor("  Guess the hidden word, one letter at a time.", ConsoleColor.Gray);
        WriteLineColor($"  You have {maxMistakes} wrong guesses allowed.", ConsoleColor.Gray);
        Console.WriteLine();
        WriteLineColor("  Press Enter to start...", ConsoleColor.DarkGray);
        Console.ReadLine();
    }

    public void RenderGame(
        char[] guessedLetters,
        int mistakesMade,
        int maxMistakes,
        string statusMessage,
        ConsoleColor statusColor)
    {
        Console.Clear();
        WriteBanner();

        int stage = Math.Min(mistakesMade, HangmanStages.Length - 1);
        string[] art = HangmanStages[stage];

        Console.WriteLine();
        foreach (string line in art)
        {
            WriteLineColor("  " + line, mistakesMade >= maxMistakes ? ConsoleColor.Red : ConsoleColor.White);
        }

        Console.WriteLine();
        WriteLineColor("  ┌─────────────────────────────────────┐", ConsoleColor.DarkCyan);
        WriteLineColor("  │  WORD                               │", ConsoleColor.Cyan);
        WriteLineColor("  ├─────────────────────────────────────┤", ConsoleColor.DarkCyan);

        Console.Write("  │  ");
        for (int i = 0; i < guessedLetters.Length; i++)
        {
            char letter = guessedLetters[i];
            if (letter == '_')
                WriteColor("_ ", ConsoleColor.DarkGray);
            else
                WriteColor($"{char.ToUpper(letter)} ", ConsoleColor.Yellow);
        }
        Console.WriteLine("                             │");

        WriteLineColor("  └─────────────────────────────────────┘", ConsoleColor.DarkCyan);
        Console.WriteLine();

        RenderMistakeBar(mistakesMade, maxMistakes);

        if (!string.IsNullOrEmpty(statusMessage))
        {
            Console.WriteLine();
            WriteLineColor($"  » {statusMessage}", statusColor);
        }

        Console.WriteLine();
    }

    public char AskForLetter()
    {
        while (true)
        {
            Console.Write("  Enter a letter: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string? input = Console.ReadLine();
            Console.ResetColor();

            if (string.IsNullOrWhiteSpace(input) || input.Length != 1 || !char.IsLetter(input[0]))
            {
                WriteLineColor("  ! Enter exactly one letter.", ConsoleColor.Red);
                continue;
            }

            return char.ToLower(input[0]);
        }
    }

    public string AskForCategory()
    {
        Console.Clear();
        WriteBanner();
        Console.WriteLine();
        ShowCategoryMenu();

        while (true)
        {
            Console.Write("  Choose option (1-5): ");
            Console.ForegroundColor = ConsoleColor.Green;
            string? input = Console.ReadLine();
            Console.ResetColor();

            if (!int.TryParse(input, out int choice) || choice < 1 || choice > 5)
            {
                WriteLineColor("  ! Invalid choice. Pick a number from 1 to 5.", ConsoleColor.Red);
                continue;
            }

            return choice switch
            {
                1 => "math",
                2 => "panstwa",
                3 => "miasta",
                4 => "filmy",
                _ => "words"
            };
        }
    }

    private static void ShowCategoryMenu()
    {
        WriteBox("CHOOSE CATEGORY", ConsoleColor.Cyan);
        Console.WriteLine();

        WriteBox("1  MATH", ConsoleColor.DarkCyan);
        WriteBox("2  COUNTRIES", ConsoleColor.DarkCyan);
        WriteBox("3  CITIES", ConsoleColor.DarkCyan);
        WriteBox("4  MOVIES", ConsoleColor.DarkCyan);
        WriteBox("5  GENERAL", ConsoleColor.DarkCyan);

        Console.WriteLine();
    }

    private static void WriteBox(string text, ConsoleColor color)
    {
        const int width = 35;
        string padded = text.Length >= width ? text[..width] : text.PadLeft((width + text.Length) / 2).PadRight(width);

        WriteLineColor($"  ╔{new string('═', width)}╗", color);
        WriteLineColor($"  ║{padded}║", color);
        WriteLineColor($"  ╚{new string('═', width)}╝", color);
    }

    public void ShowResult(GameResult result, string word)
    {
        Console.WriteLine();

        if (result == GameResult.win)
        {
            WriteLineColor("  ╔═══════════════════════════════════╗", ConsoleColor.Green);
            WriteLineColor("  ║      CONGRATULATIONS! YOU WIN!    ║", ConsoleColor.Green);
            WriteLineColor("  ╚═══════════════════════════════════╝", ConsoleColor.Green);
            WriteLineColor($"  Word: {word.ToUpper()}", ConsoleColor.Yellow);
        }
        else
        {
            WriteLineColor("  ╔═══════════════════════════════════╗", ConsoleColor.Red);
            WriteLineColor("  ║             GAME OVER             ║", ConsoleColor.Red);
            WriteLineColor("  ╚═══════════════════════════════════╝", ConsoleColor.Red);
            WriteLineColor($"  The word was: {word.ToUpper()}", ConsoleColor.Yellow);
        }

        Console.WriteLine();
        WriteLineColor("  Press Enter to play again...", ConsoleColor.DarkGray);
        Console.ReadLine();
        Console.ResetColor();
    }

    private static void RenderMistakeBar(int mistakesMade, int maxMistakes)
    {
        int remaining = Math.Max(0, maxMistakes - mistakesMade);

        Console.Write("  Mistakes: ");
        for (int i = 0; i < maxMistakes; i++)
        {
            if (i < mistakesMade)
                WriteColor("■ ", ConsoleColor.Red);
            else
                WriteColor("□ ", ConsoleColor.DarkGray);
        }

        Console.Write("   ");
        WriteColor($"({mistakesMade}/{maxMistakes})", ConsoleColor.Gray);
        Console.Write("   Remaining: ");
        WriteColor(remaining.ToString(), remaining <= 1 ? ConsoleColor.Red : ConsoleColor.Green);
        Console.WriteLine();
    }

    private static void WriteBanner()
    {
        string[] hangman =
        {
            "██╗  ██╗ █████╗ ███╗   ██╗ ██████╗ ███╗   ███╗ █████╗ ███╗   ██╗",
            "██║  ██║██╔══██╗████╗  ██║██╔════╝ ████╗ ████║██╔══██╗████╗  ██║",
            "███████║███████║██╔██╗ ██║██║  ███╗██╔████╔██║███████║██╔██╗ ██║",
            "██╔══██║██╔══██║██║╚██╗██║██║   ██║██║╚██╔╝██║██╔══██║██║╚██╗██║",
            "██║  ██║██║  ██║██║ ╚████║╚██████╔╝██║ ╚═╝ ██║██║  ██║██║ ╚████║",
            "╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝"
        };

        string[] game =
        {
            " ██████╗  █████╗ ███╗   ███╗███████╗",
            "██╔════╝ ██╔══██╗████╗ ████║██╔════╝",
            "██║  ███╗███████║██╔████╔██║█████╗",
            "╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗"
        };

        const int gameStartColumn = 68;

        for (int i = 0; i < hangman.Length; i++)
        {
            Console.Write("  ");
            WriteColor(hangman[i], ConsoleColor.Cyan);

            int gameIndex = i - (hangman.Length - game.Length);
            if (gameIndex >= 0)
            {
                int padding = gameStartColumn - (2 + hangman[i].Length);
                Console.Write(new string(' ', Math.Max(2, padding)));
                WriteColor(game[gameIndex], ConsoleColor.DarkYellow);
            }

            Console.WriteLine();
        }
    }

    private static void WriteLineColor(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    private static void WriteColor(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }
}
