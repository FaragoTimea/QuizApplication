using System.Collections.Generic;

namespace QuizApplication.DTO
{
    public class GuessForQuestionDTO
    {
        public int QuestionId { get; set; }
        public List<int> Guesses { get; set; }
    }
}