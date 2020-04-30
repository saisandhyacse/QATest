using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuestinAnswerSystem
{
    // Class to split words in required format
    internal class Tokenizer : LineFormatter
    {
        public List<string> Tokens { get; set; }
       
        public override void SplitSentence(string sentence, clsConstant.StringSplitPattern splitType)
        {
            switch (splitType)
            {
                case clsConstant.StringSplitPattern.individualWords:
                    var w = sentence.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' },
                                                         StringSplitOptions.RemoveEmptyEntries);
                    if (w != null)
                    {
                        Tokens = w.ToList();
                    }
                    break;
                case clsConstant.StringSplitPattern.wordsSeparatedBySemicolon:
                    var ws = sentence.Split(new char[] { ';' },
                                                        StringSplitOptions.RemoveEmptyEntries);

                    if (ws != null)
                    {
                        Tokens = ws.ToList();
                    }
                    break;

                default:
                    break;
            }

        }

    }
}
