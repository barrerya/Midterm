using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Midterm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int gridsize = 3;
        int currentSize = 0;
        string[,] charGrid;

        public MainWindow()
        {
            InitializeComponent();
        }

        //button to generate new string grid
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            charGrid = generateRandomCharGrid();
            displayGrid(charGrid);
        }

        //set next grid to 3x3
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            gridsize = 3;
        }

        //set next grid to 4x4
        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            gridsize = 4;
        }

        //set next grid to 5x5
        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            gridsize = 5;
        }

        //set next grid to 6x6
        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            gridsize = 6;
        }

        //method to generate grid of characters
        private string[,] generateRandomCharGrid()
        {
            Random random = new Random();
            string[,] stringGrid = new string[gridsize, gridsize];
            string[] letters = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            for(int x = 0; x < gridsize; x++)
            {
                for (int y = 0; y < gridsize; y++)
                {
                    int nextRandom = random.Next(0,26);
                    stringGrid[x,y] = letters[nextRandom];
                }
            }
            currentSize = gridsize;
            return stringGrid;
        }

        //method to displaygrid in the text box
        private void displayGrid(String[,] stringGrid)
        {
            string output = "";
            for (int x = 0; x < gridsize; x++)
            {
                for (int y = 0; y < gridsize; y++)
                {
                    output += (stringGrid[x,y] + " ");
                }
                output += Environment.NewLine;
            }
            tbOutput.Text = output;
            using (StreamWriter writer = new StreamWriter("sometext.txt"))
            {
                writer.Write(tbOutput.Text);
            }
        }

        //when search button is pressed
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String searchTerm = tbSearch.Text;
            searchTerm = searchTerm.ToUpper();
            int timesFound = gridSearch(searchTerm);
            
            if(timesFound == 0)
            {
                svSearchOutput.Content += (Environment.NewLine + searchTerm + " has no instances of it in the grid!");
            }
            else if(timesFound > 0)
            {
                svSearchOutput.Content += (Environment.NewLine + searchTerm + " has " + timesFound + " instance(s) of it in the grid!");
            }
            svSearchOutput.Content += (Environment.NewLine);


        }

        //function to search through the string grid
        private int gridSearch(String searchTerm)
        {
            int timesFound = 0;
            string tmpString;
            


            //no point searching if the given string is larger than any possible string or the grid hasn't been generated
            if (searchTerm.Length > currentSize || currentSize == 0)
            {
                return 0;
            }

            //if the search term is one character
            else if (searchTerm.Length == 1)
            {
                //horizontal search
                for (int x = 0; x < currentSize; x++)
                {
                    for (int y = 0; y < currentSize; y++)
                    {
                        if (charGrid[x, y].Equals(searchTerm)) {timesFound++;}
                    }
                }

                //vertical search
                for (int x = 0; x < currentSize; x++)
                {
                    for (int y = 0; y < currentSize; y++)
                    {
                        if (charGrid[y, x].Equals(searchTerm)) { timesFound++; }
                    }
                }

            }

            //if the search term is smaller than the grid and more than one character
            else
            {
                int difference = currentSize - searchTerm.Length;

                //horizontal search
                for (int x = 0; x < currentSize; x++)
                {
                    for (int d = 0; d <= difference; d++)
                    {
                        tmpString = "";
                        for (int y = 0+d; y < searchTerm.Length+d; y++)
                        {
                            tmpString += (charGrid[x, y]);
                        }
                        
                        if (tmpString.Equals(searchTerm))
                        {
                            
                            timesFound++;
                        }
                    }
                }

                //vertical search
                for (int y = 0; y < currentSize; y++)
                {
                    for (int d = 0; d <= difference; d++)
                    {
                        tmpString = "";
                        for (int x = 0+d; x < searchTerm.Length + d; x++)
                        {
                            tmpString += (charGrid[x, y]);
                        }
                        
                        if (tmpString.Equals(searchTerm))
                        {
                            
                            timesFound++;
                        }
                    }
                }
            }

            return timesFound;
        }

        //hides grid from user
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            tbOutput.Visibility = Visibility.Hidden;
        }

        //shows grid again
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            tbOutput.Visibility = Visibility.Visible;
        }
    }
}
