using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            UIDisplay.ApplicationStartMessage();

            PlayerInfoModel testPlayer = UILogic.CreatePlayer("Test Player");

            UIDisplay.DisplayGrid(testPlayer.ShipLocations);
            
            Console.ReadLine();
        }
    }
}