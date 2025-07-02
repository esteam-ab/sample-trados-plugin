using Sdl.Desktop.IntegrationApi;
using Sdl.Desktop.IntegrationApi.Extensions;
using Sdl.Desktop.IntegrationApi.Interfaces;
// Support for 2024 version
//using Sdl.Desktop.IntegrationApi.Notifications.Events;
using Sdl.TranslationStudioAutomation.IntegrationApi;
using System;

namespace SampleTradosPlugin
{
    [ApplicationInitializer]
    public class SampleTradosPluginInitializer : IApplicationInitializer
    {
        // Support for 2017-2022 versions  
        //public void Execute()  
        //{  
        //    EditorController = SdlTradosStudio.Application.GetController<EditorController>();  
        //}  

        // Support for 2024 version  
        //public void Execute()  
        //{  
        //    SdlTradosStudio.Application.GetService<IStudioEventAggregator>()  
        //        .GetEvent<StudioWindowCreatedNotificationEvent>().Subscribe(OnStudioWindowCreated);  
        //}  

        //private void OnStudioWindowCreated(StudioWindowCreatedNotificationEvent obj)  
        //{  
        //    EditorController = SdlTradosStudio.Application.GetController<EditorController>();  
        //}  
        private static EditorController _editorController;

        public static EditorController EditorController
        {
            get
            {
                if (_editorController == null)
                {
                    _editorController = SdlTradosStudio.Application.GetController<EditorController>();
                }
                return _editorController;
            }
        }

        public void Execute()
        {

        }
    }
}
