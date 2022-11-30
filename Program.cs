using System;
using System.Threading;

namespace ConsoleFighting
{
    public class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            game.Start();
        }
    }

    public class Game
    {
        private int playerLife = 150;
        private int playerEnergy = 100;
        private int virusLife = 100;
        private int virusEnergy = 200;

        private int playerStatIndex = 12;
        private int virusStatIndex = 49;

        private Random random = new Random();

        public void Start()
        {
            WriteStats();
            UpdateStats();
            WritePlayerActions();
            WriteAction(string.Empty);

            while (playerLife > 0 && virusLife > 0)
            {
                var keyInfo = Console.ReadKey();
                var userAction = MakeUserAction(keyInfo);
                UpdateStats();

                WriteAction(userAction);

                if (virusLife <= 0)
                    break;

                Thread.Sleep(1000);

                var virusAction = MakeVirusAction();
                UpdateStats();

                WriteAction(virusAction);
            }

            Thread.Sleep(3000);

            if (playerLife <= 0)
            {
                WriteAction("   Пользователь проиграл вирусу");
            }

            if (virusLife <= 0)
            {
                WriteAction("   Пользователь победил вирус");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("   Нажать ESC для завершения");
            while (true)
            {
                var keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape)
                    break;
            }

        }

        private void WriteAction(string action)
        {
            var initialLeft = Console.CursorLeft;
            var initialTop = Console.CursorTop;
            var length = Console.BufferWidth;

            Console.SetCursorPosition(0, initialTop);

            Console.Write(new string(' ', length));
            Console.SetCursorPosition(0, initialTop);
            Console.Write(action);

            Console.SetCursorPosition(initialLeft, initialTop);
        }

        private string MakeUserAction(ConsoleKeyInfo keyInfo)
        {
            string result;

            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                    if (playerEnergy < 10)
                    {
                        result = "   Пользователь не смог очистить папку Temp";
                        break;
                    }

                    virusLife -= 15;
                    playerEnergy -= 10;
                    result = "   Пользователь очистил папку Temp";
                    break;
                case ConsoleKey.D2:
                    if (playerEnergy < 40)
                    {
                        result = "   Пользователь не смог использовать антивирус Касперского";
                        break;
                    }

                    virusLife -= 30;
                    playerEnergy -= 40;
                    result = "   Пользователь использовал антивирус Касперского";
                    break;
                case ConsoleKey.D3:
                    playerEnergy += 20;
                    result = "   Пользователь выпил кофе";
                    break;
                case ConsoleKey.D4:
                    if (playerEnergy < 20)
                    {
                        result = "   Пользователь не смог заказать пиццу";
                        break;
                    }

                    playerLife += 30;
                    playerEnergy -= 20;
                    result = "   Пользователь заказал пиццу";
                    break;
                case ConsoleKey.D5:
                    if (playerEnergy < 15)
                    {
                        result = "   Пользователь не смог перезагрузиться в безопасный режим";
                        break;
                    }

                    playerLife += 25;
                    playerEnergy -= 15;
                    result = "   Пользователь перезагрузился в безопасный режим";
                    break;
                case ConsoleKey.D6:
                    if (playerEnergy < 80)
                    {
                        result = "   Пользователь не смог восстановить файлы вручную";
                        break;
                    }

                    virusLife -= 60;
                    playerEnergy -= 80;
                    result = "   Пользователь восстановил файлы вручную";
                    break;
                default:
                    result = "   Пользователь бездействует";
                    break;
            }

            return result;
        }

        private string MakeVirusAction()
        {
            var needRetry = false;

            string result = "";

            do
            {
                var rnd = random.Next(0, 12);

                switch (rnd)
                {
                    case 1:
                        if (virusEnergy < 10)
                        {
                            needRetry = true;
                            break;
                        }
                        playerLife -= 10;
                        virusEnergy -= 10;
                        result = "   Вирус заразил диспетчер задач";
                        needRetry = false;
                        break;
                    case 2:
                        if (virusEnergy < 40)
                        {
                            needRetry = true;
                            break;
                        }
                        virusLife += 30;
                        virusEnergy -= 40;
                        result = "   Вирус скачал новую версию вируса";
                        needRetry = false;
                        break;
                    case 5:
                        virusEnergy += 20;
                        result = "   Вирус украл данные пользователя";
                        needRetry = false;
                        break;
                    case 6:
                        if (virusEnergy < 40)
                        {
                            needRetry = true;
                            break;
                        }
                        playerLife -= 35;
                        virusEnergy -= 40;
                        result = "   Вирус отключил антивирус";
                        needRetry = false;
                        break;
                    case 9:
                        if (virusEnergy < 15)
                        {
                            needRetry = true;
                            break;
                        }
                        playerLife -= 20;
                        virusEnergy -= 15;
                        result = "   Вирус зашифровал пользовательские данные";
                        needRetry = false;
                        break;
                    case 11:
                        if (virusEnergy < 20)
                        {
                            needRetry = true;
                            break;
                        }
                        playerLife -= 15;
                        virusEnergy -= 20;
                        result = "   Вирус отключил интернет для пользователя";
                        needRetry = false;
                        break;
                    case 12:
                        if (virusEnergy < 60)
                        {
                            needRetry = true;
                            break;
                        }
                        playerLife -= 60;
                        virusEnergy -= 60;
                        result = "   Вирус отключил клавиатуру и мышь";
                        needRetry = false;
                        break;
                    default:
                        playerLife -= 5;
                        virusEnergy += 10;
                        virusLife += 10;
                        result = "   Вирус заражил системный файл";
                        needRetry = false;
                        break;
                }
            } while (needRetry);

            return result;
        }

        private void WriteStats()
        {
            Console.WriteLine("     Жизни: 000                    Жизни вируса: 000");
            Console.WriteLine("   Энергия: 000                  Энергия вируса: 000");
        }

        private void UpdateStats()
        {
            var initialLeft = Console.CursorLeft;
            var initialTop = Console.CursorTop;

            Console.SetCursorPosition(playerStatIndex, 0);
            var str = playerLife.ToString("000");
            Console.Write(str);

            Console.SetCursorPosition(virusStatIndex, 0);
            str = virusLife.ToString("000");
            Console.Write(str);

            Console.SetCursorPosition(playerStatIndex, 1);
            str = playerEnergy.ToString("000");
            Console.Write(str);

            Console.SetCursorPosition(virusStatIndex, 1);
            str = virusEnergy.ToString("000");
            Console.Write(str);

            Console.SetCursorPosition(initialLeft, initialTop);
        }

        private void WritePlayerActions()
        {
            Console.WriteLine();
            Console.WriteLine("   1. Почистить папку Temp (15 урона, -10 энергии)");
            Console.WriteLine("   2. Использовать антивирус Касперского (30 урона, -40 энергии)");
            Console.WriteLine("   3. Выпить кофе (+20 энергии)");
            Console.WriteLine("   4. Заказать доставку пиццы (+30 жизни, -20 энергии)");
            Console.WriteLine("   5. Перезагрузиться в безопасный режим (+25 жизни, -15 энергии)");
            Console.WriteLine("   6. Восстановить файлы вручную (60 урона, -80 энергии)");
            Console.WriteLine();
        }
    }
}
