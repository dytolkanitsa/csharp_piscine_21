using System;

namespace charp
{
	class Program
	{
		static bool if_valid(string[] args)
		{
			if (args.Length != 5)
				return (false);
			if ((double.TryParse(args[0], out _) == false) ||
				(double.TryParse(args[1], out _) == false) ||
				(Int32.TryParse(args[2], out _) == false) ||
				(Int32.TryParse(args[3], out _) == false) ||
				(double.TryParse(args[4], out _) == false))
				return (false);
			double sum = Int32.Parse(args[0]);
			double rate = Int32.Parse(args[1]);
			int term = Int32.Parse(args[2]);
			int selectedMonth = Int32.Parse(args[3]);
			double payment = Int32.Parse(args[4]);
			if (sum < 0 || rate < 0 || term < 0 || selectedMonth < 0 || payment < 0)
				return (false);
			return (true);
		}
		static void	compare(double result1, double result2)
		{
			if (result1 < result2)
			{
				Console.WriteLine($"Переплата при уменьшении платежа: {result1}р.");
				Console.WriteLine($"Переплата при уменьшении срока: {result2}р.");
				Console.WriteLine($"Уменьшение срока выгоднее уменьшения платежа на {result1 - result2}р.");
			}
			else if (result1 > result2)
			{
				Console.WriteLine($"Переплата при уменьшении платежа: {result1}р.");
				Console.WriteLine($"Переплата при уменьшении срока: {result2}р.");
				Console.WriteLine($"Уменьшение платежа выгоднее уменьшения срока на {result1 - result2}р.");
			}
			else 
			{
				Console.WriteLine($"Переплата при уменьшении платежа: {result1}р.");
				Console.WriteLine($"Переплата при уменьшении срока: {result2}р.");
				Console.WriteLine($"Переплата одинакова в обоих вариантах.{result1 - result2}р.");
			}
		}
		static void Main(string[] args)
		{
			if (if_valid(args) == false)
				return;
			double	sum;
			double	rate;
			int		term;
			int		selectedMonth;
			double	payment;

			double	i; // годовая процентная ставка

			double	annuit; // аннуит платеж
			double	OD; // общий долг
			double	procent; // проценты
			double	CreditReminder; // остаток долга
			int		DaysInMonth;
			double	result1;
			double	result2;

			sum = Int32.Parse(args[0]);
			rate = Int32.Parse(args[1]);
			term = Int32.Parse(args[2]);
			selectedMonth = Int32.Parse(args[3]);
			payment = Int32.Parse(args[4]);

			DateTime date = new DateTime(2021, 05, 01);

			result1 = 0;
			i = rate / 12 / 100;
			annuit = (sum * i * Math.Pow((1 + i), term))/(Math.Pow((1 + i), term) - 1);
			CreditReminder = sum;
			OD = sum;
			// procent = (sum * rate * 31) / (100 * 365);
			procent = (annuit * 10 - sum);
			Console.WriteLine("|   date   |   annuit   |   OD   |   procent   |   CreditReminder   |");
			Console.WriteLine($"\t   |  {Math.Round(annuit, 2)} |{Math.Round(OD, 2)} |{Math.Round(procent, 2)}     |{Math.Round(CreditReminder, 2)}\t\t    |");
			for (int n = 0; n < term; n++)
			{
				if (n == selectedMonth - 1)
				{
					DaysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
					procent = (CreditReminder * rate * DaysInMonth) / (100 * 365);
					OD = annuit - procent;
					CreditReminder = CreditReminder - OD - payment;
					date = date.AddMonths(1);
					annuit = (CreditReminder * i * Math.Pow((1 + i), term - selectedMonth))/(Math.Pow((1 + i), term - selectedMonth) - 1);
				}
				else
				{
					DaysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
					procent = (CreditReminder * rate * DaysInMonth) / (100 * 365);
					OD = annuit - procent;
					date = date.AddMonths(1);
					CreditReminder = CreditReminder - OD;
				}
				result1 = result1 + procent;
				Console.WriteLine($"{date}| {Math.Round(annuit, 2)} |{Math.Round(OD, 2)} |{Math.Round(procent, 2)}       |{Math.Round(CreditReminder, 2)}|");
			}
				Console.WriteLine(result1);
			

			sum = Int32.Parse(args[0]);
			rate = Int32.Parse(args[1]);
			term = Int32.Parse(args[2]);
			selectedMonth = Int32.Parse(args[3]);
			payment = Int32.Parse(args[4]);

			date = new DateTime(2021, 05, 01);

			result2 = 0;
			i = rate / 12 / 100;
			annuit = (sum * i * Math.Pow((1 + i), term))/(Math.Pow((1 + i), term) - 1);
			CreditReminder = sum;
			OD = sum;
			// procent = (sum * rate * 31) / (100 * 365);
			procent = (annuit * 10 - sum);
			Console.WriteLine("|   date   |   annuit   |   OD   |   procent   |   CreditReminder   |");
			Console.WriteLine($"\t   |  {Math.Round(annuit, 2)} |{Math.Round(OD, 2)} |{Math.Round(procent, 2)}     |{Math.Round(CreditReminder, 2)}\t\t    |");
			for (int n = 0; n < term; n++)
			{
				if (n == selectedMonth - 1)
				{
					DaysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
					procent = (CreditReminder * rate * DaysInMonth) / (100 * 365);
					OD = annuit - procent;
					CreditReminder = CreditReminder - OD - payment;
					date = date.AddMonths(1);
				}
				else
				{
					DaysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
					procent = (CreditReminder * rate * DaysInMonth) / (100 * 365);
					OD = annuit - procent;
					date = date.AddMonths(1);
					if (CreditReminder - OD < 0)
						CreditReminder = 0;
					else
						CreditReminder = CreditReminder - OD;
				}
				result2 = result2 + procent;
				Console.WriteLine($"{date}| {Math.Round(annuit, 2)} |{Math.Round(OD, 2)} |{Math.Round(procent, 2)}       |{Math.Round(CreditReminder, 2)}|");
			}
				Console.WriteLine(result2);

			compare(result1, result2);
		}
	}
}
