using LoanSharkMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanSharkMVC.Helpers
{
    public static class LoanHelper
    {
        public static LoanShark GetPayments(LoanShark loan)
        {
            // Calculate my Monthly Payment
            loan.Payment = CalcPayment(loan.LoanAmount, loan.LoanRate, loan.LoanTerm);

            //Loop from 1 to end-of-term
            var balance = loan.LoanAmount;
            var totalInterest = 0.0m;
            var monthlyInterest = 0.0m;
            var monthlyPrincipal = 0.0m;
            var monthlyRate = CalcMonthlyRate(loan.LoanRate);

            for (int month = 1; month <= loan.LoanTerm; month++)
            {
                monthlyInterest = CalcMonthlyInterest(balance, monthlyRate);
                totalInterest += monthlyInterest;
                monthlyPrincipal = loan.Payment - monthlyInterest;
                balance -= monthlyPrincipal;

                LoanPayment loanPayment = new();

                loanPayment.Month = month;
                loanPayment.Payment = loan.Payment;
                loanPayment.MonthlyPrincipal = monthlyPrincipal;
                loanPayment.MonthlyInterest = monthlyInterest;
                loanPayment.TotalInterest = totalInterest;
                loanPayment.Balance = balance;

                loan.Payments.Add(loanPayment);
            }

            loan.TotalInterest = totalInterest;
            loan.TotalCost = loan.LoanAmount + totalInterest;

            return loan;
        }

        private static decimal CalcPayment(decimal amount, decimal rate, int term)
        {
            rate = CalcMonthlyRate(rate);
            var rateD = Convert.ToDouble(rate);
            var amountD = Convert.ToDouble(amount);

            var paymentD = (amountD * rateD) / (1 - Math.Pow(1 + rateD, -term));

            return Convert.ToDecimal(paymentD);
        }

        private static decimal CalcMonthlyRate(decimal rate)
        {
            return rate / 1200;
        }

        private static decimal CalcMonthlyInterest(decimal balance, decimal monthlyRate)
        {
            return balance * monthlyRate;
        }
    }
}
