namespace QuizApplication.DTO
{
    public class ResultDTO
    {
        public int QuizId { get; set; }
        public int TotalOptions { get; set; }
        public int CorrectOptions { get; set; }
        public double Percent { get; set; }
    }
}
