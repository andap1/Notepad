using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Notepad__
{
    public class TextFile
    {
        public string Text { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public bool NewFile { get; set; }
        public bool IsSaved { get; set; }
        public Brush TextColor { get; set; }
        
    }
}
