using DiffPlex.DiffBuilder;
using DiffPlex.Chunkers;
using SampleTradosPlugin.Model;
using SampleTradosPlugin.Services;
using Sdl.FileTypeSupport.Framework.BilingualApi;
using Sdl.FileTypeSupport.Framework.NativeApi;
using Sdl.TranslationStudioAutomation.IntegrationApi;
using System;
using System.Collections.Generic;
using System.Linq;
using DiffPlex.DiffBuilder.Model;
using Sdl.DesktopEditor.EditorApi;
using System.Windows.Controls;
using System.Windows.Forms;
using Newtonsoft.Json;
using SampleTradosPlugin.Controls;
using SampleTradosPlugin.View;
using Sdl.ProjectAutomation.Core;

namespace SampleTradosPlugin.ViewModel
{
	public class SampleTradosPluginViewModel : ModelBase, IDisposable
	{
		private readonly EditorController _editorController;
		private readonly SegmentVisitor _segmentVisitor;
		private readonly SampleTradosPluginView _view;
		// Support for 2021-2022 versions
		private IStudioDocument _activeDocument;
		// Support for 2017-2019 versions
		//private Document _activeDocument;
		private List<IComment> _comments;
		private SampleTradosPluginModel _sampleTradosPluginModel;
		private List<TabItem> _activeTabs;

		public SampleTradosPluginViewModel(SampleTradosPluginView view, EditorController editorController)
		{
			_view = view;
			_editorController = editorController;
			_segmentVisitor = new SegmentVisitor(true);
			_comments = new List<IComment>();
			_sampleTradosPluginModel = new SampleTradosPluginModel();
            _activeTabs = new List<TabItem> { new SampleTabItem() };

			_editorController.ActiveDocumentChanged += EditorController_ActiveDocumentChanged;

			SetActiveDocument(_editorController.ActiveDocument);
        }

		public IOrderedEnumerable<IComment> Comments
		{
			get
			{
				_comments = _comments ?? new List<IComment>();
				return _comments.OrderByDescending(a => (int)a.Severity).ThenByDescending(a => a.Date);
			}
			set
			{
				_comments = value?.ToList();
				OnPropertyChanged(nameof(Comments));
			}
		}

		public SampleTradosPluginModel InformationBoxModel
		{
			get
			{
				if (_sampleTradosPluginModel == null)
				{
					_sampleTradosPluginModel = new SampleTradosPluginModel();
				}

				return _sampleTradosPluginModel;
			}
			set
			{
				_sampleTradosPluginModel = value;
				OnPropertyChanged(nameof(InformationBoxModel));
			}
		}

		public List<TabItem> ActiveTabs
		{
			get
			{
				_activeTabs = _activeTabs ?? new List<TabItem>();
				return _activeTabs;
			}
			set
			{
				_activeTabs = value?.ToList();
				OnPropertyChanged(nameof(ActiveTabs));
			}
		}

        public bool HasComments => Comments.Any();

		public object SelectedItem { get; set; }

		public void Dispose()
		{
			if (_editorController != null)
			{
				_editorController.ActiveDocumentChanged -= EditorController_ActiveDocumentChanged;
			}

			if (_activeDocument != null)
			{
				_activeDocument.ActiveSegmentChanged -= ActiveDocument_ActiveSegmentChanged;
				_activeDocument.ContentChanged -= ActiveDocument_ContentChanged;
			}
		}

		private void ActiveDocument_ContentChanged(object sender, DocumentContentEventArgs e)
		{
			if (_activeDocument == null)
			{
				// ContentChanged level active document is null
				return;
			}

			// _activeDocument.ActiveFile -> Null Reference when split AL segment (e.g. sample xliff segment 22)
			// Quick solution to release v.1.0
			try
			{
				var file = GetActiveFile(_activeDocument);
				if (file == null)
				{
					// string.Format("ContentChanged level active document has no active file but has {0} files part of it.", ActiveDocument.Files.Count())
					return;
				}
				// refresh the highlighting/text-difference of the target segment of the Translation Results tab
				UpdateTranslationResultsTargetText(e);
			}
            catch (Exception)
            {
            }
		}		

		private void ActiveDocument_ActiveSegmentChanged(object sender, EventArgs e)
		{
			UpdateComments();
			UpdateDocumentStructureInformation();
		}

		private void EditorController_ActiveDocumentChanged(object sender, DocumentEventArgs e)
		{
			SetActiveDocument(e.Document);
		}

		private void AddComments(ISegment segment)
		{
			_segmentVisitor.VisitSegment(segment);
			foreach (var comment in _segmentVisitor.Comments)
			{
				_comments.Add(comment);
			}
		}

		private void AddComments(ISegmentPair segmentPair)
		{
			var paragraphInfo = segmentPair?.GetParagraphUnitProperties();
			if (paragraphInfo?.Comments?.Comments == null)
			{
				return;
			}

			foreach (var comment in paragraphInfo.Comments.Comments)
			{
				_comments.Add(comment);
			}
		}

		// Support for 2021-2022 versions
		    private void SetActiveDocument(IStudioDocument document)
		// Support for 2017-2019 versions
		//private void SetActiveDocument(Document document)
        {
            if (_activeDocument != null)
            {
                _activeDocument.ActiveSegmentChanged -= ActiveDocument_ActiveSegmentChanged;
            }

            _activeDocument = document;

            if (_activeDocument != null && GetActiveFile(_activeDocument) != null)
            {
                _activeDocument.ActiveSegmentChanged += ActiveDocument_ActiveSegmentChanged;
                _activeDocument.ContentChanged += ActiveDocument_ContentChanged;

                UpdateDocumentStructureInformation();
            }
        }

        private void UpdateComments()
		{
			_comments.Clear();

			var segmentPair = _activeDocument?.ActiveSegmentPair;
			if (segmentPair == null)
			{
				OnPropertyChanged(nameof(Comments));
				OnPropertyChanged(nameof(HasComments));
				return;
			}

			AddComments(segmentPair);
			AddComments(segmentPair.Source);
			AddComments(segmentPair.Target);

			OnPropertyChanged(nameof(Comments));
			OnPropertyChanged(nameof(HasComments));
		}

		public event EventHandler<SegmentPropertiesChangedEventArgs> SegmentPropertiesChanged;

		protected virtual void OnSegmentPropertiesChanged(SegmentPropertiesChangedEventArgs e)
		{
			EventHandler<SegmentPropertiesChangedEventArgs> handler = SegmentPropertiesChanged;
			handler?.Invoke(this, e);
		}

		private void UpdateTranslationResultsTargetText(DocumentContentEventArgs eventArgs)
        {
			if (_sampleTradosPluginModel != null)
            {
				// Some logic

                OnPropertyChanged(nameof(InformationBoxModel));
			}			
		}

		/// Returns the plain text representation of the contents of a segments. By default this ignores any tags
		/// </summary>
		/// <param name="segment"></param>
		/// <returns></returns>
		private string GetPlainText(ISegment segment)
        {
			_segmentVisitor.VisitSegment(segment);
			return _segmentVisitor.Text;
		}


        private void UpdateDocumentStructureInformation()
		{
			// Main logic of plugin

			OnPropertyChanged(nameof(InformationBoxModel));
		}

        private ProjectFile GetActiveFile(IStudioDocument document)
        {
            try
            {
                if (document?.ActiveFile != null)
                {
                    return document.ActiveFile;
                }

                var file = document?.Files?.FirstOrDefault();
                if (file != null)
                {
                    return file;
                }
            }
            catch
            {
                throw new Exception("Document files object is null");
            }

            return null;
        }

	}
}
