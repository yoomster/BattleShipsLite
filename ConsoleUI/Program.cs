using BattleShipLiteLibrary;
using BattleShipLiteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();

            PlayerInfoModel activePlayer = CreatePlayer("Player 1");
            PlayerInfoModel opponent = CreatePlayer("Player 2");
            PlayerInfoModel winner = null;

            do
            {
                DisplayShotGrid(activePlayer);

                RecordPlayerShot(activePlayer, opponent);
                bool doesGameContinue = GameLogic.PlayerStillActive(opponent);

                if (doesGameContinue == true)
                {
                    // swap possitions with tuple
                    (activePlayer, opponent) = (opponent, activePlayer);
                }
                else
                {
                    winner = activePlayer;
                }

            } while (winner == null);

            IdentifyWinner(winner);

            Console.WriteLine();
            Console.ReadLine();
        }

        private static void IdentifyWinner(PlayerInfoModel winner)
        {
            Console.WriteLine($"Congratulations {winner.FirstName}, you won");
            Console.WriteLine($"You took {GameLogic.GetShotCount(winner)} shots.");
        }

        private static void RecordPlayerShot(PlayerInfoModel activePlayer, PlayerInfoModel opponent)
        {                      
            bool isValidShot = false;
            string row = "";
            int column = 0;

            do
            {
                string shot = AskForShot(activePlayer);
                (row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);
                isValidShot = GameLogic.ValidateShot(activePlayer, row, column);

                if (isValidShot == false)
                {
                    Console.WriteLine("Invalid shot, plz try another.");
                }


            } while (isValidShot == false);

            //show & save shot results 
            bool isAHit = GameLogic.IdentifyShotResult(opponent, row, column);

            GameLogic.MarkShotResult(activePlayer, row, column, isAHit);
        } //WERKT

        private static string AskForShot(PlayerInfoModel activePlayer)
        {
            Console.WriteLine();
            Console.Write($"{activePlayer.FirstName} plz enter shot:");
            string output = Console.ReadLine();
            
            return output;
        } //WERKT

        private static void DisplayShotGrid(PlayerInfoModel activePlayer)
        {
            string currentRow = activePlayer.ShotGrid[0].SpotLetter;

            foreach (var gridSpot in activePlayer.ShotGrid)
            {
                if (gridSpot.SpotLetter != currentRow)
                {
                    Console.WriteLine();
                    currentRow = gridSpot.SpotLetter;
                }

                if (gridSpot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($"{gridSpot.SpotLetter}{gridSpot.SpotNumber} ");
                }                 
                else if (gridSpot.Status == GridSpotStatus.Hit)
                {
                    Console.Write(" X ");
                }
                else if (gridSpot.Status == GridSpotStatus.Miss)
                {
                    Console.Write(" O ");
                }
                else
                {
                    Console.Write(" ? ");
                }
            }
        } //WERKT

        public static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to the game BattleShips Lite, I hope you'll enjoy yourself");
            Console.WriteLine();
        } //WERKT

        public static PlayerInfoModel CreatePlayer(string playerTitle)
        {
            PlayerInfoModel output = new PlayerInfoModel();

            Console.WriteLine($"Player info for {playerTitle}");

            //Ask for users name
            output.FirstName = AskName();

            //Load up the grid
            GameLogic.InitializeGrid(output);

            //Ask use for 5 ship pacement
            PlaceShips(output);

            //Clear
            Console.Clear();

            return output;

        }  //WERKT

        public static string AskName()
        {
            Console.Write("What is your name: ");
            string firstName = Console.ReadLine(); ;

            return firstName;
        } //WERKT

        public static void PlaceShips(PlayerInfoModel model) 
        {
            do
            {
                Console.Write($"Where do you want to place ship number {model.ShipLocations.Count + 1} ");
                string location = Console.ReadLine();

                bool isValidLocation = GameLogic.StoreShip(model, location);

                if (isValidLocation == false)
                {
                    Console.WriteLine("invalid location, try again");
                }
            } while (model.ShipLocations.Count < 5);
        }  //WERKT
    }
}
