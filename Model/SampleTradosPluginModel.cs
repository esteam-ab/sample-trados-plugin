using DiffPlex.DiffBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTradosPlugin.Model
{
    public class SampleTradosPluginModel : ModelBase
    {
        private string _originalSourceText;
        private string _originalSourceLanguage;
        private string _sourceText;
        private string _targetText;
        private string _commentText;

        public string OriginalSourceText
        {
            get => _originalSourceText;
            set
            {
                if (_originalSourceText == value)
                {
                    return;
                }
                _originalSourceText = value;
                OnPropertyChanged(nameof(OriginalSourceText));
            }
        }

        public string OriginalSourceLanguage
        {
            get => _originalSourceLanguage;
            set
            {
                if (_originalSourceLanguage == value)
                {
                    return;
                }
                _originalSourceLanguage = value;
                OnPropertyChanged(nameof(OriginalSourceLanguage));
            }
        }

        public string SourceText
        {
            get => _sourceText;
            set
            {
                if (_sourceText == value)
                {
                    return;
                }
                _sourceText = value;
                OnPropertyChanged(nameof(SourceText));
            }
        }

        public string TargetText
        {
            get => _targetText;
            set
            {
                if (_targetText == value)
                {
                    return;
                }
                _targetText = value;
                OnPropertyChanged(nameof(TargetText));
            }
        }

        public string CommentText
        {
            get => _commentText;
            set
            {
                if (_commentText == value)
                {
                    return;
                }
                _commentText = value;
                OnPropertyChanged(nameof(CommentText));
            }
        }
    }
}
