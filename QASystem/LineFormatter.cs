using System;
using System.Collections.Generic;
using System.Text;

namespace QuestinAnswerSystem
{
    abstract class LineFormatter
    {
        public abstract void SplitSentence(string text, clsConstant.StringSplitPattern splitType);
    }
}
