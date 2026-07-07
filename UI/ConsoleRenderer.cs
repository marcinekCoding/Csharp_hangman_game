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

    public void ShowResult(GameResult result, string word)
    {
        Console.WriteLine();

        if (result == GameResult.win)
        {
            WriteLineColor("  ╔═══════════════════════════════════╗", ConsoleColor.Green);
            WriteLineColor("  ║      CONGRATULATIONS! YOU WIN!      ║", ConsoleColor.Green);
            WriteLineColor("  ╚═══════════════════════════════════╝", ConsoleColor.Green);
            WriteLineColor($"  Word: {word.ToUpper()}", ConsoleColor.Yellow);
        }
        else
        {
            WriteLineColor("  ╔═══════════════════════════════════╗", ConsoleColor.Red);
            WriteLineColor("  ║             GAME OVER               ║", ConsoleColor.Red);
            WriteLineColor("  ╚═══════════════════════════════════╝", ConsoleColor.Red);
            WriteLineColor($"  The word was: {word.ToUpper()}", ConsoleColor.Yellow);
        }

        Console.WriteLine();
        WriteLineColor("  Press Enter to exit...", ConsoleColor.DarkGray);
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
        WriteLineColor(@"
  ██╗    ██╗██╗███████╗██╗███████╗██╗     ███████╗ ██████╗
  ██║    ██║██║██╔════╝██║██╔════╝██║     ██╔════╝██╔════╝
  ██║ █╗ ██║██║███████╗██║█████╗  ██║     █████╗  ██║     
  ██║███╗██║██║╚════██║██║██╔══╝  ██║     ██╔══╝  ██║     
  ╚███╔███╔╝██║███████║██║███████╗███████╗███████╗╚██████╗
   ╚══╝╚══╝ ╚═╝╚══════╝╚═╝╚══════╝╚══════╝╚══════╝ ╚═════╝", ConsoleColor.Cyan);
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
