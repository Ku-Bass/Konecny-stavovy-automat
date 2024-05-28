using System;
using System.Text.RegularExpressions;

public class EmailValidator
{
	// Function to check if the input text is a valid email
	public bool IsValidEmail(string input)
	{
		// Define states
		int q0 = 0;  // Initial state
		int q1 = 1;  // Reading local part
		int q2 = 2;  // After @, start reading domain
		int q3 = 3;  // Reading domain part
		int q4 = 4; // reading ending part

		int currentState = q0;

		for (int i = 0; i < input.Length; i++)
		{
			char ch = input[i];

			switch (currentState)
			{
				case 0:  // q0: Initial state
					if (char.IsLetterOrDigit(ch))
					{
						currentState = q1;
					}
					else
					{
						return false; // Invalid character for start of local part
					}
					break;

				case 1:  // q1: Reading local part
					if (char.IsLetterOrDigit(ch) || ch == '.' || ch == '_' || ch == '-')
					{
						currentState = q1;
					}
					else if (ch == '@')
					{
						currentState = q2;
					}
					else
					{
						return false; // Invalid character in local part
					}
					break;

				case 2:  // q2: After @, start reading domain
					if (char.IsLetterOrDigit(ch))
					{
						currentState = q3;
					}
					else
					{
						return false; // Invalid start of domain part
					}
					break;

				case 3:  // q3: Reading domain part
					if (char.IsLetterOrDigit(ch))
					{
						currentState = q3;
					}
					else if (ch == '.')
					{
						currentState = q4;
					}
					break;
				case 4:  // q4: reading domain ending
					if (char.IsLetterOrDigit(ch))
					{
						currentState = q4;
					}
					else
					{
						return false; // Invalid character in domain ending
					}
					break;
			}
		}

		// If we end in state q4 and last character is alphanumeric, it's a valid email
		return currentState == q4 && char.IsLetterOrDigit(input[input.Length - 1]);
	}

	public static void Main()
	{
		EmailValidator validator = new EmailValidator();

		// Test cases
		string[] testEmails = {
			"jan.novak@mensagymnazium.cz",
			"jan.novak",
			"jan@l.l"
		};

		foreach (string email in testEmails)
		{
			bool isValid = validator.IsValidEmail(email);
			Console.WriteLine($"{email}: {isValid}");
		}
	}
}
