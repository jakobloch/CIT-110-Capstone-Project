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
		enum Operations
		{
			ADD,
			SUBTRACT,
			MULTIPLY,
			DIVIDE
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
				Console.WriteLine("C) Convert");
				Console.WriteLine("D) Calculator");
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
						DisplayConvert(userNumber, newBase);
						break;
					case LetterChoice.D:
						DisplayCalculator();
						break;
					case LetterChoice.Q:
						runApp = false;
						break;
					default:
						RedText("Please enter A, B, C, or Q");
						DisplayRedContinuePrompt();
						break;
				}

			} while (runApp);
		}

		private static void DisplayCalculator()
		{
			bool runCalculator = true;
			int[] operands = new int[2];
			int newBase;
			Operations currentOperation;
			string operationSymbol;
			int answer;

			do
			{
				operands = DisplayGetOperands();
				newBase = DisplayChooseNewBase();
				currentOperation = DisplayGetOperation();
				operationSymbol = GetOperationSymbol(currentOperation);
				answer = PerformOperation(currentOperation, operands);
				DisplayCalculation(operands, currentOperation, operationSymbol, answer, newBase);
				runCalculator =  DisplayGetQuitResponse();
			} while (runCalculator);
			
		}

		static bool DisplayGetQuitResponse()
		{
			bool validResponse = false;
			string userResponse;
			bool runCalculator = true;


			while (!validResponse)
			{
				DisplayHeader("Calculator");

				Console.Write("Would you like to perform another calculation (YES or NO):");
				userResponse = Console.ReadLine().ToUpper();

				if (!(userResponse == "NO" || userResponse == "YES"))
				{
					RedText("You must enter either 'YES' or 'NO'.");
					DisplayRedContinuePrompt();
				}
				else
				{
					if (userResponse == "NO")
					{
						runCalculator = false;
					}

					validResponse = true;
				}
			}
			return runCalculator;
		}

		static void DisplayCalculation(int[] numbers, Operations operation, string operationSymbol, int answer, int newBase)
		{
			string answerString = "";
			string newNum;
			string newNum2;

			DisplayHeader("Calculation");
			newNum = Convert.ToString(numbers[0], newBase);
			newNum2 = Convert.ToString(numbers[1], newBase);
			answerString = Convert.ToString(answer, newBase);



			if (operation == Operations.DIVIDE && numbers[1] == 0)
			{
				RedText("Dividing by zero is not allowed.");
			}
			else
			{
				Console.WriteLine($"\tAnswer: {newNum} {operationSymbol} {newNum2} = {answerString}");
			}

			DisplayContinuePrompt();
		}

		static int PerformOperation(Operations operation, int[] operands)
		{
			int answer = 0;

			switch (operation)
			{
				case Operations.ADD:
					answer = operands[0] + operands[1];
					break;

				case Operations.SUBTRACT:
					answer = operands[0] - operands[1];
					break;

				case Operations.MULTIPLY:
					answer = operands[0] * operands[1];
					break;

				case Operations.DIVIDE:
					answer = operands[0] / operands[1];
					break;
				default:
					
					break;
			}
			return answer;
		}

		static string GetOperationSymbol(Operations currentOperation)
		{
			string operationSymbol = "none";

			switch (currentOperation)
			{
				case Operations.ADD:
					operationSymbol = "+";
					break;

				case Operations.SUBTRACT:
					operationSymbol = "-";
					break;

				case Operations.MULTIPLY:
					operationSymbol = "*";
					break;

				case Operations.DIVIDE:
					operationSymbol = "/";
					break;
			}
			return operationSymbol;
		}

		static Operations DisplayGetOperation()
		{
			string userReponse;
			Operations operation;

			do
			{
				DisplayHeader("Enter Operation");
				Console.Write("Enter the operation (Add, Subtract, Multiply, or Divide):");
				userReponse = Console.ReadLine();
				Operations.TryParse(userReponse.ToUpper(), out operation);
				Console.WriteLine();

				if (!Operations.TryParse(userReponse.ToUpper(), out operation))
				{
					RedText("Please enter a valid operation (Add, Subtract, Multiply, or Divide)");
					Console.ReadKey();
				}
			} while (!Operations.TryParse(userReponse.ToUpper(), out operation));

			return operation;
		}

		static int[] DisplayGetOperands()
		{
			bool validResponse = false;
			string userResponse;
			int[] numbers = new int[2];


			while (!validResponse)
			{
				for (int index = 0; index < numbers.Length; index++)
				{
					do
					{
						DisplayHeader("Enter operands");

						Console.Write($"Enter operand {index + 1} in base 10:");
						userResponse = Console.ReadLine();

						if (!int.TryParse(userResponse, out numbers[index]))
						{
							RedText("You must enter a number");
							DisplayRedContinuePrompt();
						}
						else
						{
							validResponse = true;
						}
					} while (!int.TryParse(userResponse, out numbers[index]));

				}

			}

			return numbers;
		}

		private static void DisplayConvert(int userNumber, int newBase)
		{
			DisplayHeader("Conversion");
			string newNumber;

			newNumber = Convert.ToString(userNumber,newBase);
			userNumber.ToString("N");
			
			Console.WriteLine($"The number {userNumber} in base 10 is {newNumber} in base {newBase}.");
			DisplayContinuePrompt();
		}

		private static int DisplayChooseNewBase()
		{
			int newBase = 0;
			bool validResponse;

			do
			{
				string menuChoice;
				LetterChoice letterChoice;
				validResponse = true;

				DisplayHeader("Choose New Base");

				Console.Write("Enter new base:");
				Console.WriteLine();
				Console.WriteLine("A) Base 2");
				Console.WriteLine("B) Base 8");
				Console.WriteLine("C) Base 10");
				Console.WriteLine("D) Base 16");
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
						newBase = 10;
						break;
					case LetterChoice.D:
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
			Console.WriteLine("In this application the user will convert a number from base 10 to base 2, 8, 10, or 16");
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
