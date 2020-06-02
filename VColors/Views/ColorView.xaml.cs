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

namespace VColors.Views
{
    /// <summary>
    /// Interaction logic for ColorView.xaml
    /// </summary>
    public partial class ColorView : Window
    {
        public ColorView()
        {
            var vm = new ColorViewModel();

            DataContext = vm;
            InitializeComponent();
        }
    }
}
