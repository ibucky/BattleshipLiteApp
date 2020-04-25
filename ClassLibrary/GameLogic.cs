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
        public static List<GridSpotModel> InitializeGrid()
        {
            List<GridSpotModel> grid = new List<GridSpotModel>();

            List<string> rows = new List<string>();
            rows.Add("A");
            rows.Add("B");
            rows.Add("C");
            rows.Add("D");
            rows.Add("E");

            for (int i = 0; i < rows.Count; i++)
            {
                string currentRowLetter = rows[i];

                List<int> column = new List<int>();
                column.Add(1);
                column.Add(2);
                column.Add(3);
                column.Add(4);
                column.Add(5);

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

        private static bool IsValidGridSpotSelection(GridSpotModel gridSpotInput, List<GridSpotModel> grid)
        {
            bool output = false;
            foreach (GridSpotModel gridSpot in grid)
            {
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
            foreach (GridSpotModel gridSpot in grid)
            {
                if (gridSpot.SpotLetter == gridSpotInput.SpotLetter & gridSpot.SpotNumber == gridSpotInput.SpotNumber)
                {
                    if (gridSpot.SpotStatus == GridSpotStatus.Empty)
                    {
                        output = true;
                    }
                }
            }

            return output;
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
                if (gridSpot.SpotLetter == gridSpotInput.SpotLetter & gridSpot.SpotNumber == gridSpotInput.SpotNumber)
                {
                    gridSpot.SpotStatus = GridSpotStatus.Ship;

                    return;
                }
            }
            throw new ApplicationException("Attempt to record ship placement has failed: could not match input gridSpot to active grid.");
        }
    }
}
