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
            string[] runs_name = new string[] { "0", "1", "2", "3", "4", "5", "6", "OUT" };
            int[,] probablityMatrix = new int[,] {
                { 5, 30, 25, 10, 15, 1, 9, 5}, //Kirat Bol
                { 10, 40, 20, 5, 10, 1, 4, 10 },
                { 20, 30, 15, 5, 5, 1, 4, 20 },
                { 30, 25, 5, 0, 5, 1, 4, 30 },
            };
            var playersName = new string[] { "Kirat boli", "NS Nodhi", "R Rumrah", "Shashi Henra" };

            //function generateProbablity(probablity)
            //{
            //    var counter = [0, 0, 0, 0, 0, 0, 0, 0];

            //    var list = [];
            //    for (var i = 0; i < 100; i++)
            //    {
            //        var num = 0;
            //        num = Math.floor((Math.random() * 8) + 0);
            //        if (num == 7)
            //        {
            //            //console.log(num);
            //        }
            //        if (counter[num] < probablity[num])
            //        {
            //            counter[num]++;
            //            list[i] = num;

            //        }
            //        else
            //        {
            //            for (var j = 0; j < 8; j++)
            //            {
            //                if (counter[j] < probablity[j])
            //                {
            //                    counter[j]++;
            //                    list[i] = j;

            //                    break;
            //                }
            //            }
            //        }
            //    }
            //    //console.log(counter); //= [0, 0, 0, 0, 0, 0, 0, 0];
            //    return list;
            //};
            // var matrix = generateProbablity(probablityMatrix[0]);
            // for (var i = 0; i < 100; i++) {
            //     //console.log(runs_name[matrix[i]]);
            // }

            //function Player(name, list)
            //{
            //    this.scoreBag = list;
            //    this.name = name;
            //    this.isOut = false;
            //    var _runsScored = 0;
            //    var _ballPlayed = 0;
            //    var _batted = false;


            //    this.strike = function() {
            //        ++_ballPlayed;
            //        var run = this.scoreBag[Math.floor(Math.random() * 100)];
            //        if (run == 7)
            //        {
            //            this.isOut = true;
            //        }
            //        else
            //        {
            //            _runsScored += run;
            //        }
            //        return run;
            //    }
            //    this.getRunsScored = function() {
            //        return _runsScored;
            //    }
            //    this.getBallsPlayed = function() {
            //        return _ballPlayed;
            //    }
            //    this.setPlayerHasBatted = function() {
            //        _batted = true;
            //    }
            //    this.hasBatted = function() {
            //        return _batted;
            //    }
            //}
            var players = [];
            for (var p = 0; p < probablityMatrix.length; p++)
            {
                var score_bag = generateProbablity(probablityMatrix[p]);

                var player = new Player(playersName[p], score_bag);
                players.push(player);
                //console.log(player + " added\n");
            };
            var match = { };
            match.requiredRuns = 40;
            match.remainingBall = 24;
            match.score = 0;
            match.playerCount = 2;
            match.batsman = players[0];
            match.batsman.setPlayerHasBatted();
            match.NonStriker = players[1];
            match.NonStriker.setPlayerHasBatted();
            match.changeStrike = function() {
                var temp = match.batsman;
                match.batsman = match.NonStriker;
                match.NonStriker = temp;
            };
            match.addNextPlayer = function() {

                match.batsman = players[match.playerCount++];
                match.batsman.setPlayerHasBatted();
            };
            match.start = function() {
                for (var over = 16; over < 20; over++)
                {
                    if (!match.isFinished)
                    {
                        console.log("\n<b>" + (20 - over) + " overs left.  " + (match.requiredRuns - match.score) + " runs remaining.<b>");

                        for (var ball = 1; ball <= 6; ball++)
                        {
                            match.remainingBall--;
                            var over_ball = over + "." + ball + " ";
                            var run_scored = match.batsman.strike();

                            if (run_scored > 6)
                            {
                                console.log(over_ball + match.batsman.name + " is out !!!.");
                                if (match.playerCount >= players.length)
                                {
                                    match.isFinished = true;
                                    break;
                                }
                                match.addNextPlayer();
                            }
                            else
                            {

                                match.score += run_scored;

                                console.log(over_ball + match.batsman.name + " scored " + runs_name[run_scored] + " runs.");
                                if (run_scored % 2 == 1)
                                {
                                    match.changeStrike();
                                }
                                if (match.score >= match.requiredRuns)
                                {
                                    match.isFinished = true;
                                    break;
                                }


                            }
                        }
                        match.changeStrike();
                    }
                    else
                    {
                        break;
                    }
                }
            };
            setTimeout(function(e){
                var conso = console.log;
                var logger = document.getElementById("logger");
                console.log = function(s){

                    logger.innerHTML = logger.innerHTML + s.replace("\n", "<br>") + "<br>";
                    conso(s);
                };
                console.log("===============  Match started  ==================\n\n");
                console.log("Match Commentry\n\n");
                match.start();

                console.log("\n\n=============  Scorecard  ===========================\n\n");

                var wicket = 0;
                for (var count = 0; count < players.length; count++)
                {
                    var player = players[count];
                    if (player.hasBatted())
                    {
                        if (!player.isOut)
                        {
                            wicket++;
                        }
                        console.log(player.name + " - " + player.getRunsScored() + (player.isOut ? "" : "*") + " (" + player.getBallsPlayed() + " balls)");
                    }
                }
                console.log("=============  Result  ===========================\n\n");
                if (match.score == match.requiredRuns - 1)
                {
                    debugger;
                    console.log("Match tied");
                }
                else if (match.score < match.requiredRuns)
                {
                    console.log("Team lost by " + (match.requiredRuns - match.score) + " runs.");

                }
                else
                {
                    console.log("Bengaluru won by " + wicket + " wicket" + (match.remainingBall > 0 ? " and " + match.remainingBall + " balls remaining." : "."));
                }
            }, 10);
        }
    }
}
