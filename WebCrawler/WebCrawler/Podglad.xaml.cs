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
using System.Windows.Shapes;
using WebCrawler.Class;
using QuickGraph;

namespace WebCrawler
{
    /// <summary>
    /// Logika interakcji dla klasy Podglad.xaml
    /// </summary>
    public partial class Podglad : Window
    {
        public Subsite head { get; set; }
        public BidirectionalGraph<object, IEdge<object>> graph = new BidirectionalGraph<object, IEdge<object>>();
        public Podglad()
        {
            InitializeComponent();
        }
        public void NewVertice(Subsite subsite)
        {
            graph.AddVertex(subsite.url);
            if (subsite.childList.Count != 0)
            {
                foreach (var child in subsite.childList)
                    NewVertice(child);
            }
        }
        public void NewEdges(Subsite subsite)
        {
            if(subsite.childList.Count != 0)
            {
                foreach(var child in subsite.childList)
                {
                    graph.AddEdge(new Edge<object>(subsite.url, child.url));
                    NewEdges(child);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NewVertice(head);
            NewEdges(head);
            graphLayout.Graph = graph;
        }
    }
}
