using System;

public class WisielecGame
{
    string _wordToGuess = "Toyota";
    char[] geussedLetters;


    void WholeGame()
    {
        initGame();

        while (graszDalej())
        {
            podajLitere(char c);
            pokazHaslo();
        }
    }

    void initGame()
    {
        //losowanie slowa 
        string _wordToGuess = "Toyota";
        char[] geussedLetters;
        geussedLetters = new char[_wordToGuess.Length];
        Mistakes miastakes = new Mistakes(5);
        for (int c =0; c<_wordToGuess.Length;c++)
        {
            geussedLetters[c] = '_';
        }
    }

    bool graszDalej()
    {
        if (slowa_te_same()) return false;
        if (mistakes.is_mistakes_left())
        {
            return true;
        }

    }

    bool slowa_te_same()
    {
        int i = 0;
        foreach (var c in geussedLetters)
        {
            if (_wordToGuess[i] != c)
            {
                return false;
            }
            i++;
        }
        return true;
    }

    void podajLitere(char c)
    {
        int sizer = _wordToGuess.Length;
        bool czy_cos_zgadniete = false;
        for (int i = 0; i < sizer; i++)
        {
            if (c == _wordToGuess[i])
            {
                czy_cos_zgadniete = true;
                geussedLetters[i] == c;
            }
        }
        if (!czy_cos_zgadniete)
        {
            Console.WriteLine("Nie ma takiej litery");
        }
    }

    void pokazHaslo()
    {
        foreach (var c in geussedLetters)
        {
            Console.WriteLine(c);

        }
    }





};