using System;
using System.Collections.Generic;
using System.Text;

class BonusCodes
{
    //resources we will need
    private const string CapitalLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string SmallLetters = "abcdefghijklmnopqrstuvwxyz";
    private const string Digits = "0123456789";
    private const string SpecialChars = "@$%";
    public static Random RandomGen = new Random();
    
    static void Main()
    {
        //Let's say you are launching a new game and you need bonus codes or beta-keys to give away
        //Here is a simple generator for those along with some primitive verification

        //We want the codes to be in the pattern bellow:
        // 2 Capital letters - at start or end but not in the middle
        // 2 small letters  - at random positions in the middle
        // 4 digits - at random positions in the middle
        // 0-2 special char at random position that will give extra bonus if present

        StringBuilder code = new StringBuilder();

        //small letters first
        ChooseNumberOfChars(code, 2, SmallLetters);

        //digits
        ChooseNumberOfChars(code, 4, Digits);

        //for the special chars we use RandomGen to see if we have any
        ChooseNumberOfChars(code, RandomGen.Next(3), SpecialChars);

        for (int i = 1; i <= 2; i++)
        {
            //capital letters - they must be at index 0 or at the end
            char capitalLetter = ChooseChar(CapitalLetters);
            int randomPosition = RandomGen.Next(2); // this will give 0 or 1
            if (randomPosition == 0)
            {
                code.Insert(0, capitalLetter);
            }
            else
            {
                code.Append(capitalLetter);
            }
        }

        Console.WriteLine("Our newly generated bonus code is {0}", code);

        //Now we can keep the generated codes in some sort of databade
        //or in a simple List<string>

        List<string> codeDB = new List<string>();

        codeDB.Add(Convert.ToString(code));

        //About the verification - if we have a database we just look up the code

        string codeInput = Convert.ToString(code);
        foreach (string codeOnRecord in codeDB)
        {
            if (codeOnRecord == codeInput)
            {
                //choose appropriate action or output
                int bonusPoints = 1000;

                //if a special char is present
                for (int i = 0; i < codeInput.Length; i++)
			    {
			        switch (codeInput[i])
	                {
		                case '@':
                            bonusPoints += 1000;
                            break;
                        case '$':
                            bonusPoints += 5000;
                            break;
                        case '%':
                            bonusPoints += 10000;
                            break;
                        default:
                            break;
	                }
			    }

                Console.WriteLine("You successfully redeemed a bonus code.");
                Console.WriteLine("Your bonus points are: {0}", bonusPoints);
                codeDB.Remove(codeInput);
                break;
            }
        }

        //Alternatively you can go about de-coding the bonus code to see if it fits the pattern
        //but I don't recommend it since the algorithm can be recreated very easily
    }

    private static void ChooseNumberOfChars(StringBuilder code, int numberOfChars, string chars)
    {
        for (int i = 1; i <= numberOfChars; i++)
        {
            char chosenChar = ChooseChar(chars);
            AddCharToCode(code, chosenChar);
        }
    }

    private static char ChooseChar(string chars)
    {
        int randomIndex = RandomGen.Next(chars.Length);
        char randomChar = chars[randomIndex];
        return randomChar;
    }

    private static void AddCharToCode(StringBuilder code, char character)
    {
        int randomPosition = RandomGen.Next(code.Length + 1);
        code.Insert(randomPosition, character);
    }

    

}

