using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {
        private string name;
        private readonly List<ILoan> loans;
        private readonly List<IClient> clients;

        public Bank(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            loans = new List<ILoan>();
            clients = new List<IClient>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BankNameNullOrWhiteSpace);
                }
                name = value;
            }
        }

        public int Capacity { get; private set; }

        public IReadOnlyCollection<ILoan> Loans => loans;

        public IReadOnlyCollection<IClient> Clients => clients;

        public void AddClient(IClient Client)
        {
            if (Capacity > clients.Count)
            {
                clients.Add(Client);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.NotEnoughCapacity);
            }
        }

        public void AddLoan(ILoan loan) => loans.Add(loan);

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Name: {Name}, Type: {this.GetType().Name}");

            if (clients.Count == 0)
            {
                sb.AppendLine($"Clients: none");
            }
            else
            {
                sb.AppendLine($"Clients: {string.Join(", ", clients.Select(c => c.Name))}");
            }

            sb.AppendLine($"Loans: {loans.Count}, Sum of Rates: {loans.Sum(l => l.InterestRate)}");

            return sb.ToString().TrimEnd();
        }

        public void RemoveClient(IClient Client) => clients.Remove(Client);

        public double SumRates() => loans.Sum(l => l.InterestRate);
    }
}