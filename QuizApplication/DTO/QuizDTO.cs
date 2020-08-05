using System;
using System.Collections.Generic;

namespace QuizApplication.DTO
{
    public class QuizDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<QuestionDTO> Questions { get; set; }
    }
}
