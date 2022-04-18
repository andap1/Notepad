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
    /// Interaction logic for Find.xaml
    /// </summary>
    public partial class Find : Window
    {
        private TextFile file;
        private ObservableCollection<TextFile> files;
        public Find()
        {
            InitializeComponent();
        }
        public Find(ObservableCollection<TextFile> TextFiles, TextFile selectedFile)
        {
            InitializeComponent();

            file = selectedFile;
            files = TextFiles;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FindBtn_Click(object sender, RoutedEventArgs e)
        {
            if((bool)CurrentFileOnly.IsChecked)
            {
                int Place = file.Text.IndexOf(FindText.Text);
            }
        }
    }
}
