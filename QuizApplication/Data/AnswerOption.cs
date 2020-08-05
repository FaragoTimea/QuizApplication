namespace QuizApplication.Data
{
    public class AnswerOption
    {
        public int Id { get; set; }
        public string AnswerString { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
