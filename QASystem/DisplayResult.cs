using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace QuestinAnswerSystem
{
    class DisplayResult
    {
        public void ShowResult(Collection<QARanker> FinalResult, int[] answ, string answer)
        {
            Tokenizer AnswerList = new Tokenizer();
            AnswerList.SplitSentence(answer, clsConstant.StringSplitPattern.wordsSeparatedBySemicolon);

            List<QARanker> DisplayResult = FinalResult.OrderBy(Q => Q.questionId).ToList();

            if (DisplayResult == null || DisplayResult.Count == 0)
            {
                Console.WriteLine("Question/Answer not in sync");
            }

            foreach (QARanker Q in DisplayResult)
            {
                //Console.WriteLine(Q.questionId + " " + Q.answerID + " : " + Q.Rank);
            }
           
            for(int i = 1; i <= 5; i++)
            {
                var lvar = DisplayResult.Where(l => l.questionId == i);

                if (lvar == null || lvar.Count() == 0)
                {
                    Console.WriteLine("No valid answer for Question " + i);
                }

                var QA = lvar.OrderByDescending(l => l.Rank).FirstOrDefault();

                if (QA != null)
                {
                    Console.WriteLine(AnswerList.Tokens[QA.answerID - 1]);
                }
            }

        }
    }
}
