using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    internal class ForeignPassport
    {
        private string fullName;
        private string passportNumber;
        private DateTime issueDate;

        public ForeignPassport(string fullName, string passportNumber, DateTime issueDate)
        {
            FullName = fullName;
            PassportNumber = passportNumber;
            IssueDate = issueDate;
        }

        public string FullName
        {
            get { return fullName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(FullName), "We cannot be empty.");
                }
                fullName = value.Trim();
            }
        }

        public string PassportNumber
        {
            get { return passportNumber; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(PassportNumber), "The passport number cannot be empty.");
                }
                if (value.Trim().Length < 6)
                {
                    throw new ArgumentException("The passport number is too short (minimum 6 characters).", nameof(PassportNumber));
                }
                passportNumber = value.Trim().ToUpper();
            }
        }

        public DateTime IssueDate
        {
            get { return issueDate; }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentException("The issue date of the passport cannot be in the future.", nameof(IssueDate));
                }
                if (value.Year < 1950)
                {
                    throw new ArgumentException("An incorrect year of passport issuance has been specified.", nameof(IssueDate));
                }
                issueDate = value;
            }
        }

        public void PrintPassportInfo()
        {
            Console.WriteLine($"Owner: {fullName}");
            Console.WriteLine($"Passport Number: {passportNumber}");
            Console.WriteLine($"Issue Date: {issueDate.ToString("dd.MM.yyyy")}");
        }
    }
}
