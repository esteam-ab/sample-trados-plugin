using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleTradosPlugin
{
    public partial class SampleTradosPluginControl : UserControl
    {
        public SampleTradosPluginControl()
        {
            InitializeComponent();

            var viewModel = new ViewModel.SampleTradosPluginViewModel();
            var infoBoxControl = new View.SampleTradosPluginView
            {
                DataContext = viewModel
            };

            infoBoxControl.InitializeComponent();
            elementHost.Child = infoBoxControl;
        }
    }
}
