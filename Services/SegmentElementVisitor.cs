using Sdl.LanguagePlatform.Core;
using Sdl.LanguagePlatform.Core.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTradosPlugin.Services
{
    public class SegmentElementVisitor : ISegmentElementVisitor
    {
        public void VisitDateTimeToken(DateTimeToken token)
        {
            throw new NotImplementedException();
        }

        public void VisitMeasureToken(MeasureToken token)
        {
            throw new NotImplementedException();
        }

        public void VisitNumberToken(NumberToken token)
        {
            throw new NotImplementedException();
        }

        public void VisitSimpleToken(SimpleToken token)
        {
            throw new NotImplementedException();
        }

        public void VisitTag(Tag tag)
        {
            Console.WriteLine("tag");
        }

        public void VisitTagToken(TagToken token)
        {
            Console.WriteLine("tagToken");
        }

        public void VisitText(Text text)
        {
            Console.WriteLine("text");
        }
    }
}
