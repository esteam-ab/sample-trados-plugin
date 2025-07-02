using Sdl.Desktop.IntegrationApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SampleTradosPlugin.View
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    /// Support for 2021-2022 versions
    public partial class SampleTradosPluginView : UserControl, IUIControl
    /// Support for 2017-2019 versions
    //public partial class InformationBoxView : UserControl
    {
        public SampleTradosPluginView()
        {
            InitializeComponent();
        }

        public void Dispose()
        {
        }
    }
}
