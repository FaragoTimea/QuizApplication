using QuizApplication.Data;
using QuizApplication.DTO;
using QuizApplication.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace QuizApplication.Services
{
    public class QuizChecker : IQuizChecker
    {
        public ResultDTO CheckQuiz(List<Question> questions, GuessDTO guess)
        {
            ResultDTO result = new ResultDTO()
            {
                TotalOptions = 0,
                CorrectOptions = 0
            };
            foreach (var item in questions)
            {
                int tmp = item.AnswerOptions.Count;
                result.TotalOptions += tmp;
                var g = guess.GuessCorrect.Where(g => g.QuestionId == item.Id).FirstOrDefault();
                if (g != null)
                    foreach (var opt in item.AnswerOptions)
                    {

                        if (opt.IsCorrect)
                        {
                            //option is correct
                            //pts++ when it was guessed
                            if (g.Guesses.Contains(opt.Id)) result.CorrectOptions++;
                        }
                        else
                        {
                            //option is not correct
                            //pts++ when it wasnt guessed
                            if (!g.Guesses.Contains(opt.Id)) result.CorrectOptions++;
                        }
                    }
            }

            result.Percent = (double)result.CorrectOptions / (double)result.TotalOptions *100;
            return result;
        }
    }
}
