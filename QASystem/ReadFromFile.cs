using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace QuestinAnswerSystem

{
    class ReadFromFile
    {
        public string paragraph { get; set; }
        public List<string> questions { get; set; }
        public string answer { get; set; }

        private string[] lines { get; set; }

        public bool isValidInput { get; set; }

        enum InputType { paragraph, question, answer }
        public void ReadFile(string FileName)
        {
            try
            {

                string text = System.IO.File.ReadAllText(@"E:\" + FileName + ".txt");


                lines = System.IO.File.ReadAllLines(@"E:\" + FileName + ".txt");

                GetRequiredEntities();

                // Keep the console window open in debug mode.
                Console.WriteLine("Press any key to read next file.");

                System.Console.ReadKey();
                Console.WriteLine("*********Reading from file : " + FileName + " ***********");
            }
            catch(Exception e)
            {
                Console.WriteLine("File not found");
            }
        }

        // Input file segregated to paragraph, questions and answers.
        private void GetRequiredEntities()
        {
            int lineCount = 0;

            if(lines != null || lines.Count() == clsConstant.MAXLINES)
            {
                foreach (string line in lines)
                {
                    if (lineCount == 0)
                    {
                        if (!(ValidateEntity(InputType.paragraph, line)))
                            return;
                        paragraph = line;
                    }

                    else if (lineCount > 0 && lineCount < 6)
                    {
                        if (!(ValidateEntity(InputType.question, line)))
                            return;
                        if(questions == null)
                        {
                            questions = new List<string>();
                        }

                        questions.Add(line);
                    }

                    else if (lineCount == 6)
                    {
                        if (!(ValidateEntity(InputType.answer, line)))
                            return;
                        answer = line;
                    }

                    lineCount++;
                }
            }
            else
            {
                isValidInput = false;
                return;
            }
        }

        // Just added minimum Validation criteria
        private bool ValidateEntity(InputType inputType, string line)
        {
            switch(inputType)
            {
                case InputType.paragraph:
                    // More criteria needed. Just to show the usecase
                    isValidInput = line != null && line.Count() > clsConstant.MINSENTENCESIZE && line.Contains('.');
                    break;

                case InputType.question:
                    isValidInput = line != null && line.Count() > 0 && line.EndsWith('?');
                    break;

                case InputType.answer:
                    isValidInput = line != null && line.Count() > 0 && line.Contains(';');
                    break;

                default:
                    break;
            }

            return true;
        }
    }
}
