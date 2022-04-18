using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace Notepad__
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int numberOfNewFiles = 0;
   
        public ObservableCollection<TextFile> TextFiles { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            TextFiles = new ObservableCollection<TextFile>();
            Tabs.ItemsSource = TextFiles;
        }

        private void AboutMenu_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void NewTabMenuItem_Click(object sender, RoutedEventArgs e)
        {
            numberOfNewFiles++;

            TextFile textFile = new TextFile();
            textFile.FileName = "File " + numberOfNewFiles.ToString();
            textFile.IsSaved = false;
            textFile.NewFile = true;
            textFile.TextColor = Brushes.HotPink;
            TextFiles.Add(textFile);
            if(TextFiles.Count == 1)
            {
                Tabs.SelectedItem = TextFiles[0];
            }
        }

        private void SaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (Tabs.SelectedIndex >= 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";
                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, FileContent.Text);
                }

                TextFiles[Tabs.SelectedIndex].FileName = System.IO.Path.GetFileName(saveFileDialog.FileName);
                TextFiles[Tabs.SelectedIndex].IsSaved = true;
                TextFiles[Tabs.SelectedIndex].NewFile = false;
                TextFiles[Tabs.SelectedIndex].Text = FileContent.Text;
                TextFiles[Tabs.SelectedIndex].Path = saveFileDialog.FileName;
                TextFiles[Tabs.SelectedIndex].TextColor = Brushes.Black;
                
            }
        }

        private void OpenFie_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFile.ShowDialog() == true)
                FileContent.Text = File.ReadAllText(openFile.FileName);
            
            TextFile textFile = new TextFile();
            textFile.FileName = System.IO.Path.GetFileName(openFile.FileName);
            textFile.TextColor = Brushes.Black;
            TextFiles.Add(textFile);
            
        }

        private void ExitFile_Click(object sender, RoutedEventArgs e)
        {
            if (Tabs.SelectedItem is TextFile file)
            {
                if(!file.IsSaved)
                {
                    MessageBox.Show("Save the file before exit!");
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void Tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Tabs.SelectedItem is TextFile file)
            {
                FileContent.Text = file.Text;
                
            } 
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (Tabs.SelectedItem is TextFile file)
            {
                if(file.NewFile)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        File.WriteAllText(saveFileDialog.FileName, FileContent.Text);
                    }

                    TextFiles[Tabs.SelectedIndex].FileName = System.IO.Path.GetFileName(saveFileDialog.FileName);
                    TextFiles[Tabs.SelectedIndex].IsSaved = true;
                    TextFiles[Tabs.SelectedIndex].NewFile = false;
                    TextFiles[Tabs.SelectedIndex].Text = FileContent.Text;
                    TextFiles[Tabs.SelectedIndex].Path = saveFileDialog.FileName;

                }

                TextFiles[Tabs.SelectedIndex].IsSaved = true;
                TextFiles[Tabs.SelectedIndex].Text = FileContent.Text;
                TextFiles[Tabs.SelectedIndex].TextColor = Brushes.Black;
                File.WriteAllText(TextFiles[Tabs.SelectedIndex].Path, FileContent.Text);
            }
        }

        private void FileContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Tabs.SelectedItem is TextFile file)
            {
                file.IsSaved = false;
                file.TextColor = Brushes.HotPink;
            }
        }

        private void ReplaceMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(Tabs.SelectedItem is TextFile file)
            {
                file.Text = FileContent.Text;
                Replace replace = new Replace(TextFiles, file, false);
                replace.Show();
            }
            
        }

        private void ReplaceAllMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (Tabs.SelectedItem is TextFile file)
            {
                file.Text = FileContent.Text;
                Replace replace = new Replace(TextFiles, file, true);
                replace.Show();
            }
        }

        private void FindMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (Tabs.SelectedItem is TextFile file)
            {
                file.Text = FileContent.Text;
                Find find = new Find(TextFiles, file);
                find.Show();
            }
        }
    }
}
