using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;


namespace аудеоплеер
{
    public partial class MainWindow : Window
    {
        private static List<string> list = new List<string>();
        private List<string> A = new List<string>();
        private int robot = 0;
        private int num = 0;
        private string put = "";
        private int pesna = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void V_Click_1(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dlg = new CommonOpenFileDialog() { IsFolderPicker = true };
            CommonFileDialogResult result = dlg.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                string[] file = Directory.GetFiles(dlg.FileName);
                put = file[0];
                int ind = put.Length - 11;
                put = put.Remove(ind);
                Regex regex = new Regex(@"\w*\.mp3");
                foreach (string item in file)
                {
                    if (regex.IsMatch(item))
                    {
                        ListBox.Items.Add(regex.Match(item).Value);
                        list.Add(item);

                    }
                }
            }
            media.Source = new Uri(list[pesna]);
            media.Play();
            media.Volume = 0.7;
        }

        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
            audioSlider.Maximum = media.NaturalDuration.TimeSpan.Ticks;
        }

        private void audioSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media.Position = new TimeSpan(Convert.ToInt64(audioSlider.Value));
        }

        private void paus_Click(object sender, RoutedEventArgs e)
        {
            media.Pause();
        }

        private void pusk_Click(object sender, RoutedEventArgs e)
        {
            media.Play();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = ListBox.SelectedItem.ToString();
            selected = ListBox.Items[ListBox.SelectedIndex].ToString();
            int index = ListBox.SelectedIndex;
            pesna = index;
            string a = put + selected;
            media.Source = new Uri(a);
            media.Play();
            media.Volume = 0.7;
        }

        private void povtor_Click(object sender, RoutedEventArgs e)
        {
            media.Stop();
            media.Play();
        }

        private void nazat_Click(object sender, RoutedEventArgs e)
        {
            pesna--;
            if (pesna < 0)
            {
                media.Stop();
            }
            else
            {
                if (robot == 0)
                {
                    media.Source = new Uri(list[pesna]); 
                    media.Play();
                    media.Volume = 0.7;
                }   
                if (robot == 1)
                {
                    media.Source = new Uri(list[pesna]);
                    media.Play();
                    media.Volume = 0.7;
                }
            }
            
        }

        private void vper_Click(object sender, RoutedEventArgs e)
        {
            pesna++;
            if (robot == 0)
            {
                media.Source = new Uri(list[pesna]);
                media.Play();
                media.Volume = 0.7;
            }
            if (robot == 1)
            {
                media.Source = new Uri(list[pesna]);
                media.Play();
                media.Volume = 0.7;
            }
        }

        private void peremesh()
        {
            if (robot == 0)
            {
                Random RND = new Random();
                A = list;
                A = A.OrderBy(v => RND.Next()).ToList();
                media.Source = new Uri(A[pesna]);
                media.Play();
                media.Volume = 0.7;
                robot = 1;
            }
            else
            {
                robot = 0;
                vozproiz();
            }


        }
        private void vozproiz()
        {
            media.Source = new Uri(list[pesna]);
            media.Play();
            media.Volume = 0.7;
        }

        private void peremeshka_Click(object sender, RoutedEventArgs e)
        {
            peremesh();
        }
    }
}

