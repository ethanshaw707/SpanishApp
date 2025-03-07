namespace SpanishAppGame
{
using System;
using System.Collections.Generic;

public class Crossword{
    static Dictionary<string, string> crosswordWords = new Dictionary<string, string>(){
        {"dog","perro"},
        {"cat","gato"},
        {"house","casa"},
        {"book","libro"},
        {"car", "coche"}
    };

    public void Play(){
        Console.Clear();
        Console.WriteLine("Welcome to the Spanish Crossword");
        Console.WriteLine("Enter the Spanish word for the following English word");

        foreach (var word in crosswordWords.Keys){
            Console.Write($" What is '{word}' in Spanish?");
            string? answer = Console.ReadLine()?.Trim().ToLower();

            if (answer == crosswordWords[word]){
                Console.WriteLine("Correct!");
            } else {
                Console.WriteLine($"Incorrect. The correct answer is '{crosswordWords[word]}'");
            }
        }

        Console.WriteLine("You have completed the crossword! Press Enter to return to the menu.");
        Console.ReadLine();
    }
}
}