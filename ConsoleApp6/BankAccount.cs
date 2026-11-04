using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    internal class BankAccount
    {
        private string accountNumber;
        private double balance;

        public BankAccount(string accountNumber, double initialBalance)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException("The rack room cannot be empty.", nameof(accountNumber));
            }

            if (initialBalance < 0)
            {
                throw new ArgumentException("The initial balance cannot be negative.", nameof(initialBalance));
            }

            this.accountNumber = accountNumber.Trim();
            this.balance = initialBalance;
        }

        public string AccountNumber => accountNumber;
        public double Balance => balance;

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("The deposit amount must be positive.", nameof(amount));
            }

            balance += amount;
            Console.WriteLine($"Account successfully funded with {amount:F2}. Current balance: {balance:F2}");
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("The withdrawal amount must be positive.", nameof(amount));
            }

            if (amount > balance)
            {
                throw new InvalidOperationException($"Insufficient funds on the account for withdrawal {amount:F2}. Current balance: {balance:F2}.");
            }

            balance -= amount;
            Console.WriteLine($"Successfully withdrew {amount:F2} from the account. Current balance: {balance:F2}");
        }

        public void PrintAccountInfo()
        {
            Console.WriteLine($"Account: {accountNumber} | Balance: {balance:F2}");
        }
    }
}
