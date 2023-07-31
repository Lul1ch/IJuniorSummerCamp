using System;
using System.Collections.Generic;
namespace CSharpHomework2
{
    class MatchesStatistic
    {
        private int victoriesCounter;
        private int defeatsCounter;
        private int currentWinStreak;

        public MatchesStatistic()
        {
            this.victoriesCounter = 0;
            this.defeatsCounter = 0;
            this.currentWinStreak = 0;
        }

        public int GetGamesNumber()
        {
            return victoriesCounter + defeatsCounter;
        }

        public float GetWinrate()
        {
            return (float)victoriesCounter / (float)GetGamesNumber(); 
        }
        
        public int GetCurrentWinStreak()
        {
            return currentWinStreak;
        }

        public void AddNewMatchResult(string matchResult)
        {
            switch(matchResult)
            {
                case ("victory"):
                    currentWinStreak++;
                    victoriesCounter++;
                    break;
                case ("defeat"):
                    currentWinStreak = 0;
                    defeatsCounter++;
                    break;
                default:
                    Console.WriteLine("Unknow match result");
                    break;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var dic = new Dictionary<string, MatchesStatistic>();
            string[] characters = { "Луизианна", "Коркес", "Нова", "Юн Джин", "Рэйко"};
            string[] winOrLose = { "victory", "defeat" };
            Random rand = new Random();

            List<string> mostPlayedHeroes = new List<string>(), lessPlayedHeroes = new List<string>(), longestWinStreakHeroes = new List<string>();
            List<string> mostSuccessfullHeroes = new List<string>(), lessSuccessfullHeroes = new List<string>();

            float highestWinRate = 0, lowestWinRate = -1;
            int mostGamesPlayed = 0, lessGamesPlayed = -1, longestWinStreak = 0;

            foreach (var charName in characters)
            {
                dic.Add(charName, new MatchesStatistic());
                Console.Write(charName + ": ");
                int matchesNum = rand.Next(3, 10);
                for (int i = 0; i < matchesNum; i++)
                {
                    int matchResultIndex = rand.Next(0, 2);
                    dic[charName].AddNewMatchResult(winOrLose[matchResultIndex]);
                    Console.Write(winOrLose[matchResultIndex] + " ");
                }
                Console.Write("\n");
                if (dic[charName].GetWinrate() > highestWinRate)
                {
                    highestWinRate = dic[charName].GetWinrate();
                    mostSuccessfullHeroes.Clear();
                    mostSuccessfullHeroes.Add(charName);
                } else if (dic[charName].GetWinrate() == highestWinRate)
                {
                    mostSuccessfullHeroes.Add(charName);
                }

                if (dic[charName].GetWinrate() < lowestWinRate || lowestWinRate < 0f)
                {
                    lowestWinRate = dic[charName].GetWinrate();
                    lessSuccessfullHeroes.Clear();
                    lessSuccessfullHeroes.Add(charName);
                } else if (dic[charName].GetWinrate() == lowestWinRate)
                {
                    lessSuccessfullHeroes.Add(charName);
                } 

                if (dic[charName].GetGamesNumber() > mostGamesPlayed)
                {
                    mostGamesPlayed = dic[charName].GetGamesNumber();
                    mostPlayedHeroes.Clear();
                    mostPlayedHeroes.Add(charName);
                } else if (dic[charName].GetGamesNumber() == mostGamesPlayed)
                {
                    mostPlayedHeroes.Add(charName);
                }

                if (dic[charName].GetGamesNumber() < lessGamesPlayed || lessGamesPlayed < 0f)
                {
                    lessGamesPlayed = dic[charName].GetGamesNumber();
                    lessPlayedHeroes.Clear();
                    lessPlayedHeroes.Add(charName);
                }
                else if (dic[charName].GetGamesNumber() == lessGamesPlayed)
                {
                    lessPlayedHeroes.Add(charName);
                }

                if (dic[charName].GetCurrentWinStreak() > longestWinStreak)
                {
                    longestWinStreak = dic[charName].GetCurrentWinStreak();
                    longestWinStreakHeroes.Clear();
                    longestWinStreakHeroes.Add(charName);
                }
                else if (dic[charName].GetCurrentWinStreak() == longestWinStreak)
                {
                    longestWinStreakHeroes.Add(charName);
                }
            }

            Console.Write("\nСамый успешный герой: ");
            foreach(var charName in mostSuccessfullHeroes)
            {
                Console.Write($"{charName} (винрейт {string.Format("{0:0.00}", dic[charName].GetWinrate())}) ");
            }

            Console.Write("\nСамый неуспешный герой: ");
            foreach (var charName in lessSuccessfullHeroes)
            {
                Console.Write($"{charName} (винрейт {string.Format("{0:0.00}", dic[charName].GetWinrate())}) ");
            }

            Console.Write("\nСамый любимый герой:  ");
            foreach (var charName in mostPlayedHeroes)
            {
                Console.Write($"{charName} ({dic[charName].GetGamesNumber()} сыгранных матча) ");
            }

            Console.Write("\nСамый нелюбимый герой: ");
            foreach (var charName in lessPlayedHeroes)
            {
                Console.Write($"{charName} ({dic[charName].GetGamesNumber()} сыгранных матча) ");
            }

            Console.Write("\nГерой с самым большим винстриком: ");
            foreach (var charName in longestWinStreakHeroes)
            {
                Console.Write($"{charName} (винстрик {dic[charName].GetCurrentWinStreak()}) ");
            }
        }
    }
}
