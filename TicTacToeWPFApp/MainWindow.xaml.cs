using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace TicTacToeWPFApp
{
    class Board
    {
        public string currentPlayer;
        private string[,] ticTacToeGrid;

        public Board()
        {
            ticTacToeGrid = new string[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.ticTacToeGrid[i, j] = "";
                }
            }
            this.currentPlayer = "X";
        }

        public string checkIfPlayerWins(string player)
        {
            for (int row = 0; row < ticTacToeGrid.GetLength(0); row++)
            {
                if (ticTacToeGrid[row, 0] == player && ticTacToeGrid[row, 1] == player && ticTacToeGrid[row, 2] == player)
                {
                    return currentPlayer + " Wins";
                }
            }
            for (int column = 0; column < ticTacToeGrid.GetLength(1); column++)
            {
                if (ticTacToeGrid[0, column] == player && ticTacToeGrid[1, column] == player && ticTacToeGrid[2, column] == player)
                {
                    return currentPlayer + " Wins";
                }
            }

            if (ticTacToeGrid[0, 0] == player && ticTacToeGrid[1, 1] == player && ticTacToeGrid[2, 2] == player)
            {
                return currentPlayer + " Wins";
            }
            if (ticTacToeGrid[0, 2] == player && ticTacToeGrid[1, 1] == player && ticTacToeGrid[2, 0] == player)
            {
                return currentPlayer + " Wins";
            }
            return "";
        }

        public bool isDraw()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (ticTacToeGrid[i, j] == "")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void setField(int x, int y, string value)
        {
            this.ticTacToeGrid[x, y] = value;
        }

        public void switchCurrentPlayer()
        {
            if (currentPlayer == "X")
            {
                currentPlayer = "O";
            }
            else
            {
                currentPlayer = "X";
            }
        }
        public void reset()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.ticTacToeGrid[i, j] = "";
                }
            }
            this.currentPlayer = "X";
        }
    }
    public partial class MainWindow : Window
    {
        Board board = new Board();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button00_Click(object sender, RoutedEventArgs e)
        {
            board.setField(0, 0, board.currentPlayer);
            Button00.IsEnabled = false;
            Button00.Content = board.currentPlayer;
            Display.Text = board.checkIfPlayerWins(board.currentPlayer);
            if (Display.Text != "")
            {
                ResetButton.IsEnabled = true;
            }
            else if (board.isDraw())
            {
                Display.Text = "Draw";
                ResetButton.IsEnabled = true;
            }
            else
            {
                board.switchCurrentPlayer();
            }
        }

        private void Button10_Click(object sender, RoutedEventArgs e)
        {
            board.setField(1, 0, board.currentPlayer);
            Button10.IsEnabled = false;
            Button10.Content = board.currentPlayer;
            Display.Text = board.checkIfPlayerWins(board.currentPlayer);
            if (Display.Text != "")
            {
                ResetButton.IsEnabled = true;
            }
            else if (board.isDraw())
            {
                Display.Text = "Draw";
                ResetButton.IsEnabled = true;
            }
            else
            {
                board.switchCurrentPlayer();
            }
        }

        private void Button20_Click(object sender, RoutedEventArgs e)
        {
            board.setField(2, 0, board.currentPlayer);
            Button20.IsEnabled = false;
            Button20.Content = board.currentPlayer;
            Display.Text = board.checkIfPlayerWins(board.currentPlayer);
            if (Display.Text != "")
            {
                ResetButton.IsEnabled = true;
            }
            else if (board.isDraw())
            {
                Display.Text = "Draw";
                ResetButton.IsEnabled = true;
            }
            else
            {
                board.switchCurrentPlayer();
            }
        }

        private void Button01_Click(object sender, RoutedEventArgs e)
        {
            board.setField(0, 1, board.currentPlayer);
            Button01.IsEnabled = false;
            Button01.Content = board.currentPlayer;
            Display.Text = board.checkIfPlayerWins(board.currentPlayer);
            if (Display.Text != "")
            {
                ResetButton.IsEnabled = true;
            }
            else if (board.isDraw())
            {
                Display.Text = "Draw";
                ResetButton.IsEnabled = true;
            }
            else
            {
                board.switchCurrentPlayer();
            }
        }

        private void Button11_Click(object sender, RoutedEventArgs e)
        {
            board.setField(1, 1, board.currentPlayer);
            Button11.IsEnabled = false;
            Button11.Content = board.currentPlayer;
            Display.Text = board.checkIfPlayerWins(board.currentPlayer);
            if (Display.Text != "")
            {
                ResetButton.IsEnabled = true;
            }
            else if (board.isDraw())
            {
                Display.Text = "Draw";
                ResetButton.IsEnabled = true;
            }
            else
            {
                board.switchCurrentPlayer();
            }
        }

        private void Button21_Click(object sender, RoutedEventArgs e)
        {
            board.setField(2, 1, board.currentPlayer);
            Button21.IsEnabled = false;
            Button21.Content = board.currentPlayer;
            Display.Text = board.checkIfPlayerWins(board.currentPlayer);
            if (Display.Text != "")
            {
                ResetButton.IsEnabled = true;
            }
            else if (board.isDraw())
            {
                Display.Text = "Draw";
                ResetButton.IsEnabled = true;
            }
            else
            {
                board.switchCurrentPlayer();
            }
        }

        private void Button02_Click(object sender, RoutedEventArgs e)
        {
            board.setField(0, 2, board.currentPlayer);
            Button02.IsEnabled = false;
            Button02.Content = board.currentPlayer;
            Display.Text = board.checkIfPlayerWins(board.currentPlayer);
            if (Display.Text != "")
            {
                ResetButton.IsEnabled = true;
            }
            else if (board.isDraw())
            {
                Display.Text = "Draw";
                ResetButton.IsEnabled = true;
            }
            else
            {
                board.switchCurrentPlayer();
            }
        }

        private void Button12_Click(object sender, RoutedEventArgs e)
        {
            board.setField(1, 2, board.currentPlayer);
            Button12.IsEnabled = false;
            Button12.Content = board.currentPlayer;
            Display.Text = board.checkIfPlayerWins(board.currentPlayer);
            if (Display.Text != "")
            {
                ResetButton.IsEnabled = true;
            }
            else if (board.isDraw())
            {
                Display.Text = "Draw";
                ResetButton.IsEnabled = true;
            }
            else
            {
                board.switchCurrentPlayer();
            }
        }

        private void Button22_Click(object sender, RoutedEventArgs e)
        {
            board.setField(2, 2, board.currentPlayer);
            Button22.IsEnabled = false;
            Button22.Content = board.currentPlayer;
            Display.Text = board.checkIfPlayerWins(board.currentPlayer);
            if (Display.Text != "")
            {
                ResetButton.IsEnabled = true;
            }
            else if (board.isDraw())
            {
                Display.Text = "Draw";
                ResetButton.IsEnabled = true;
            }
            else
            {
                board.switchCurrentPlayer();
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            board.reset();
            Button00.Content = "";
            Button00.IsEnabled = true;
            Button10.Content = "";
            Button10.IsEnabled = true;
            Button20.Content = "";
            Button20.IsEnabled = true;
            Button01.Content = "";
            Button01.IsEnabled = true;
            Button11.Content = "";
            Button11.IsEnabled = true;
            Button21.Content = "";
            Button21.IsEnabled = true;
            Button02.Content = "";
            Button02.IsEnabled = true;
            Button12.Content = "";
            Button12.IsEnabled = true;
            Button22.Content = "";
            Button22.IsEnabled = true;
            Display.Text = "";
            ResetButton.IsEnabled = false;
        }
    }
}