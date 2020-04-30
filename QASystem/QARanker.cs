using System;
using System.Collections.Generic;
using System.Text;

namespace QuestinAnswerSystem
{
    class QARanker
    {
        public QARanker(int i, int j, int k, int l, int x, int y)
        {
            questionId = i;
            answerID = j;
            questionCount = k;
            answerCount = l;
            TotalQCount = x;
            TotalACount = y;
        }

        public int questionId { get; set; }
        public int answerID { get; set; }
        public int questionCount { get;  set; }
        public int answerCount { get; set; }
        public int TotalQCount { get; set; }
        public int TotalACount { get; set; }
        public int Rank => CalculateRank();

        public int CalculateRank()
        {
            // we need some efficient algorithm here.
            int rank = (questionCount * 100 / TotalQCount) + (answerCount * 100 / TotalACount);
            return rank;
        }

        public int CalculateRank(int x, int y, int T1, int T2)
        {
            // we need some efficient algorithm here.
            int rank =  (x * 100 / T1) + (y * 100 / T2);
            return rank;
        }

        // Check and Update Rank if needed.
        public void UpdateRank(int QCount, int ACount, int totalQTokens, int totalATokens)
        {
            if(CalculateRank(QCount, ACount, totalQTokens, totalATokens) > Rank)
            {
                this.questionCount = QCount;
                this.answerCount = ACount;
            }
        }

        public void UpdateAnswerOrder()
        {

        }

    }
}
