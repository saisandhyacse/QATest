using System;
using System.Collections.Generic;
using System.Text;

namespace QuestinAnswerSystem
{
    // Class To Give language specific instructions. Like a trainer class. Just added minimal functioality here.
    class LanguageInstructor
    {
        public List<string> IlstIgnoreString = new List<string>();

        public string[] IstrWordsIgnore = { "Why", "Which", "Where", "What", "Who", "When", "is", "was", "a", "the", "this", "that", "there", "of",
            "for", "and", "are", "do", "to", "in", "on","an" };

        public char[] singleWord = { '.', '?', '!', ' ', ';', ':', ',' };

        public char[] semicolonSeparatedWords = { ';'};
    }
}
