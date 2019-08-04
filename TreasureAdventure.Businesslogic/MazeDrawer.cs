using System;
using System.Collections.Generic;
using System.Text;

namespace TreasureAdventure.Businesslogic
{
    public static class MazeDrawer
    {

        internal static void DrawBox(int size, int visitRoom, char[,] mazeVisited)
        {
            // Draw top line, north indicator
            int roomId = 0;
            for (int row = 0; row < size; row++)
            {
                char[] arr = new char[size];
                for (int col = 0; col < size; col++)
                {
                    //space at every avalable place at the beginning
                    roomId++; 
                    if(roomId == visitRoom || mazeVisited[row, col] == 'V')
                    {
                        mazeVisited[row, col] = 'V';
                        arr[col] = 'v';
                    }
                    else
                    {
                        mazeVisited[row, col] = ' ';
                        arr[col] = ' ';
                    }
                    
                      
                  
                }
                Console.WriteLine($" { string.Join("  ", arr)} ");
                
            }
            
        }
    }
}
