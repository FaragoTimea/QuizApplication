using System.Collections.Generic;

namespace QuizApplication.Data
{
    public class Quiz
    {
        public int Id { get; set; }
        public ICollection<Question> Questions { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
