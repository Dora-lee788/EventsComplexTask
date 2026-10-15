using System;

namespace EventsComplexTask
{
    public class Program
    {
        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("\n1. Запустить таймер");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                try
                {
                    int choice = int.Parse(Console.ReadLine()!);

                    if (choice == 0)
                    {
                        break;
                    }

                    if (choice != 1)
                    {
                        throw new ArgumentException("Такого пункта меню нет.");
                    }

                    CountdownTimer timer = new CountdownTimer();

                    Console.Write("Введите время в секундах (для проверки введите 6): ");
                    int seconds = int.Parse(Console.ReadLine()!);

                    EventHandler<int> logger = (sender, remaining) =>
                        Console.WriteLine($"Первый обработчик: осталось {remaining} сек.");

                    EventHandler<int> earlyStopper = null!;

                    earlyStopper = (sender, remaining) =>
                    {
                        if (remaining == seconds / 2)
                        {
                            Console.WriteLine("Второй обработчик отписывается.");
                            timer.Tick -= earlyStopper;
                        }
                        else if (remaining > seconds / 2)
                        {
                            Console.WriteLine($"Второй обработчик: осталось {remaining} сек.");
                        }
                    };

                    EventHandler finishedHandler = (sender, e) =>
                        Console.WriteLine("Таймер завершён!");

                    timer.Tick += logger;
                    timer.Tick += earlyStopper;
                    timer.Finished += finishedHandler;

                    timer.Start(seconds);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: введите целое число.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}

