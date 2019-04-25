using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterMind.NumberGuessGame.ConsoleApp
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Input 4 digit number and see if your guess matches system generated number and hit enter. ");
            var systemGeneratedNumberDigits = GenerateFourDigitNumber().ToCharArray();
            var inputNumberText = Console.ReadLine();
            var attemptsIndex = 1;

            while (attemptsIndex <= 10)
            {
                var attemptSuccess = CheckInputNumberMatchesGeneratedNumber(inputNumberText, systemGeneratedNumberDigits);
                if (attemptSuccess)
                {
                    Console.WriteLine("You guessed it right and that's a win, end of the game!!");
                    Console.Read();
                    break;
                }

                Console.WriteLine($"Failed attempt {attemptsIndex}, try again!!");

                if (attemptsIndex == 10)
                {
                    Console.WriteLine();
                    Console.WriteLine("Sorry, none of the attempts exactly matched system generated number and so you lost, end of the game!!");
                    break;
                }

                inputNumberText = Console.ReadLine();

                attemptsIndex++;
            }

            Console.Read();
        }

        public static string GenerateFourDigitNumber()
        {
            var randomNumberInstance = new Random();
            return $"{randomNumberInstance.Next(1, 7)}{randomNumberInstance.Next(1, 7)}{randomNumberInstance.Next(1, 7)}{randomNumberInstance.Next(1, 7)}";
        }

        public static bool CheckInputNumberMatchesGeneratedNumber(string inputNumberText, char[] systemGeneratedNumberDigits)
        {
            if (!int.TryParse(inputNumberText, out _))
            {
                Console.Write("Input is not a number - ");
                return false;
            }
            var inputNumberDigitsArray = inputNumberText.ToCharArray();
            if (inputNumberDigitsArray.Length != 4)
            {
                Console.Write("Input number is not 4 digit value - ");
                return false;
            }
            List<char> matchedDigitWithPositionValues = new List<char>(), matchedDigitOnlyValues = new List<char>(), nonMatchedDigitValues = new List<char>();
            for (var j = 0; j < 4; j++)
            {
                if (inputNumberDigitsArray[j] == systemGeneratedNumberDigits[j])
                {
                    matchedDigitWithPositionValues.Add('+');
                }
                else if (systemGeneratedNumberDigits.Any(x => x == inputNumberDigitsArray[j]))
                {
                    matchedDigitOnlyValues.Add('-');
                }
                else
                {
                    nonMatchedDigitValues.Add(' ');
                }
            }

            if (matchedDigitWithPositionValues.Count == 4)
            {
                return true;
            }

            Console.Write($"{new string(matchedDigitWithPositionValues.ToArray())}{new string(matchedDigitOnlyValues.ToArray())}{new string(nonMatchedDigitValues.ToArray())}");

            return false;
        }
    }
}
