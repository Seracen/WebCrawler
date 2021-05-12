using System;
using System.Collections.Generic;
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

namespace WebCrawler
{
    /// <summary>
    /// Logika interakcji dla klasy NumberControl.xaml
    /// </summary>
    public partial class NumberControl : UserControl
    {
        public int chosenNumber { get; set; } = 1;
        public static readonly DependencyProperty minimal = DependencyProperty.Register("min", typeof(int), typeof(NumberControl), new PropertyMetadata(0));
        public static readonly DependencyProperty maximal = DependencyProperty.Register("max", typeof(int), typeof(NumberControl), new PropertyMetadata(0));
        public int min { get { return (int)GetValue(minimal); } set { SetValue(minimal, value); } }
        public int max { get { return (int)GetValue(maximal); } set { SetValue(maximal, value); } }
        public NumberControl()
        {
            InitializeComponent();
        }
        private void Up(object sender, RoutedEventArgs e)
        {
            int givenNumber;
            if(Int32.TryParse(number.Content.ToString(), out givenNumber))
            {
                if (givenNumber < max)
                {
                    givenNumber++;
                    chosenNumber = givenNumber;
                    number.Content = givenNumber;
                }
            }
        }
        private void Down(object sender, RoutedEventArgs e)
        {
            int givenNumber;
            if (Int32.TryParse(number.Content.ToString(), out givenNumber))
            {
                if (givenNumber > min)
                {
                    givenNumber--;
                    chosenNumber = givenNumber;
                    number.Content = givenNumber;
                }
            }
        }
    }
}
