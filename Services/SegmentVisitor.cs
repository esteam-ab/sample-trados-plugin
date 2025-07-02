using Sdl.FileTypeSupport.Framework.BilingualApi;
using Sdl.FileTypeSupport.Framework.NativeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTradosPlugin.Services
{
    public class SegmentVisitor : IMarkupDataVisitor
    {
        private readonly bool _ignoreTags;

        private Stack<ITagPair> _tagPairStack;

        public SegmentVisitor(bool ignoreTags)
        {
            _ignoreTags = ignoreTags;
            InitializeComponents();
        }

        public List<IComment> Comments { get; set; }
        public bool HasRevisions { get; private set; }
        public bool HasTags { get; private set; }
        public string Text { get; private set; }

        public void VisitCommentMarker(ICommentMarker commentMarker)
        {
            foreach (var commentProperty in commentMarker.Comments.Comments)
            {
                Comments.Add(commentProperty);
            }

            VisitChilderen(commentMarker);
        }

        public void VisitLocationMarker(ILocationMarker location)
        {
            // not used in this implementation
        }

        public void VisitLockedContent(ILockedContent lockedContent)
        {
            if (_ignoreTags)
            {
                return;
            }

            Text += lockedContent.Content.ToString();
        }

        public void VisitOtherMarker(IOtherMarker marker)
        {
            VisitChilderen(marker);
        }

        public void VisitPlaceholderTag(IPlaceholderTag placeholderTag)
        {
            HasTags = true;

            if (_ignoreTags)
            {
                return;
            }

            Text += placeholderTag.Properties.TagContent;
        }

        public void VisitRevisionMarker(IRevisionMarker revisionMarker)
        {
            HasRevisions = true;

            if (revisionMarker.Properties.RevisionType == RevisionType.Insert
                || revisionMarker.Properties.RevisionType == RevisionType.FeedbackAdded)
            {
                VisitChilderen(revisionMarker);
            }
        }

        public void VisitSegment(ISegment segment)
        {
            InitializeComponents();
            VisitChilderen(segment);
        }

        public void VisitTagPair(ITagPair tagPair)
        {
            HasTags = true;

            if (!_ignoreTags)
            {
                Text += tagPair.StartTagProperties.TagContent;
                _tagPairStack.Push(tagPair);
            }

            if (!_ignoreTags)
                VisitChilderen(tagPair);

            if (!_ignoreTags)
            {
                var currentTag = _tagPairStack.Pop();
                Text += currentTag.EndTagProperties.TagContent;
            }
        }

        public void VisitText(IText text)
        {
            Text += text.Properties.Text;
        }

        private void InitializeComponents()
        {
            HasTags = false;
            _tagPairStack = new Stack<ITagPair>();
            Text = string.Empty;
            Comments = new List<IComment>();
        }

        private void VisitChilderen(IAbstractMarkupDataContainer container)
        {
            foreach (var item in container.AllSubItems)
            {
                if (item.GetType().Name != "CommentMarker") 
                {
                    item.AcceptVisitor(this);
                }                
            }
        }
    }
}
