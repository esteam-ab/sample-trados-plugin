using SampleTradosPlugin.View;
using SampleTradosPlugin.ViewModel;
using Sdl.Desktop.IntegrationApi;
using Sdl.Desktop.IntegrationApi.Extensions;
using Sdl.Desktop.IntegrationApi.Interfaces;
using Sdl.TranslationStudioAutomation.IntegrationApi;

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
        private SampleTradosPluginView _view;
        private SampleTradosPluginViewModel _viewModel;
        private EditorController _editorController;

        protected override IUIControl GetContentControl()
        {
            return _view;
        }
        protected override void Initialize()
        {
            _editorController = SdlTradosStudio.Application.GetController<EditorController>();
            
            _view = new SampleTradosPluginView();
            _viewModel = new SampleTradosPluginViewModel(_view, _editorController);

            _view.DataContext = _viewModel;
        }
    }
}
