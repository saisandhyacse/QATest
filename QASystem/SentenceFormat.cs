using System;
using System.Collections.Generic;
using System.Text;

namespace QuestinAnswerSystem
{
    internal class SentenceFormat : LineFormatter
    {
        public string[] sentences { get; set; }

        public override void SplitSentence(string text, clsConstant.StringSplitPattern splitType)
        {
            sentences = text.Split(new char[] { '.', '?', '!' });
        }
    }
}
