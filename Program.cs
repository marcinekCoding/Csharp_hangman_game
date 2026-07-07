try
{
    WisielecGame gra = new WisielecGame();
    gra.WholeGame();
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine();
    Console.WriteLine("  Game crashed:");
    Console.WriteLine($"  {ex.Message}");
    Console.ResetColor();
    Console.WriteLine();
    Console.WriteLine("  Press Enter to exit...");
    Console.ReadLine();
}
