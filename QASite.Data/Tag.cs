using System.Collections.Generic;

namespace QASite.Data
{
    public class Tag
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<QuestionsTags> QuestionsTags { get; set; } = new List<QuestionsTags>();
    }
}
