using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Notepad__
{
    /// <summary>
    /// Interaction logic for Replace.xaml
    /// </summary>
    public partial class Replace : Window
    {
        private bool all;
        private TextFile file;
        private ObservableCollection<TextFile> files;
        public Replace()
        {
            InitializeComponent();
        }

        public Replace(ObservableCollection<TextFile> TextFiles, TextFile selectedFile, bool replaceAll)
        {
            InitializeComponent();

            all = replaceAll;
            file = selectedFile;
            files = TextFiles;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ReplaceBtn_Click(object sender, RoutedEventArgs e)
        {
            if((bool)CurrentFileOnly.IsChecked)
            {
                if(all)
                {
                    file.Text = file.Text.Replace(FindText.Text, ReplaceText.Text);
                }
                else
                {
                    file.Text = ReplaceFirstOccurrence(file.Text, FindText.Text, ReplaceText.Text);
                }
            }
            else
            {
                for (int i = 0; i < files.Count; i++)
                {
                    if (all)
                    {
                        files[i].Text = files[i].Text.Replace(FindText.Text, ReplaceText.Text);
                    }
                    else
                    {
                        files[i].Text = ReplaceFirstOccurrence(files[i].Text, FindText.Text, ReplaceText.Text);
                    }
                }
            }
        }

        public static string ReplaceFirstOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.IndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }
    }
}
