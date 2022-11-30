using Score;
using System.Diagnostics;
using System.Threading;
using System.Text.Json;
using Newtonsoft.Json;

namespace SpeedTest
{
    public static class ScoreTable
    {
	public static void addToTable(string username, string scsec, string scmin)
	{
		string file = "table.json";
		if (File.Exists(file))
		{
			//List<UserScore> users = new List<UserScore>();
		//	string json = JsonSerializer.Deserialize(file);
			string tab = File.ReadAllText(file);
			List<UserScore> json = JsonConvert.DeserializeObject<List<UserScore>>(tab);
			UserScore newuser = new UserScore(username, scmin, scsec);
			json.Add(newuser);
			string newjson = JsonConvert.SerializeObject(json);
			File.WriteAllText(file, newjson);
		}
		else
		{
			List<UserScore> lst = new List<UserScore>();
			UserScore user = new UserScore(username, scmin, scsec);
			lst.Add(user);
			string json = JsonConvert.SerializeObject(lst);
			File.WriteAllText(file, json);
		}
	}
	public static void ShowTable()
	{
		string file = "table.json";
		string tab = File.ReadAllText(file);
		List<UserScore> json = JsonConvert.DeserializeObject<List<UserScore>>(tab);
		Console.Clear();
		Console.WriteLine("Таблица: ");
		foreach(var u in json)
		{
			Console.WriteLine($"{u.username}    {u.scoremin}сим/мин    {u.scoresec}сим/сек");
		}

	}
    }

    public static class Timer
    {
        public static Stopwatch stopWatch = new Stopwatch();
        public static int sec = 0;
        public static bool cancel;

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
            //Console.Clear();
            //cancelationMethod();
        }
    }
    public static class SpeedWriteTest
    {
        private static UserScore user;
        public static string nickname;
        public static int inp_l;

        // public static int sec 
        
        public static void init()
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
	    int losep = 0;
            
            Console.ForegroundColor = ConsoleColor.Magenta;
            
            string v = "Ритуальная служба \"Горслужба-Ритуал\" Невозможно быть готовым к смерти дорогого и близкого человека." +
                       " Это всегда – удар, который необходимо принять и пережить." +
                       " И проводы в последний путь – дань уважения, памяти безвозвратно ушедшему человеку. " +
                       "Их нужно провести, превозмогая боль от утраты. Проведение и подготовка похорон связаны с" +
                       " множеством болезненных и неприятных хлопот, вот почему так важно, чтобы организацией всех " +
                       "его этапов занималась ритуальная служба. Это сможет хоть немного, но оградить вас и ваших близких" +
                       " от трудностей и не усиливать боль и горечь утраты. Служба ритуальных услуг \"Горслужба-Ритуал\"" +
                       " выражает вам свои соболезнования и предлагает свою помощь. Специализированный агент ритуальной" +
                       " (похоронной) службы будет заниматься вашим вопросом с момента подготовки документов до захоронения и отпевания.";
            List<char> lst = v.ToList();
            
            Console.WriteLine(string.Join("", lst));
            Thread tr = new Thread(_ =>
            {
                Timer.UpdateTimer();
            });
            tr.Start();
	    bool lose = false;
            while (true)
            {
		//step++;
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Console.WriteLine(key.KeyChar);
		if (lose)
		{

			//Timer.cancel = true;
			if (coord_x + 1 > Console.BufferWidth)
			{
				coord_y++;
				coord_x = 0;
			}
			else if (Timer.sec == 59)
			{

		    		ScoreTable.addToTable(nickname, $"{inp_l}", $"{inp_l / 60}");
		    		ScoreTable.ShowTable();
				break;
			}
			else if (inp_l + losep == lst.Count)
			{
				Console.Clear();
				Timer.cancel = true;
		    		ScoreTable.addToTable(nickname, $"{inp_l}", $"{inp_l / 60}");
		    		ScoreTable.ShowTable();
				break;
			}
			else
			{
				Console.SetCursorPosition(coord_x, coord_y);
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.Write(key.KeyChar);
				losep++;
				coord_x++;
			}
		}
		else
		{
                if (coord_x + 1 > Console.BufferWidth)
                {
                    coord_y++;
                    coord_x = 0;
                }
		else if (key.KeyChar != lst[inp_l])
		    {
			   lose = true;
		    }
                else if (inp_l == lst.Count)
                {
                    Console.Clear();
                    Timer.cancel = true;
		    ScoreTable.addToTable(nickname, $"{inp_l}", $"{inp_l / 60}");
		    ScoreTable.ShowTable();
                    break;
                    
		}
                else if (Timer.sec == 59)
                {
		    
		    ScoreTable.addToTable(nickname, $"{inp_l}", $"{inp_l / 60}");
		    ScoreTable.ShowTable();
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

            }
            
        }
        public static void Main()
        {
	//string s1 = "dfdfdfdfd";
	//Console.WriteLine(s + s1);
            init();
        }
    }
}
