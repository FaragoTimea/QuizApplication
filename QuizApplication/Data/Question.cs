using System.Collections.Generic;

namespace QuizApplication.Data
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionString { get; set; }
        public ICollection<AnswerOption> AnswerOptions { get; set; }
        public QuestionType QuestionType { get; set; }

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}
