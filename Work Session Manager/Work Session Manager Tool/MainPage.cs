using System.IO;
using System.Windows.Forms;

namespace Work_Session_Manager_Tool
{
    public partial class MainPage : Form
    {
        private const string directoryPath = @"C:\WorkSessionLogs";

        public MainPage()
        {
            CheckDirectory();

            InitializeComponent();
        }

        /// <summary>
        /// Create a directory if it does not already exist
        /// </summary>
        private void CheckDirectory()
        {
            Directory.CreateDirectory(directoryPath);
        }


    }
}
