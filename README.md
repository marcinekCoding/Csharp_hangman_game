![Hangman Game — welcome screen](screenshots/main.png)

# Hangman Game

Console hangman written in **C#** and **.NET**. Guess the hidden word letter by letter — you have a limited number of wrong tries before the game ends.

> Semester project · C# learning · clean project structure

---

## Screenshots

| Welcome screen | Gameplay |
|:---:|:---:|
| ![Welcome](screenshots/main.png) | ![Gameplay](screenshots/image%20copy.png) |

---

## Features

- **ASCII UI** with colors — banner, gallows, word box, mistake tracker
- **Random words** loaded from category files in `Data/`
- **6 categories** — math, countries, cities, movies, football, general
- **Wrong letters panel** — shows all incorrect letters used in current round
- **Duplicate-letter protection** — repeated letter does not consume another mistake
- **Play again** — start a new round after win/loss
- **Game state** — win / lose / in progress (`GameResult`)
- **Mistake limit** — configurable wrong guesses (`Mistakes`)
- **Separated concerns** — logic, UI, and data in different folders

---

## How to play

1. Run the game (`dotnet run`).
2. Press **Enter** on the welcome screen.
3. Pick a **category** (1–6).
4. Type **one letter** and confirm with Enter.
5. Guess the whole word before you run out of mistakes.
6. Track your wrong guesses in the **Wrong letters** line.
7. If you type the same letter again, the game warns you and does not add a mistake.
8. Press **Enter** after the result to play again.

---

## Requirements

- [.NET SDK 10.0](https://dotnet.microsoft.com/download) or newer

Check your version:

```bash
dotnet --version
```

---

## Quick start

```bash
git clone <your-repo-url>
cd wisielec_game
dotnet run
```

Build only:

```bash
dotnet build
```

---

## Project structure

```
wisielec_game/
├── Data/
│   ├── WordRepo.cs       # loads words from file, random pick
│   ├── words.txt         # general words
│   ├── math.txt          # math terms
│   ├── panstwa.txt       # countries
│   ├── miasta.txt        # cities
│   ├── filmy.txt         # movies
│   └── football.txt      # players & clubs
├── Game/
│   └── wisielec_engine.cs  # game logic (Hangman engine)
├── Models/
│   ├── GameResult.cs     # win / lost / in_progress
│   └── Mistakes.cs       # mistake counter
├── UI/
│   └── ConsoleRenderer.cs  # console display & input
├── screenshots/
│   ├── main.png
│   └── image copy.png
├── Program.cs              # entry point
└── Wisielec.csproj
```

### Categories

| Option | File | Content |
|--------|------|---------|
| 1 | `math.txt` | Math terms |
| 2 | `panstwa.txt` | Countries |
| 3 | `miasta.txt` | Cities |
| 4 | `filmy.txt` | Movies |
| 5 | `football.txt` | Famous players & football clubs |
| 6 | `words.txt` | General (default) |

| Layer | Responsibility |
|-------|----------------|
| `Game/` | Rules: letters, win/lose, game loop |
| `UI/` | Display only: banner, gallows, prompts |
| `Data/` | Words: load file, `GetRandomWord()` |
| `Models/` | Small types shared across the app |

---

## Adding new words

Edit the matching file in `Data/` — **one word per line**, lowercase, no spaces:

```text
messi
realmadrid
liverpool
```

Available files: `words.txt`, `math.txt`, `panstwa.txt`, `miasta.txt`, `filmy.txt`, `football.txt`.

After `dotnet build`, the file is copied to the output folder automatically (`Wisielec.csproj`).

---

## Tech stack

- **Language:** C# 13 / .NET 10
- **Type:** Console application
- **Architecture:** simple layered structure (no external packages)

---

## Author

**Marcin Pawlak** — C# learning project (SEM 3)

---

## License

Educational project — free to use for learning.
