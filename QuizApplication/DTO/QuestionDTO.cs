using System.Collections.Generic;

namespace QuizApplication.DTO
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string QuestionString { get; set; }
        public List<AnswerOptionDTO> AnswerOptions { get; set; }
        public string Type { get; set; }
        public int QuizId { get; set; }
    }
}