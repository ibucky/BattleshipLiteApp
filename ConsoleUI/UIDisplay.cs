using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    internal class UIDisplay
    {
        //Private Methods
        private static string GetShipPlacementSelection(int placementListCount)
        {
            Console.Write($"Where would you like to place ship {placementListCount + 1}: ");
            string output = Console.ReadLine();

            return output;
        }

        //Internal Methods
        internal static void ApplicationStartMessage()
        {
            Console.WriteLine("Welcome to Battleship Lite!");
            Console.WriteLine("This app was created by Bucky Harmos");
        }

        internal static void GetPlayerName(PlayerInfoModel player)
        {
            Console.Write("Please enter your name: ");
            player.PlayerName = Console.ReadLine();
        }

        internal static void DisplayGrid(List<GridSpotModel> grid)
        {  
            string currentGridSpotLetter = "A";

            //Writes the numbers above the grid
            Console.Write("  1  2  3  4  5 ");
            Console.WriteLine("");
            Console.Write(currentGridSpotLetter);

            foreach (GridSpotModel gridSpot in grid)
            {
                //Makes a line break and writes the line letter
                if (currentGridSpotLetter != gridSpot.SpotLetter)
                {
                    Console.WriteLine("");
                    currentGridSpotLetter = gridSpot.SpotLetter;
                    Console.Write(currentGridSpotLetter);
                }

                //Series of if statements that determine what to show based on gridSpot status
                if (gridSpot.SpotStatus == GridSpotStatus.Empty)
                {
                    Console.Write(" ~ "); 
                }

                else if (gridSpot.SpotStatus == GridSpotStatus.Hit)
                {
                    Console.Write(" X ");
                }

                else if (gridSpot.SpotStatus == GridSpotStatus.Miss)
                {
                    Console.Write(" O ");
                }

                else if (gridSpot.SpotStatus == GridSpotStatus.Ship)
                {
                    Console.Write(" V ");
                }

                else if (gridSpot.SpotStatus == GridSpotStatus.Sunk)
                {
                    Console.Write(" ! ");
                }

                else
                {
                    Console.Write(" ? ");
                }
            }
        }

        internal static void GetShipPlacements(PlayerInfoModel newPlayer)
        {
            //Initialize a grid for the player's ship placements
            newPlayer.ShipLocations =  GameLogic.InitializeGrid();

            List<GridSpotModel> shipPlacements = new List<GridSpotModel>();

            bool isValidPlacement = false;
            do
            {
                DisplayGrid(newPlayer.ShipLocations);
                Console.WriteLine("");


                //Ask player for their next ship location

                string newPlacement = GetShipPlacementSelection(shipPlacements.Count);

                //Parse selection into GridSpotModel.GridSpotLetter and GridSpotNumber
                GridSpotModel gridSpotSelection = UILogic.ParseStringToGridSpot(newPlacement);


                //Check to see if that spot is a valid spot on the grid or if they have already placed a ship there
                isValidPlacement = GameLogic.IsValidShipPlacement(gridSpotSelection, newPlayer.ShipLocations);
                while (isValidPlacement == false)
                {
                    Console.Clear();

                    Console.WriteLine("Oops!  That's not a valid selection on your grid.  Please try again.");

                    DisplayGrid(newPlayer.ShipLocations);
                    Console.WriteLine("");

                    newPlacement = GetShipPlacementSelection(shipPlacements.Count);

                    gridSpotSelection = UILogic.ParseStringToGridSpot(newPlacement);

                    isValidPlacement = GameLogic.IsValidShipPlacement(gridSpotSelection, newPlayer.ShipLocations);
                }

                //Mark gridspot status as "ship" if the selection is valid
                GameLogic.RecordShipPlacement(gridSpotSelection, newPlayer.ShipLocations);

                shipPlacements.Add(gridSpotSelection);

                Console.Clear();

            //Loop back through until player has placed all five ships
            } while (shipPlacements.Count < 5);
        }

        internal static GridSpotModel AskForShot()
        {
            Console.WriteLine("Where would you like to fire: ");
            string gridSpotSelection = Console.ReadLine();

            GridSpotModel output = UILogic.ParseStringToGridSpot(gridSpotSelection);

            return output;
        }

        internal static void DisplayHitMessage(PlayerInfoModel opponent)
        {
            Console.Clear();
            Console.WriteLine("That's a HIT!!!");
            Console.WriteLine($"{opponent.PlayerName} still has {opponent.RemainingShips} ships left.");
            Console.WriteLine("Press enter to continue.");

            Console.ReadLine();
        }

        internal static void DisplayMissMessage()
        {
            Console.WriteLine("You missed!");
            Console.WriteLine("Press enter to continue.");

            Console.ReadLine();
        }

        internal static void SwitchPlayersMessage(PlayerInfoModel opponent)
        {
            Console.WriteLine($"It's {opponent.PlayerName} turn next.");
            Console.WriteLine($"When {opponent.PlayerName} is ready to take their turn, press enter.");
        }

        internal static void DisplayTurnHeader(PlayerInfoModel activePlayer, PlayerInfoModel opponent)
        {
            Console.WriteLine($"It is now {activePlayer.PlayerName}'s turn.");
            Console.WriteLine($"{activePlayer.PlayerName} has sunk {5 - opponent.RemainingShips} of {opponent.PlayerName}'s ships.");
            Console.WriteLine("Press enter to continue.");

            Console.ReadLine();
        }
    }
}
