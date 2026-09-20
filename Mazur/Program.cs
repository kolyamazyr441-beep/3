using System;
using System.Linq;
using System.Text;
using Mazur.Models;
using Mazur.Data;

namespace JobSearchService
{
    class Program
    {
        public static CsvService CsvService { get; private set; } = new CsvService();

        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== СЕРВІС ПОШУКУ РОБОТИ ===");
                Console.WriteLine("1 - Переглянути вакансії");
                Console.WriteLine("2 - Додати вакансію");
                Console.WriteLine("0 - Вийти");
                Console.Write("Оберіть дію: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowVacancies();
                        break;

                    case "2":
                        AddVacancy();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Невірний вибір");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ShowVacancies()
        {
            Console.Clear();

            var vacancies = CsvService.LoadVacancies();

            if (vacancies.Count == 0)
            {
                Console.WriteLine("Вакансій немає.");
            }
            else
            {
                foreach (var v in vacancies)
                {
                    Console.WriteLine(
                        $"{v.Id}. {v.Title} | {v.Company} | {v.Specialty}"
                    );
                }
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        static void AddVacancy()
        {
            Console.Clear();

            var vacancies = CsvService.LoadVacancies();

            Console.Write("Назва вакансії (0 - вихід): ");
            string title = Console.ReadLine();

            if (title == "0")
                return;

            Console.Write("Компанія: ");
            string company = Console.ReadLine();

            Console.Write("Спеціальність: ");
            string specialty = Console.ReadLine();

            int newId = vacancies.Count == 0
                ? 1
                : vacancies.Max(v => v.Id) + 1;

            vacancies.Add(new Vacancy
            {
                Id = newId,
                Title = title,
                Company = company,
                Specialty = specialty
            });

            CsvService.SaveVacancies(vacancies);

            Console.WriteLine("\nВакансію додано!");
            Console.WriteLine(
                "Натисніть будь-яку клавішу для повернення в меню..."
            );

            Console.ReadKey();
        }
    }
}
