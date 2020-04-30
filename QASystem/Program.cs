using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace QuestinAnswerSystem
{
    class Program
    {
        public static void Main()
        {
            // 
            // Sample Input files (Input1, Input2.. Input5).
            for (int inputCount = clsConstant.ONE; inputCount <= clsConstant.FIVE; inputCount++)
            {
                ReadFromFile inputData = new ReadFromFile();
                inputData.ReadFile(clsConstant.INPUT+ inputCount);

                if (!inputData.isValidInput)
                {
                    Console.WriteLine("Input Data not in proper format");
                }
                else
                {
                    QASystemDriver QASinstance = new QASystemDriver();
                    QASinstance.MatchWordsInSentences(inputData);
                }
            }

            // Keep the console window open in debug mode.  
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }


    }

}
