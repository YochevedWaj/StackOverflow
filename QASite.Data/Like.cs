namespace QASite.Data
{
    public class Like
    {
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public User User { get; set; }
        public Question Question { get; set; }
    }
}
