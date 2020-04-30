using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Xml.Serialization;

namespace QuestinAnswerSystem
{
    class QASystemDriver
    {
        LanguageInstructor LI = new LanguageInstructor();
        Collection<QARanker> colQARank = new Collection<QARanker>();
        public int[] answer = { 100, 100, 100, 100, 100 };
        public void MatchWordsInSentences(ReadFromFile inputData)
        {
            // Get words from sentences
            SentenceFormat sf = new SentenceFormat();
            sf.SplitSentence(inputData.paragraph, clsConstant.StringSplitPattern.sentenceForm);

            foreach (var sentence in sf.sentences)
            {
                Tokenizer TSentence = new Tokenizer();
                GetTokens(TSentence, sentence);
                MatchQuestionAndAnswer(inputData, TSentence);
            }

            DisplayResult d = new DisplayResult();
            d.ShowResult(colQARank, answer, inputData.answer);
        }

        private void MatchQuestionAndAnswer(ReadFromFile inputData, Tokenizer STokens)
        {
            int Q_num = 0;
            foreach (var ques in inputData.questions)
            {
                // Get words from Questions
                Q_num++;
                Tokenizer Qtokens = new Tokenizer();
                GetTokens(Qtokens, ques);

                int Q_Count = CountHits(STokens, Qtokens);

                if (Q_Count > 0)
                {
                    // Split to respective Answers
                    Tokenizer AnswerList = new Tokenizer();
                    AnswerList.SplitSentence(inputData.answer, clsConstant.StringSplitPattern.wordsSeparatedBySemicolon);

                    int A_num = 0;
                    foreach (var ans in AnswerList.Tokens)
                    {
                        A_num++;
                        Tokenizer AnsTokens = new Tokenizer();
                        GetTokens(AnsTokens, ans);

                        int A_Count = CountHits(STokens, AnsTokens);

                        if (A_Count > 0)
                        {
                            CreateOrUpdateQARank(Q_num, A_num, Q_Count, A_Count, Qtokens.Tokens.Count, AnsTokens.Tokens.Count);
                        }
                    }
                }
            }
        }

        // Create or Update Question-Answer pair for rank calculation
        private void CreateOrUpdateQARank(int question, int answer, int QCount, int ACount, int totalQTokens, int totalATokens)
        {
            QARanker lPair = colQARank.Where(l => l.questionId == question && l.answerID == answer).FirstOrDefault();

            if (lPair != null)
            {
                lPair.UpdateRank(QCount, ACount, totalQTokens, totalATokens);
                //UpdateAnswerOrder(lPair);
            }
            else
            {
                QARanker QAR = new QARanker(question, answer, QCount, ACount, totalQTokens, totalATokens);
                //UpdateAnswerOrder(QAR);

                colQARank.Add(QAR);
            }
        }

        //private void UpdateAnswerOrder(QARanker curQA)
        //{
        //    if(colQARank == null || colQARank.Count == 0)
        //    {
        //        answer[curQA.questionId - 1] = curQA.answerID - 1;
        //    }
        //    else if(colQARank.Count > 0)
        //    {
        //        var QA = colQARank.Where(l => l.answerID == curQA.answerID).OrderByDescending(l => l.Rank).FirstOrDefault();

        //        if(QA == null)
        //        {
        //            if (answer[curQA.questionId - 1] == 100)
        //                answer[curQA.questionId - 1] = curQA.answerID - 1;
        //            else
        //            {
        //                var Ques = colQARank.Where(l => l.questionId == curQA.questionId).OrderByDescending(l => l.Rank).FirstOrDefault();

        //                if(Ques.Rank < curQA.Rank)
        //                {
        //                    answer[curQA.questionId - 1] = curQA.answerID - 1;
        //                }

        //            }
        //        }

        //        else if(curQA.Rank >= QA.Rank)
        //        {
        //            answer[curQA.questionId - 1] = curQA.answerID - 1;
        //        }
        //        else
        //        {
                    
        //        }
        //    }    
        //}
        private bool AddSubstringCheck(string s1, List<string> Question)
        {

            foreach (string s in Question)
            {
                if (s1.Equals(s.Substring(0, s.Length - 1), StringComparison.InvariantCultureIgnoreCase))
                    return true;

                if (s.Equals(s1.Substring(0, s1.Length - 1), StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }

            return false;
        }

        private void GetTokens(Tokenizer T, string line)
        {
            T.SplitSentence(line, clsConstant.StringSplitPattern.individualWords);

            // Ignore words provided by the Language Dictionary.
            T.Tokens = T.Tokens.Except(LI.IstrWordsIgnore).ToList();
        }

        // Get no of matching words
        private int CountHits(Tokenizer Line1, Tokenizer Line2)
        {
            int Count = Line1.Tokens.Distinct().Intersect(Line2.Tokens).Count();

            foreach (string s in Line1.Tokens.Distinct())
            {
                if (AddSubstringCheck(s, Line2.Tokens))
                    Count++;
            }

            return Count;
        }
    }
}
