using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Output = System.Console;

namespace Lab_assignment_1_TIc_Tac_Toe {
   //Main function calls tic tac toe game logic
   class Program {
      static void Main (string[] args) {
         TicTacToe game = new TicTacToe();
         game.PrintBoard();
         game.Play();
      }
   }
   //Tic tac toe game class
   public class TicTacToe {
      private const int BOARDSIZE = 3;
      private int[,] board;
      int currentPlayer;

      //Constructor for the board 
      public TicTacToe () {
         this.board = new int[BOARDSIZE, BOARDSIZE];
      }
      //Prints the board 
      public void PrintBoard () {
         Console.WriteLine("Print board ");
         for (int i = 0; i < BOARDSIZE; i++) {
            for (int j = 0; j < BOARDSIZE; j++) {
               if (board[i, j] == 1) {
                  Console.Write(" X ");
               }
               else if (board[i, j] == -1) {
                  Console.Write(" O ");
               }
               else {
                  Console.Write(" . ");
               }

            }
            Console.WriteLine();
         }
      }
      //Main game logic loop 
      public void Play () {
         Console.WriteLine("Play method");
         int gamecount = 0;
         bool gameState = true;
         currentPlayer = 1;
         int row, col;
         string x;

         while (gameState || gamecount > 9) {
            if (currentPlayer % 2 <= 0) {
               x = "O";
            }
            else {
               x = "X";
            }
            if (gamecount == 9) {
               Output.WriteLine("Game is a draw ");

               break;
            }
            gamecount++;
            Console.WriteLine("Player " + x + "'s turn: ");
            Console.Write("Player " + x + " : Enter row ( 0 <= row < 3):  ");
            string rowInput = Console.ReadLine();
            Console.Write("Player " + x + " : Enter column ( 0 <= row < 3):  ");
            string colInput = Console.ReadLine();

            Int32.TryParse(rowInput, out row);
            Int32.TryParse(colInput, out col);

            while (!inBounds(row, col) || !isValidMove(row,col)) {
               Output.WriteLine("Invalid move, please enter a valid move");
               Console.WriteLine("Player " + x + "'s turn: ");
               Console.Write("Player " + x + " : Enter row ( 0 <= row < 3):  ");
               rowInput = Console.ReadLine();
               Console.Write("Player " + x + " : Enter column ( 0 <= row < 3):  ");
               colInput = Console.ReadLine();

               Int32.TryParse(rowInput, out row);
               Int32.TryParse(colInput, out col);
            }

            board[row, col] = currentPlayer;
            PrintBoard();
            if (winCheck(row, col)) {
               gameState = false;
               Output.WriteLine("Player " + x + " wins ");
               break;
            }

            currentPlayer *= -1;
         }
      }
      //Method that checks if user input is inbounds
      public bool inBounds (int a, int b) {
         return ( a < BOARDSIZE ) && ( a > -1 ) &&
         ( b < BOARDSIZE ) && ( b > -1 );
      }
      //Mehtod that checks to see if the input is valid and not 
      //taken already
      public bool isValidMove (int a, int b) {
         return board[a, b] == 0;
      }
      //Method that checks to see if the user has won 
      public bool winCheck (int a, int b) {
         int count = 0, row = 0, col = 0;
         int p;
         for (int r = 0; r < BOARDSIZE; r++) {
            for (int c = 0; c < BOARDSIZE; c++) {
               p = ( board[r, c] );
               if (board[r, c] != 0) {
                  for (int k = -1; k < 2; k++) {
                     for (int l = -1; l < 2; l++) {
                        row = r;
                        col = c;
                        count = 0;
                        if (k == 0 && l == 0) {
                           break;
                        }
                        if (inBounds(row + k, col + l)) {
                           if (board[row + k, col + l] == p) { 
                              count++;
                              row += k;
                              col += l;
                              if (inBounds(row + k, col + l)) {
                                 if (board[row + k, col + l] == p) { 
                                    count++;
                                    if (!inBounds(row + k, col + l) || board[row + k, col + l] == 0) {
                                       break;
                                    }
                                    if (count == 2) {
                                       return true;
                                    }
                                 }
                              }

                           }
                        }

                     }
                  }
               }
            }
         }
         return false;
      }
   }
}