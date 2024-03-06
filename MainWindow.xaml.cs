using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace GoldenSound
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mediaElement.Source = new Uri("C:\\Users\\User\\Music\\MUSICsharp");
            mediaElement.Play();
            mediaElement.Volume = 0.7;
        }

        private List<string> listeningHistory = new List<string>();

        List<string> MUSICsharp = new List<string>();
        int currentTrackIndex = 0;

        CommonOpenFileDialog dialoge = new CommonOpenFileDialog();

        
       

        public Uri Source { get; private set; }

        private void audioSlider_valueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Position = new TimeSpan(Convert.ToInt64(audioSlider.Value));

        }
        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
            audioSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.Ticks;
            currentTrackIndex++;
              
            

        }

        private void PAPMU_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialoge = new CommonOpenFileDialog { IsFolderPicker = true };
            var result = dialoge.ShowDialog(); //open okno


            if (result == CommonFileDialogResult.Ok)
            {
                string[] files = Directory.GetFiles(dialoge.FileName);
                foreach (string file in files)
                    if (file.EndsWith(".mp3") || file.EndsWith(".wav") || file.EndsWith(".m4a"))
                    {
                        MUSICsharp.Add(file);

                    }
                

            }
            
        }


        private void GROMKO_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Volume = GROMKO.Value;

        }

        private void IGRAT_Click(object sender, RoutedEventArgs e)
        {


            if (mediaElement.Source == null)
            {
                mediaElement.Play();
            }
            else
            {
                mediaElement.Stop();
            }

            mediaElement.Source = new Uri("C:\\Users\\User\\Music\\MUSICsharp");
            mediaElement.Play();


            // listeningHistory.AddRange();// не понимаю как 

        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            if (MUSICsharp.Count > 0 && currentTrackIndex > 0)
            {
                currentTrackIndex--;
                mediaElement.Source = new Uri(MUSICsharp[currentTrackIndex]);
                mediaElement.Play();
            }
        }

        private void Vpered_Click(object sender, RoutedEventArgs e)
        {
            if (MUSICsharp.Count > 0 && currentTrackIndex < MUSICsharp.Count - 1)
            {
                currentTrackIndex++;
                mediaElement.Source = new Uri(MUSICsharp[currentTrackIndex]);
                mediaElement.Play();
            }
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow historyWindow = new HistoryWindow();

            StringBuilder historyText = new StringBuilder();

            foreach (string track in listeningHistory)
            {
                historyText.AppendLine(track);
            }

            historyWindow.historyTextBlock.Text = historyText.ToString();

            bool? dialogResult = historyWindow.ShowDialog();

            
        }

        private void Sluch_Click(object sender, RoutedEventArgs e)
        {
            if (MUSICsharp.Count > 0)
            { 
                Random random = new Random();
                int randomIndex = random.Next(0, MUSICsharp.Count);

                Uri uri = new Uri(MUSICsharp[randomIndex]);
                mediaElement.Source = uri;


            }
        }
        
        private void POVT_Click(object sender, RoutedEventArgs e)
        {
            if (mediaElement.Position == TimeSpan.Zero)
            {
                mediaElement.Play();
            }
            else
            {
                mediaElement.Position = TimeSpan.Zero;
                mediaElement.Play();
            }
        }

        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = TimeSpan.Zero;
            mediaElement.Play();
        }
    }
    


}



