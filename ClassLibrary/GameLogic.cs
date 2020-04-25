using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace ClassLibrary
{
    public class GameLogic
    {
        //Private Methods
        private static bool GridSpotHasShip(GridSpotModel gridSpotInput, List<GridSpotModel> grid)
        {
            bool output = false;
            //Searches the grid which was passed in
            foreach (GridSpotModel gridSpot in grid)
            {
                //and matches the spot in the grid which was passed in to the specific grid spot that was wassed in
                if (gridSpot.SpotLetter == gridSpotInput.SpotLetter & gridSpot.SpotNumber == gridSpotInput.SpotNumber)
                {
                    //then looks to see if the spot in the grid is "empty" status
                    if (gridSpot.SpotStatus == GridSpotStatus.Ship)
                    {
                        output = true;

                        return output;
                    }
                }
            }

            return output;
        }

        private static bool IsValidGridSpotSelection(GridSpotModel gridSpotInput, List<GridSpotModel> grid)
        {
            bool output = false;
            //Searches the grid that is passed in
            foreach (GridSpotModel gridSpot in grid)
            {
                //and determines if the gridspot object that was passed in matches a valid spot on the grid which was
                //passed in
                if (gridSpot.SpotLetter == gridSpotInput.SpotLetter & gridSpot.SpotNumber == gridSpotInput.SpotNumber)
                {
                    output = true;

                    return output;
                }

                else
                {
                    output = false;
                }
            }
            return output;
        }

        private static bool GridSpotIsEmpty(GridSpotModel gridSpotInput, List<GridSpotModel> grid)
        {
            bool output = false;
            //Searches the grid which was passed in
            foreach (GridSpotModel gridSpot in grid)
            {
                //and matches the spot in the grid which was passed in to the specific grid spot that was wassed in
                if (gridSpot.SpotLetter == gridSpotInput.SpotLetter & gridSpot.SpotNumber == gridSpotInput.SpotNumber)
                {
                    //then looks to see if the spot in the grid is "empty" status
                    if (gridSpot.SpotStatus == GridSpotStatus.Empty)
                    {
                        output = true;
                    }
                }
            }

            return output;
        }

        private static bool GridSpotAlreadyFiredOn(GridSpotModel gridSpotInput, List<GridSpotModel> grid)
        {
            bool output = false;
            //Searches the grid which was passed in
            foreach (GridSpotModel gridSpot in grid)
            {
                //and matches the spot in the grid which was passed in to the specific grid spot that was wassed in
                if (gridSpot.SpotLetter == gridSpotInput.SpotLetter & gridSpot.SpotNumber == gridSpotInput.SpotNumber)
                {
                    //then looks to see if the spot in the grid is "empty" status
                    if (gridSpot.SpotStatus == GridSpotStatus.Hit || gridSpot.SpotStatus == GridSpotStatus.Miss
                        || gridSpot.SpotStatus == GridSpotStatus.Sunk)
                    {
                        output = true;
                    }
                }
            }

            return output;
        }

        //Public Methods
        public static List<GridSpotModel> InitializeGrid()
        {
            List<GridSpotModel> grid = new List<GridSpotModel>();

            //Generates all of the rows for the grid
            List<string> rows = new List<string>();
            rows.Add("A");
            rows.Add("B");
            rows.Add("C");
            rows.Add("D");
            rows.Add("E");

            //Generates columns in each row
            for (int i = 0; i < rows.Count; i++)
            {
                string currentRowLetter = rows[i];

                List<int> column = new List<int>();
                column.Add(1);
                column.Add(2);
                column.Add(3);
                column.Add(4);
                column.Add(5);

                //As each spot on the grid is created with the loops and lists, a new GridSpotModel
                //object is instanciated with the applicable letter and number.  Spot status is set
                //to "empty" by default.
                for (int c = 0; c < column.Count; c++)
                {
                    int currentColumnNumber = column[c];

                    GridSpotModel gridSpot = new GridSpotModel();
                    gridSpot.SpotLetter = currentRowLetter;
                    gridSpot.SpotNumber = currentColumnNumber;
                    gridSpot.SpotStatus = GridSpotStatus.Empty;

                    grid.Add(gridSpot);
                }
            }

            return grid;
        }

        public static bool IsValidShipPlacement(GridSpotModel gridSpotInput, List<GridSpotModel> grid)
        {
            bool output = false;
            if (IsValidGridSpotSelection(gridSpotInput, grid) == true & GridSpotIsEmpty(gridSpotInput, grid) == true)
            {
                output = true;
            }

            else
            {
                output = false;
            }

            return output;
        }

        public static void RecordShipPlacement(GridSpotModel gridSpotInput, List<GridSpotModel> grid)
        {
            foreach (GridSpotModel gridSpot in grid)
            {
                //Final check to verify that the selected spot is valid
                if (gridSpot.SpotLetter == gridSpotInput.SpotLetter & gridSpot.SpotNumber == gridSpotInput.SpotNumber)
                {
                    //then records it if it is
                    gridSpot.SpotStatus = GridSpotStatus.Ship;

                    return;
                }
            }
            //If the selected spot is still not valid, somehting is seriosly wrong and this custom exception will be thrown.
            throw new ApplicationException("Attempt to record ship placement has failed: could not match input gridSpot to active grid.");
        }

        public static bool ValidateShot(GridSpotModel shot, PlayerInfoModel opponent)
        {
            bool output = false;

            //Compares that shot to the opponent's ship locations grid to see if it's on the grid and if they
            //have already fired on that spot
            bool isValidGridSpot = IsValidGridSpotSelection(shot, opponent.ShipLocations);
            bool isEmptyGridSpot = GridSpotIsEmpty(shot, opponent.ShipLocations);
            bool shipInGridSpot = GridSpotHasShip(shot, opponent.ShipLocations);
            bool alreadyFiredOn = GridSpotAlreadyFiredOn(shot, opponent.ShipLocations);

            if (isValidGridSpot == true || isEmptyGridSpot == true || alreadyFiredOn == false)
            {
                output = true;

                return output;
            }

            //If the spot is "empty" or "ship" the shot is valid
            else
            {
                output = false;

                return output;
            }
        }

        public static bool IsAHit(GridSpotModel shot, PlayerInfoModel opponent)
        {
            bool output = false;

            if (GridSpotHasShip(shot, opponent.ShipLocations) == true)
            {
                output = true;

                return output;
            }

            else
            {
                output = false;

                return output;
            }
        }

        public static int QuantifyRemainingShips(PlayerInfoModel player)
        {
            int shipCount = 0;
            //Search player's shipgrid for ships
            foreach (GridSpotModel gridSpot in player.ShipLocations)
            {
                bool shipExists = GridSpotHasShip(gridSpot, player.ShipLocations);
               
                //Every ship the search encounters adds one more to the ship count
                if (shipExists == true)
                {
                    shipCount++;
                }
            }

            return shipCount;
        }

        public static void RecordShotToOpponentShipGrid(GridSpotModel shot, PlayerInfoModel opponent)
        {
            //Changes opponent's ship grid
            foreach (GridSpotModel gridSpot in opponent.ShipLocations)
            {
                if (shot.SpotLetter == gridSpot.SpotLetter || shot.SpotNumber == gridSpot.SpotNumber)
                {
                    if (gridSpot.SpotStatus == GridSpotStatus.Ship)
                    {
                        gridSpot.SpotStatus = GridSpotStatus.Sunk;

                        return;
                    }

                    else if (gridSpot.SpotStatus == GridSpotStatus.Empty)
                    {
                        gridSpot.SpotStatus = GridSpotStatus.Miss;

                        return;
                    }
                }
            }
            //If the method fails to record a shot on both grids, this exeption is thrown.
            throw new ApplicationException("Failed to record the shot.");
        }

        public static void RecordShotToActivePlayerShotsGrid(GridSpotModel shot, PlayerInfoModel activePlayer)
        {
            //Changes activePlayer's shot grid
            foreach (GridSpotModel gridSpot in activePlayer.ShotsGrid)
            {
                if (shot.SpotLetter == gridSpot.SpotLetter || shot.SpotNumber == gridSpot.SpotNumber)
                {
                    if (gridSpot.SpotStatus == GridSpotStatus.Ship)
                    {
                        gridSpot.SpotStatus = GridSpotStatus.Hit;

                        return;
                    }

                    else if (gridSpot.SpotStatus == GridSpotStatus.Empty)
                    {
                        gridSpot.SpotStatus = GridSpotStatus.Miss;

                        return;
                    }
                }
            }
            //If the method fails to record a shot on both grids, this exeption is thrown.
            throw new ApplicationException("Failed to record the shot.");
        }
            
        public static bool GameIsOver(PlayerInfoModel opponent)
        {
            bool output = false;

            if (opponent.RemainingShips == 0)
            {
                output = true;

                return output;
            }

            else
            {
                output = false;

                return output;
            }
        }
    }
}
