using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using ConsoleUI;

namespace ConsoleUI
{
    internal class UILogic
    {
        internal static PlayerInfoModel CreatePlayer(string playerTag)
        {
            Console.WriteLine($"Build {playerTag}");

            PlayerInfoModel newPlayer = new PlayerInfoModel();

            UIDisplay.GetPlayerName(newPlayer);
            UIDisplay.GetShipPlacements(newPlayer);

            Console.Clear();

            return newPlayer;
        }

        internal static GridSpotModel ParseStringToGridSpot(string input)
        {
            //Figures out if the length of the input is correct
            GridSpotModel gridSpot = new GridSpotModel();
            if (input.Length != 2)
            {
                //In order to get the grid to display accurately each time, the output is set to something that will be
                //erronius in the next level of method up instead of having to pass in an active grid into this method as well
                gridSpot.SpotLetter = "Z";
                gridSpot.SpotNumber = 9;

                return gridSpot;
            }
            
            //Net of if statements to assign the correct letter to the gridSpot
            if (input.Substring(0,1).ToLower() == "a")
            {
                gridSpot.SpotLetter = "A";
            }

            else if (input.Substring(0, 1).ToLower() == "b")
            {
                gridSpot.SpotLetter = "B";
            }

            else if (input.Substring(0, 1).ToLower() == "c")
            {
                gridSpot.SpotLetter = "C";
            }

            else if (input.Substring(0, 1).ToLower() == "d")
            {
                gridSpot.SpotLetter = "D";
            }

            else if (input.Substring(0, 1).ToLower() == "e")
            {
                gridSpot.SpotLetter = "E";
            }

            else
            {
                gridSpot.SpotLetter = "Z";
                gridSpot.SpotNumber = 9;

                return gridSpot;
            }

            //Tries to parse the second charicter of user's input into an int
            string inputNumberString = input.Substring(1, 1);
            bool isValidInputNumber = int.TryParse(inputNumberString, out int inputNumber);
            if (isValidInputNumber == false)
            {
                gridSpot.SpotLetter = "Z";
                gridSpot.SpotNumber = 9;

                return gridSpot;
            }

            //Net of if statements to assign the correct number to gridSpot
            if (inputNumber == 1)
            {
                gridSpot.SpotNumber = 1;
            }

            else if (inputNumber == 2)
            {
                gridSpot.SpotNumber = 2;
            }

            else if (inputNumber == 3)
            {
                gridSpot.SpotNumber = 3;
            }

            else if (inputNumber == 4)
            {
                gridSpot.SpotNumber = 4;
            }

            else if (inputNumber == 5)
            {
                gridSpot.SpotNumber = 5;
            }

            else
            {
                gridSpot.SpotLetter = "Z";
                gridSpot.SpotNumber = 9;

                return gridSpot;
            }

            return gridSpot;
        }        
    }
}
