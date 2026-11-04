namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. From Desyatkova (10) to Dviykova (2)");
                Console.WriteLine("2. From Dviykova (2) to Desyatkova (10)");
                Console.WriteLine("3. From Desyatkova (10) to Shistnadtsiatkovaya (16)");
                Console.WriteLine("4. From Shistnadtsiatkovaya (16) to Desyatkova (10)");
                Console.WriteLine("0. Exit");
                Console.Write("Choose the conversion direction: ");

                string choice = Console.ReadLine();

                if (choice == "0") break;

                switch (choice)
                {
                    case "1":
                        ConvertFromDecimal(2);
                        break;
                    case "2":
                        ConvertToDecimal(2);
                        break;
                    case "3":
                        ConvertFromDecimal(16);
                        break;
                    case "4":
                        ConvertToDecimal(16);
                        break;
                    default:
                        Console.WriteLine("Error: Invalid menu choice. Please try again.");
                        break;
                }

                Console.Write("Enter the number in English (from zero to nine): ");
                string input = Console.ReadLine();

                WordToDigit(input);
            }

            try
            {
                Console.WriteLine("--- Creating a Valid Passport ---");
                ForeignPassport validPassport = new ForeignPassport(
                    "Patrol Ivan Petrovich",
                    "USR123456",
                    new DateTime(2024, 5, 12)
                );
                validPassport.PrintPassportInfo();

                Console.WriteLine("\n--- Test 1: Error in Full Name (empty string) ---");
                ForeignPassport badPassport1 = new ForeignPassport("", "USR123456", new DateTime(2024, 5, 12));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }

            try
            {
                Console.WriteLine("\n--- Test 2: Error in Passport Number ---");
                ForeignPassport badPassport2 = new ForeignPassport("John Doe", "123", new DateTime(2024, 5, 12));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }

            try
            {
                Console.WriteLine("\n--- Test 3: Error in Date (future date) ---");
                ForeignPassport badPassport3 = new ForeignPassport("John Doe", "USR123456", new DateTime(2028, 1, 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }

            try
            {
                Console.WriteLine("1. Opening the rakhunka");
                BankAccount account = new BankAccount("UA1234567890", 1000.50);
                account.PrintAccountInfo();

                Console.WriteLine("\n2. Depositing funds");
                account.Deposit(500.0);
                account.PrintAccountInfo();

                Console.WriteLine("\n3. Withdrawing funds");
                account.Withdraw(300.50);
                account.PrintAccountInfo();

                Console.WriteLine("\n4. Testing: Attempting to withdraw more than available");
                account.Withdraw(2000.0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }

            try
            {
                Console.WriteLine("\n--- 5. Testing: Attempting to deposit a negative amount ---");
                BankAccount badAccount = new BankAccount("UA000000", 100);
                badAccount.Deposit(-50.0); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }
        
        static void ConvertFromDecimal(int toBase)
        {
            Console.Write($"Enter an integer in the decimal system (int range): ");
            string input = Console.ReadLine();

            try
            {
                int number = Convert.ToInt32(input);

                string result = Convert.ToString(number, toBase);

                Console.WriteLine($"Result ({toBase}-based system): {result.ToUpper()}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Invalid input. Please enter a valid integer.");
            }
            catch (OverflowException)
            {
                Console.WriteLine($"Error: The number is too large or too small. It exceeds the range of int ({int.MinValue} .. {int.MaxValue}).");
            }
        }

        static void ConvertToDecimal(int fromBase)
        {
            Console.Write($"Enter a number in the {fromBase}-based system: ");
            string input = Console.ReadLine();

            try
            {
                int result = Convert.ToInt32(input, fromBase);

                Console.WriteLine($"Result (decimal system): {result}");
            }
            catch (FormatException)
            {
                Console.WriteLine($"Error: The input string is not a valid number in the {fromBase}-based system.");
            }
            catch (OverflowException)
            {
                Console.WriteLine($"Error: The result of the conversion exceeds the range of int ({int.MinValue} .. {int.MaxValue}).");
            }
        }

        static void WordToDigit(string wordInput)
        {
            if (string.IsNullOrEmpty(wordInput))
            {
                Console.WriteLine("Pomilka: You didn’t enter anything.");
                return;
            }

            string cleanWord = wordInput.Trim().ToLower();

            switch (cleanWord)
            {
                case "zero": Console.WriteLine("Result: 0"); break;
                case "one": Console.WriteLine("Result: 1"); break;
                case "two": Console.WriteLine("Result: 2"); break;
                case "three": Console.WriteLine("Result: 3"); break;
                case "four": Console.WriteLine("Result: 4"); break;
                case "five": Console.WriteLine("Result: 5"); break;
                case "six": Console.WriteLine("Result: 6"); break;
                case "seven": Console.WriteLine("Result: 7"); break;
                case "eight": Console.WriteLine("Result: 8"); break;
                case "nine": Console.WriteLine("Result: 9"); break;
                default:
                    Console.WriteLine("Error: Invalid input. Please enter a valid word for a digit from 'zero' to 'nine'.");
                    break;
            }

            Console.WriteLine("Enter expression (for example: 3>2, 7<=3, 10==10).");
            Console.WriteLine("Allowed operators: <, >, <=, >=, ==, !=");
            Console.Write("Your expression: ");

            string input = Console.ReadLine();

            try
            {
                bool result = EvaluateExpression(input);
                Console.WriteLine($"Result: {result.ToString().ToLower()}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Input error: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Expression error: {ex.Message}");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Error: One of the numbers exceeds the range of the int type.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        static bool EvaluateExpression(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                throw new ArgumentException("String cannot be empty.");
            }

            string cleanExpr = expression.Replace(" ", "");

            string op = "";
            int opIndex = -1;

            if (cleanExpr.Contains("<=")) { op = "<="; opIndex = cleanExpr.IndexOf("<="); }
            else if (cleanExpr.Contains(">=")) { op = ">="; opIndex = cleanExpr.IndexOf(">="); }
            else if (cleanExpr.Contains("==")) { op = "=="; opIndex = cleanExpr.IndexOf("=="); }
            else if (cleanExpr.Contains("!=")) { op = "!="; opIndex = cleanExpr.IndexOf("!="); }
            else if (cleanExpr.Contains("<")) { op = "<"; opIndex = cleanExpr.IndexOf("<"); }
            else if (cleanExpr.Contains(">")) { op = ">"; opIndex = cleanExpr.IndexOf(">"); }

            if (opIndex == -1)
            {
                throw new ArgumentException("No valid comparison operator found (<, >, <=, >=, ==, !=).");
            }

            string leftStr = cleanExpr.Substring(0, opIndex);
            string rightStr = cleanExpr.Substring(opIndex + op.Length);

            if (string.IsNullOrEmpty(leftStr) || string.IsNullOrEmpty(rightStr))
            {
                throw new FormatException("Missing number on the left or right side of the operator.");
            }

            int leftNumber = Convert.ToInt32(leftStr);
            int rightNumber = Convert.ToInt32(rightStr);

            switch (op)
            {
                case "<": return leftNumber < rightNumber;
                case ">": return leftNumber > rightNumber;
                case "<=": return leftNumber <= rightNumber;
                case ">=": return leftNumber >= rightNumber;
                case "==": return leftNumber == rightNumber;
                case "!=": return leftNumber != rightNumber;
                default: throw new ArgumentException("Unknown operator.");
            }
        }
    }
}
