using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
using WebCrawler.Class;
using System.Threading;
using System.Collections.Concurrent;
using Microsoft.Win32;

namespace WebCrawler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Options options = new Options();
        public const int timeoutDelayMS = 5000;
        public ConcurrentQueue<Subsite> linksQueue = new ConcurrentQueue<Subsite>();
        public ConcurrentStack<Subsite> linksStack = new ConcurrentStack<Subsite>();
        public ConcurrentDictionary<int, Subsite> visitedLinksDictionary = new ConcurrentDictionary<int, Subsite>();
        public int numberOfThreads = 1;
        public Subsite core;
        public bool isDFS { get { return options.method == "DFS"; } }
        public MainWindow()
        {
            InitializeComponent();
            chosenNumberOfThreads.min = 1;
            chosenNumberOfThreads.max = 5;
            depth.min = 1;
            depth.max = 100;

        }
        private void MethodType(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            switch (radioButton.Content.ToString())
            {
                case "BFS":
                    {
                        options.method = "BFS";
                        break;
                    }
                case "DFS":
                    {
                        options.method = "DFS";
                        break;
                    }
            }
        }

        private void Podglad(object sender, MouseButtonEventArgs e)
        {
            if (core != null)
            {

                Podglad okno = new Podglad();
                okno.head = core;
                if (okno.ShowDialog() == true)
                {

                }
                else
                {

                }
            }
        }
        private async void Search(object sender, RoutedEventArgs e)
        { 
            visitedLinksDictionary.Clear();
            linksStack.Clear();
            linksQueue.Clear();
            string url = uRLAddress.Text;
            bool isUri = Uri.IsWellFormedUriString(url, UriKind.Absolute);
            if (isUri)
            {
                options.url = url;
                options.workTime = new TimeSpan(workTime.timeHour, workTime.timeMinute, workTime.timeSecond);
                numberOfThreads = chosenNumberOfThreads.chosenNumber;
                options.depth = depth.chosenNumber;
                core = new Subsite(0, url, null);
                linksListView.Items.Clear();
                linksListView.Items.Add("Loading data...");
                AddLinkToVisit(core);
                StartSearching();
            }
            else
            {
                MessageBox.Show("Given URL address is incorrect.");
            }
        }
        private async Task<List<Subsite>> GetSiteContent(Subsite subsite)
        {
            var timeoutToken = new CancellationTokenSource(timeoutDelayMS);
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = await web.LoadFromWebAsync(subsite.url, timeoutToken.Token);
            subsite.body = doc.DocumentNode.SelectNodes("//body").FirstOrDefault()?.InnerText;
            var allLinks = doc.DocumentNode.Descendants("a").Select(x =>
            {
                var toReturn = x.GetAttributeValue("href", "--");
                toReturn = toReturn[toReturn.Length - 1] == '/' ? toReturn.Substring(0, toReturn.Length - 1) : toReturn;
                return toReturn[0] == '/' ? $"{web.ResponseUri}/{toReturn.Substring(1)}" : toReturn;
            }).Where(x => x != "--" && !WasVisited(x));
            List<Subsite> allSubsiteList = new List<Subsite>();
            foreach (var a in allLinks)
            {
                allSubsiteList.Add(new Subsite(subsite.levelOfDepth + 1, a, subsite));
            }
            subsite.childList = allSubsiteList;
            subsite.status = Subsite.SubsiteStatus.FINISHED;
            return allSubsiteList;
        }
        public bool WasVisited(string url)
        {
            if (isDFS)
            {
                return linksQueue.Any(x => x.url == url) || visitedLinksDictionary.Any(x => x.Value.url == url);
            }
            else
            {
                return linksStack.Any(x => x.url == url) || visitedLinksDictionary.Any(x => x.Value.url == url);
            }
        }
        private async Task ProcessSearching(CancellationTokenSource token)
        {
            while (true)
            {
                var toProcess = GetLinkToVisit();
                if (token.IsCancellationRequested)
                {
                    if (toProcess != null)
                    {
                        toProcess.status = Subsite.SubsiteStatus.CANCELLED;
                    }
                    return;
                }
                else if (toProcess != null)
                {
                    try
                    {
                        IEnumerable<Subsite> allLinksFromGivenSite = await GetSiteContent(toProcess);
                        foreach (var a in allLinksFromGivenSite)
                        {
                            if (a.levelOfDepth >= options.depth)
                            {
                                a.status = Subsite.SubsiteStatus.MAXDEPTHCANCELLED;
                                visitedLinksDictionary.TryAdd(visitedLinksDictionary.Count, a);
                            }
                            else
                            {
                                AddLinkToVisit(a);
                            }
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        toProcess.status = Subsite.SubsiteStatus.TIMEOUT;
                    }

                    catch (Exception)
                    {
                        toProcess.status = Subsite.SubsiteStatus.FAILED;
                    }

                }
                else
                {
                    if (ShouldWait())
                    {
                        await Task.Delay(100);
                    }
                }
            }
        }
        public bool ShouldWait()
        {
            if (isDFS)
            {
                return visitedLinksDictionary.Any(x => x.Value.status == Subsite.SubsiteStatus.INPROGRESS) || linksQueue.Count != 0;
            }
            else
            {
                return visitedLinksDictionary.Any(x => x.Value.status == Subsite.SubsiteStatus.INPROGRESS) || linksStack.Count != 0;
            }
        }
        public Subsite GetLinkToVisit()
        {
            Subsite subsite;
            if (isDFS)
            {

                linksQueue.TryDequeue(out subsite);
            }
            else
            {
                linksStack.TryPop(out subsite);
            }
            if (subsite != null)
            {
                subsite.status = Subsite.SubsiteStatus.INPROGRESS;
                visitedLinksDictionary.TryAdd(visitedLinksDictionary.Count, subsite);
            }
            return subsite;
        }
        public void AddLinkToVisit(Subsite subsite)
        {
            if (isDFS)
            {
                linksQueue.Enqueue(subsite);
            }
            else
            {
                linksStack.Push(subsite);
            }
        }
        private async void StartSearching()
        {

            Task[] taskList = new Task[numberOfThreads];

            for (int i = 0; i < numberOfThreads; i++)
            {
                taskList[i] = Task.Factory.StartNew(async () => { await ProcessSearching(new CancellationTokenSource(options.workTime)); }).Unwrap();
            }
            await Task.WhenAll(taskList);
            while (true)
            {
                var subsite = GetLinkToVisit();
                if (subsite == null)
                {
                    linksListView.Items.Clear();
                    addLinksToListView(core,"");
                    return;
                }
                subsite.status = Subsite.SubsiteStatus.CANCELLED;
                visitedLinksDictionary.TryAdd(visitedLinksDictionary.Count, subsite);
            }
        }
        public void addLinksToListView(Subsite subsite,string a)
        {
            linksListView.Items.Add(a+subsite.url);
            if (subsite.childList.Count != 0)
            {
                foreach (var child in subsite.childList)
                {
                    addLinksToListView(child,a+'\t');
                }
            }

        }
        private void SaveToCSV(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = ".csv (.csv)|.csv|.* (All Files)|.";
            Stream stream;
            if (saveFileDialog.ShowDialog() == true)
            {
                using (stream = saveFileDialog.OpenFile())
                {
                    using (StreamWriter sw = new StreamWriter(stream, Encoding.UTF8))
                    {

                        foreach (var site in visitedLinksDictionary.Select(x => x.Value).GroupBy(x => x.url).Select(x => x.FirstOrDefault()))
                        {

                            var stringBody = Regex.Replace((site?.body ?? "EMPTY"), @"\s+", " ").ToCharArray().Select(x => x == ';' ? ':' : x) ?? " ";
                            sw.WriteLine(string.Format("{0};{1}", site?.url.Replace(';', ':'), new string(stringBody.ToArray())));
                        }
                    }
                    stream.Close();
                }

            }

        }

    }
}
