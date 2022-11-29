using Score;
using System.Diagnostics;
using System.Threading;

namespace SpeedTest
{
    public static class ScoreTable
    {
        
    }

    public static class Timer
    {
        public static Stopwatch stopWatch = new Stopwatch();
        public static int sec = 0;
        public static bool cancel;

        public static void cancelationMethod()
        {
            Console.WriteLine($"Пользователь: {SpeedWriteTest.nickname}");
            Console.WriteLine($"Всего набрано символов за минуту: {SpeedWriteTest.inp_l}");
            // Console.WriteLine($"Ваш рекорд в минуту: {sec / score}");
            Console.WriteLine($"Всего набрано символов за секунду: {SpeedWriteTest.inp_l / sec}");
            // Console.
        }
        public static void UpdateTimer()
        {
            
            // object sec = 0;
            // Console.WriteLine("Время: ");
            
            stopWatch.Start();
            Console.SetCursorPosition(0, 10);
            Console.Write("Время:");
            cancel = false;
            
            while (stopWatch.Elapsed.Seconds != 59)
            {
                if (cancel)
                {
                    break;
                }
                
                sec += 1;
                Console.SetCursorPosition(7, 10);
                Console.Write("  ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"{sec} сек.  ");
                
                Thread.Sleep(1000);
            }
            stopWatch.Stop();
            Console.Clear();
            cancelationMethod();
        }
    }
    public class SpeedWriteTest
    {
        private static List<UserScore> users;
        public static string nickname;
        public static int inp_l;

        // public static int sec 
        
        private static void init()
        {
            
            // Console.SetCursorPosition(1, 7);
            Console.WriteLine("Введите никнейм");
            nickname = Console.ReadLine();
            Console.Clear();
            Console.SetCursorPosition(0, 2);
            int step = 0;
            int coord_x = 0;
            int coord_y = 2; 
            inp_l = 0;
            
            Console.ForegroundColor = ConsoleColor.Magenta;
            
            string v = "Ритуальная служба «Горслужба-Ритуал» Невозможно быть готовым к смерти дорогого и близкого человека." +
                       " Это всегда – удар, который необходимо принять и пережить." +
                       " И проводы в последний путь – дань уважения, памяти безвозвратно ушедшему человеку. " +
                       "Их нужно провести, превозмогая боль от утраты. Проведение и подготовка похорон связаны с" +
                       " множеством болезненных и неприятных хлопот, вот почему так важно, чтобы организацией всех " +
                       "его этапов занималась ритуальная служба. Это сможет хоть немного, но оградить вас и ваших близких" +
                       " от трудностей и не усиливать боль и горечь утраты. Служба ритуальных услуг «Горслужба-Ритуал»" +
                       " выражает вам свои соболезнования и предлагает свою помощь. Специализированный агент ритуальной" +
                       " (похоронной) службы будет заниматься вашим вопросом с момента подготовки документов до захоронения и отпевания.";
            List<char> lst = v.ToList();
            
            Console.WriteLine(string.Join("", lst));
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            Thread tr = new Thread(_ =>
            {
                Timer.UpdateTimer();
            });
            tr.Start(token);
             // key = Console.ReadKey(true);
             // using (var tokenSource = new CancellationTokenSource())
             // {
             //     var tr = new Thread(new ParameterizedThreadStart((param) =>
             //     {
             //         var token = (CancellationToken)param;
             //         
             //     }));
             // }
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Console.WriteLine(key.KeyChar);
                try
                {
                    if (coord_x + 1 > Console.BufferWidth)
                        {
                            coord_y++;
                            coord_x = 0;
                        }
                    else if (inp_l == lst.Count)
                    {
                        Console.Clear();
                        Timer.cancel = true;
                        // Console.WriteLine(Timer.sec);
                        // Timer.cancelationMethod();
                        // cancelTokenSource.Cancel();
                        break;
                    }
                    else if (Timer.sec == 59)
                    {
                        break;
                    }
                    
                    else
                        {
                            Console.SetCursorPosition(coord_x, coord_y);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(key.KeyChar);
                            inp_l++;
                            coord_x++;   
                        }
                }
                catch (ArgumentException)
                {
                    Timer.stopWatch.Stop();
                    cancelTokenSource.Cancel();
                    break;
                    
                }
                
            }
            
        }
        public static void Main()
        {
            init();
        }
    }
}