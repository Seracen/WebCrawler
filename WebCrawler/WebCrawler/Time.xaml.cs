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
    /// Logika interakcji dla klasy Time.xaml
    /// </summary>
    public partial class Time : UserControl
    {
        public int timeHour { get; set; }
        public int timeMinute { get; set; } = 1;
        public int timeSecond { get; set; }
        public Time()
        {
            InitializeComponent();
        }
        public void hoursUp(object sender, RoutedEventArgs e)
        {
            int h = Int32.Parse(hours.Content.ToString());
            if (h < 24)
            {

                h++;
                
                hours.Content = h;
                timeHour = h;
            }
            else
            {
                hours.Content = 0;
                timeHour = 0;
            }
        }
        public void hoursDown(object sender, RoutedEventArgs e)
        {
            int h = Int32.Parse(hours.Content.ToString());
            if (h > 0)
            {
                h--;
                hours.Content = h;
                timeHour = h;
            }
            else
            {
                hours.Content = 24;
                timeHour = 24;
            }
        }
        public void minUp(object sender, RoutedEventArgs e)
        {
            int m = Int32.Parse(min.Content.ToString());
            if (m <59)
            {
                m+=1;
                min.Content = m;
                timeMinute = m;
            }
            else
            {
                min.Content = 0;
                timeMinute = 0;
            }
        }
        public void minDown(object sender, RoutedEventArgs e)
        {
            int m = Int32.Parse(min.Content.ToString());
            if (m > 0)
            {
                m-=1;
                min.Content = m;
                timeMinute = m;
            }
            else
            {
                min.Content = 59;
                timeMinute = 59;
            }
        }
        public void secUp(object sender, RoutedEventArgs e)
        {
            int s = Int32.Parse(sec.Content.ToString());
            if(s < 55)
            {
                s += 5;
                sec.Content = s;
                timeSecond = s;
            }
            else
            {
                sec.Content = 0;
                timeSecond = 0;
            }
        }
        public void secDown(object sender, RoutedEventArgs e)
        {
            int s = Int32.Parse(sec.Content.ToString());
            if (s > 0)
            {
                s -= 5;
                sec.Content = s;
                timeSecond = s;
            }
            else
            {
                sec.Content = 55;
                timeSecond = 55;
            }
        }
    }
}
