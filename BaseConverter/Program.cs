using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseConverter
{
	//*************************************************
	//Title:				Base Converter
	//Application Type:		Console App
	//Description:			Convert numbers to new bases
	//Author:				Lochinski, Jakob R
	//Date Created:			11/30/2018
	//Date Revised:			12/01/2018
	//*************************************************      
	class Program
	{
		enum LetterChoice
		{
			A,
			B,
			C,
			D,
			Q
		}
		static void Main(string[] args)
		{
			DisplayWelcomeScreen();

			DisplayMainMenu();

			DisplayClosingScreen();
		}
		static void DisplayMainMenu()
		{
			bool runApp = true;
			int userNumber = 0;
			int newBase = 0;


			do
			{
				string menuChoice;
				LetterChoice letterChoice;

				DisplayHeader("Main Menu");
				Console.WriteLine("A) Enter Number");
				Console.WriteLine("B) Enter New Base");
				Console.WriteLine("C) Calculate");
				Console.WriteLine("Q) Quit");
				menuChoice = Console.ReadLine().ToUpper();
				LetterChoice.TryParse(menuChoice, out letterChoice); 



				switch (letterChoice)
				{
					case LetterChoice.A:
						userNumber = DisplayGetUserNumber(); ;
						break;
					case LetterChoice.B:
						newBase = DisplayChooseNewBase();
						break;
					case LetterChoice.C:
						DisplayCalculate(userNumber, newBase);
						break;
					case LetterChoice.Q:
						runApp = false;
						break;
					default:
						RedText("Please enter A, B, C, or Q");
						DisplayContinuePrompt();
						break;
				}

			} while (runApp);
		}

		private static void DisplayCalculate(int userNumber, int newBase)
		{
			DisplayHeader("Final Calculation");
			string newNumber;

			newNumber = Convert.ToString(userNumber,newBase); 
			
			Console.WriteLine($"The number {userNumber} in base 10 is {newNumber} in base {newBase}.");
			DisplayContinuePrompt();
		}

		private static int DisplayChooseNewBase()
		{
			DisplayHeader("Choose New Base");
			int newBase = 0;
			bool validResponse;

			Console.WriteLine("Now for the last step! Choose the base that you want to convert your number to.");
			Console.Write("Enter new base:");
			do
			{
				string menuChoice;
				LetterChoice letterChoice;
				validResponse = true;
				
				DisplayHeader("Main Menu");
				Console.WriteLine("A) Base 2");
				Console.WriteLine("B) Base 8");
				Console.WriteLine("C) Base 16");
				menuChoice = Console.ReadLine().ToUpper();
				LetterChoice.TryParse(menuChoice, out letterChoice);

				switch (letterChoice)
				{
					case LetterChoice.A:
						newBase = 2;
						break;
					case LetterChoice.B:
						newBase = 8;
						break;
					case LetterChoice.C:
						newBase = 16;
						break;
					default:
						validResponse = false;
						RedText("Please enter A, B, or C ");
						DisplayContinuePrompt();
						break;
				}

			} while (!validResponse);

			return newBase;
		}

		private static int DisplayGetUserNumber()
		{
			int userNumber = 0;
			string response;

			do
			{
				DisplayHeader("Choose Number");
				

				Console.WriteLine("Choose a number in base 10");
				Console.Write("Enter Number:");
				response = Console.ReadLine();
				userNumber = DoubleValidation(response);
			} while (!int.TryParse(response, out userNumber));
				

			return userNumber;
		}
		

		#region WELCOMECLOSING
		static void DisplayWelcomeScreen()
		{
			Console.Clear();
			DisplayHeader("Welcome");
			Console.WriteLine("In this application the user will convert a number from base 10 to base 2, 8, or 16");
			DisplayContinuePrompt();
		}

		static void DisplayClosingScreen()
		{
			DisplayHeader("Closing Screen");
			Console.WriteLine("Thank You for using my Application!");
			Console.WriteLine();
			Console.WriteLine("Press any key to exit");
			Console.ReadKey();
		}
		#endregion

		#region HELPERS
		static void DisplayHeader(string headerText)
		{
			Console.Clear();
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("\t\t" + headerText);
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine();
		}
		static void DisplayContinuePrompt()
		{
			Console.WriteLine();
			Console.WriteLine("Press any key to continue.");
			Console.ReadKey();
		}
		static void DisplaySpecificContinuePrompt(string text)
		{
			Console.WriteLine();
			Console.WriteLine($"Press any key to {text}");
			Console.ReadKey();
		}
		static void DisplayRedContinuePrompt()
		{
			Console.WriteLine();
			RedText("Press any key to continue.");
			Console.ReadKey();
		}
		static void RedText(string text)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(text);
			Console.ForegroundColor = ConsoleColor.White;
		}
		static int DoubleValidation(string userResponse)
		{
			int userNumber = 0;

				if (!int.TryParse(userResponse, out userNumber))
				{
					RedText("Please enter a number");
					DisplayRedContinuePrompt();
				}
			return userNumber;
		}
		#endregion
	}
}
