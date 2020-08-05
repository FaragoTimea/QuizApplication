namespace QuizApplication.DTO
{
    public class AnswerOptionDTO
    {
        public int Id { get; set; }
        public string AnswerString { get; set; }

        public bool IsCorrect { get; set; }
    }
}