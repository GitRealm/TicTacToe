// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="N/A">
//   Mike Medford
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TicTacToe
{
    using System.Net.WebSockets;
    using System.Resources;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Playable buttons
        /// </summary>
        private readonly Button[,] boardButtons;

        /// <summary>
        /// Value to keep track of current player
        /// 0 = Player 1
        /// 1 = Player 2
        /// </summary>
        private int turnCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            this.boardButtons = new Button[3, 3]
            {
                { this.Btn11, this.Btn12, this.Btn13 }, 
                { this.Btn21, this.Btn22, this.Btn23 },
                { this.Btn31, this.Btn32, this.Btn33 }
            };

            this.turnCount = 0;
            this.LblP1.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// The button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = (Button)sender;
            clickedButton.IsEnabled = false;
            this.turnCount++;
            if (this.turnCount % 2 == 0)
            {
                // Player 2
                clickedButton.Content = "O";
                if (!this.CheckWin("O", clickedButton.Name))
                {
                    this.LblP1.Visibility = Visibility.Visible;
                    this.LblP2.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                // Player 1
                clickedButton.Content = "X";
                if (!this.CheckWin("X", clickedButton.Name))
                {
                    this.LblP2.Visibility = Visibility.Visible;
                    this.LblP1.Visibility = Visibility.Hidden;
                }
            }


            if (this.turnCount <= 9)
            {
                return;
            }

            MessageBox.Show("Tie Game!");
            this.Reset();
        }

        /// <summary>
        /// Reset board
        /// </summary>
        private void Reset()
        {
            this.Btn11.IsEnabled = true;
            this.Btn11.Content = string.Empty;

            this.Btn12.IsEnabled = true;
            this.Btn12.Content = string.Empty;

            this.Btn13.IsEnabled = true;
            this.Btn13.Content = string.Empty;

            this.Btn21.IsEnabled = true;
            this.Btn21.Content = string.Empty;

            this.Btn22.IsEnabled = true;
            this.Btn22.Content = string.Empty;

            this.Btn23.IsEnabled = true;
            this.Btn23.Content = string.Empty;

            this.Btn31.IsEnabled = true;
            this.Btn31.Content = string.Empty;

            this.Btn32.IsEnabled = true;
            this.Btn32.Content = string.Empty;

            this.Btn33.IsEnabled = true;
            this.Btn33.Content = string.Empty;

            this.turnCount = 0;
            this.LblP1.Visibility = Visibility.Visible;
            this.LblP2.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Check board to see if player has won
        /// </summary>
        /// <param name="value">
        /// The value to check for win.
        /// </param>
        /// <param name="button">
        /// Name of the button that was clicked.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool CheckWin(string value, string button)
        {
            var row = int.Parse(button.Substring(3, 1));
            var column = int.Parse(button.Substring(4, 1));

            if (this.CheckColumn(value, column - 1) 
                || this.CheckRow(value, row - 1) 
                || this.CheckDiagonal1(value) 
                || this.CheckDiagonal2(value))
            {
                MessageBox.Show("WIN!");
                this.Reset();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check row for win
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool CheckRow(string value, int row)
        {
            // Check row win
            for (var i = 0; i < 3; i++)
            {
                if (!this.boardButtons[row, i].HasContent)
                {
                    return false;
                }

                if (!this.boardButtons[row, i].Content.Equals(value))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check column for win
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="column">
        /// The column.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool CheckColumn(string value, int column)
        {
            // Check column win
            for (var i = 0; i < 3; i++)
            {
                if (!this.boardButtons[i, column].HasContent)
                {
                    return false;
                }

                if (!this.boardButtons[i, column].Content.Equals(value))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check diagonal for win
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool CheckDiagonal1(string value)
        {
            var column = 0;
            for (var i = 0; i < 3; i++)
            {
                if (!this.boardButtons[column, i].HasContent)
                {
                    return false;
                }

                if (!this.boardButtons[column, i].Content.Equals(value))
                {
                    return false;
                }

                column++;   
            }

            return true;
        }

        /// <summary>
        /// Check diagonal for win
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool CheckDiagonal2(string value)
        {
            var column = 0;
            for (var i = 2; i > -1; i--)
            {
                if (!this.boardButtons[column, i].HasContent)
                {
                    return false;
                }

                if (!this.boardButtons[column, i].Content.Equals(value))
                {
                    return false;
                }

                column++;
            }

            return true;
        }
    }
}
