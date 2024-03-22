using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankLoan.Core
{
    public class Controller : IController
    {
        private LoanRepository loans;
        private BankRepository banks;

        public Controller()
        {
            loans = new LoanRepository();
            banks = new BankRepository();
        }

        public string AddBank(string bankTypeName, string name)
        {
            IBank bank;
            if (bankTypeName == nameof(BranchBank))
            {
                bank = new BranchBank(name);
            }
            else if (bankTypeName == nameof(CentralBank))
            {
                bank = new CentralBank(name);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.BankTypeInvalid);
            }

            banks.AddModel(bank);
            return string.Format(OutputMessages.BankSuccessfullyAdded, bankTypeName);
        }

        public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
        {
            IClient client;
            if (clientTypeName == nameof(Student))
            {
                client = new Student(clientName, id, income);
            }
            else if (clientTypeName == nameof(Adult))
            {
                client = new Adult(clientName, id, income);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.ClientTypeInvalid);
            }

            IBank bank = banks.FirstModel(bankName);
            if (bank.GetType().Name == nameof(BranchBank) && client.GetType().Name == nameof(Adult)
                || bank.GetType().Name == nameof(CentralBank) && client.GetType().Name == nameof(Student))
            {
                return OutputMessages.UnsuitableBank;
            }

            bank.AddClient(client);
            return string.Format(OutputMessages.ClientAddedSuccessfully, clientTypeName, bankName);

        }

        public string AddLoan(string loanTypeName)
        {
            ILoan loan;
            if (loanTypeName == nameof(StudentLoan))
            {
                loan = new StudentLoan();
            }
            else if (loanTypeName == nameof(MortgageLoan))
            {
                loan = new MortgageLoan();
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.LoanTypeInvalid);
            }
            
            loans.AddModel(loan);
            return string.Format(OutputMessages.LoanSuccessfullyAdded, loanTypeName);
        }

        public string FinalCalculation(string bankName)
        {
            IBank bank = banks.FirstModel(bankName);
            double sum = bank.Clients.Sum(c => c.Income) + bank.Loans.Sum(l => l.Amount);

            return string.Format(OutputMessages.BankFundsCalculated, bankName, $"{sum:f2}");
        }

        public string ReturnLoan(string bankName, string loanTypeName)
        {
            ILoan loan = loans.FirstModel(loanTypeName);
            if (loan == null)
            {
                return string.Format(ExceptionMessages.MissingLoanFromType, loanTypeName);
            }
            
            IBank bank = banks.FirstModel(bankName);
            bank.AddLoan(loan);
            loans.RemoveModel(loan);
           
            return string.Format(OutputMessages.LoanReturnedSuccessfully, loanTypeName, bankName);
        }

        public string Statistics()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var bank in banks.Models)
            {
                sb.AppendLine(bank.GetStatistics());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
