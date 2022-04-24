using System;
using System.Collections.Generic;

namespace QASite.Data
{
    public class Question
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public List<QuestionsTags> QuestionTags { get; set; } = new List<QuestionsTags>();
        public List<Like> Likes { get; set; } = new List<Like>();
        public int UserID { get; set; }
        public User User { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}
