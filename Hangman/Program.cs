using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> capitals = File.ReadLines("countries_and_capitals.txt")
                .Select(x => x.Split('|'))
                .ToDictionary(x => x[0], x => x[1]);

            bool playAgain = true;
            while (playAgain)
            {
                Random random = new Random();

                int index = random.Next(capitals.Count);
                KeyValuePair<string, string> pair = capitals.ElementAt(index);
                Console.WriteLine("                            GRA W WISIELCA - PAŃSTWA I ICH STOLICE - WERSJA ANGLOJĘZYCZNA ");
                string secretWord = pair.Value.ToUpper();
                secretWord = secretWord.Substring(1);

                string puzzle = "";
                for (int i = 0; i < secretWord.Length; i++)
                {
                    puzzle = puzzle + "_ ";
                }
                puzzle = puzzle.Trim();

                var puzzleArray = puzzle.ToCharArray();

                int spaceIndex = secretWord.IndexOf(" ");
                while (spaceIndex > -1)
                {
                    puzzleArray[spaceIndex * 2] = Convert.ToChar(" ");
                    spaceIndex = secretWord.IndexOf(" ", spaceIndex + 1);
                }

                int lives = 5;
                List<string> wrongLetterList = new List<string>();
                while (new string(puzzleArray).Contains("_") && lives > 0)
                {

                    Console.WriteLine();
                    Console.WriteLine(puzzleArray);
                    Console.WriteLine();
                    Console.Write("Your letter: ");

                    string guessLetter = Console.ReadLine().ToUpper();

                    List<int> secretIndexList = new List<int>();
                    if (secretWord.Contains(guessLetter))
                    {
                        int secretIndex = secretWord.IndexOf(guessLetter);
                        while (secretIndex >= 0)
                        {
                            secretIndexList.Add(secretIndex);
                            secretIndex = secretWord.IndexOf(guessLetter, secretIndex + 1);
                        }
                        foreach (var item in secretIndexList)
                        {
                            if (item == 0)
                            {
                                puzzleArray[0] = Convert.ToChar(guessLetter);
                            }
                            else
                            {
                                puzzleArray[item * 2] = Convert.ToChar(guessLetter);
                            }
                        }
                    }
                    else
                    {
                        wrongLetterList.Add(guessLetter);
                        lives--;

                        Console.Write("Wrong letter. You have " + lives + " live(s). Not-in-word-letter: ");

                        foreach (var item in wrongLetterList)
                        {
                            Console.Write(item + " ");
                        }
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
                if (lives == 0)
                {
                    Console.WriteLine("GAME OVER");
                }
                else
                {
                    Console.WriteLine("YOU ARE WINNER!!! " + pair.Value + " is the capital of " + pair.Key);
                }

                Console.WriteLine("Do you want to play again? write Y  or  N");
                string loop = Console.ReadLine().ToUpper();

                if (loop == "Y")
                {
                    Console.Clear();
                }
                else
                {
                    playAgain = false;
                }
            }
        }
    }
}
