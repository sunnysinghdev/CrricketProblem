using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cricket
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[,] probablityMatrix = new int[,] {
            //    { 5, 30, 25, 10, 15, 1, 9, 5}, //Kirat Bol
            //    { 10, 40, 20, 5, 10, 1, 4, 10 },
            //    { 20, 30, 15, 5, 5, 1, 4, 20 },
            //    { 30, 25, 5, 0, 5, 1, 4, 30 },
            //};
            //var playersName = new string[] { "Kirat boli", "NS Nodhi", "R Rumrah", "Shashi Henra" };
            Dictionary<string, int[]> probablityMatrix = new Dictionary<string, int[]>() {
                {"Kirat boli", new int[]{ 5, 30, 25, 10, 15, 1, 9, 5} }, //Kirat Bol
                {"NS Nodhi", new int[]{ 10, 40, 20, 5, 10, 1, 4, 10 }},
                {"R Rumrah", new int[]{ 20, 30, 15, 5, 5, 1, 4, 20 }},
                {"Shashi Henra", new int[]{ 30, 25, 5, 0, 5, 1, 4, 30 }},
            };
            var playersName = new string[] { "Kirat boli", "NS Nodhi", "R Rumrah", "Shashi Henra" };

            var teamLengaburu = new List<Player>();
            foreach (var p in probablityMatrix)
            {
                var player = new Player(p.Key, p.Value);
                teamLengaburu.Add(player);
            };
            Match match = new Match(teamLengaburu);
            match.Play(1, 2);
            Console.Read();
        }
    }
}
