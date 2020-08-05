using QuizApplication.Data;
using QuizApplication.DTO;
using System.Collections.Generic;

namespace QuizApplication.Interfaces
{
    public interface IQuizChecker
    {
        public ResultDTO CheckQuiz(List<Question> questions, GuessDTO guess);
    }
}
