using System;
using System.Collections.Generic;
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
using WpfAppFinanse.Themes;

namespace WpfAppFinanse.Views
{
    /// <summary>
    /// Interaction logic for DataEntryWindow.xaml
    /// </summary>
    public partial class DataEntryWindow : Window
    {
        public DataEntryWindow()
        {
            InitializeComponent();
            UpdateTheme((Application.Current as App).IsLightTheme);
        }
        private void UpdateTheme(bool isLightTheme)
        {
            ThemeManager.ApplyTheme(isLightTheme ? "Light" : "Dark");
        }
    }
}


