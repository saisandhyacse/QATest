using System;
using System.Collections.Generic;
using System.Text;

namespace QuestinAnswerSystem
{
    static class clsConstant
    {
        public  const int MINSENTENCESIZE = 3;
        public  const int NO_OF_QUESTION = 5;
        public const int NO_OF_ANSWERS = 5;
        public const int MAXLINES = 7;
        public const int ONE = 1;
        public const int FIVE = 5;
        public const string INPUT = "Input";
        public enum StringSplitPattern { individualWords, wordsSeparatedBySemicolon, sentenceForm };
    }
}
