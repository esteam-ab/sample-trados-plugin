using SampleTradosPlugin.View;
using SampleTradosPlugin.ViewModel;
using Sdl.Desktop.IntegrationApi;
using Sdl.Desktop.IntegrationApi.Extensions;
using Sdl.Desktop.IntegrationApi.Interfaces;
using Sdl.TranslationStudioAutomation.IntegrationApi;
using Sdl.TranslationStudioAutomation.IntegrationApi.Extensions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SampleTradosPlugin
{
    [ViewPart(
        Id = "Information Box",
        Name = "Information Box",
        Description = "Information Box",
        Icon = "favicon"
    )]
    [ViewPartLayout(typeof(EditorController), Dock = DockType.Top)]
    public class SampleTradosPluginController : AbstractViewPartController
    {
        // Support for 2021-2022 versions
        private readonly SampleTradosPluginView _control = new SampleTradosPluginView();

        protected override IUIControl GetContentControl()
        {
            return _control;
        }

        protected override void Initialize()
        {
            var dataContext = new SampleTradosPluginViewModel();
            _control.DataContext = dataContext;
        }

        // Support for 2017-2019 versions
        //private readonly InformationBoxControl _control = new InformationBoxControl();

        //protected override Control GetContentControl()
        //{
        //    return _control;
        //}

        //protected override void Initialize()
        //{
        //}
    }
}
