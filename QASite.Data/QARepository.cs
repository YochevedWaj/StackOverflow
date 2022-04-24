using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QASite.Data
{
    public class QARepository
    {
        private string _connectionString;
        public QARepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User GetByEmail(string email)
        {
            using var context = new QADataContext(_connectionString);
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public void AddQuestion(Question question, List<string> tags)
        {
            using var context = new QADataContext(_connectionString);
            context.Questions.Add(question);
            context.SaveChanges();
            foreach (string tag in tags)
            {
                var t = GetTag(tag);
                var tagID = t == null ? AddTag(tag) : t.ID;        
                context.QuestionsTags.Add(new QuestionsTags
                {
                    QuestionID = question.ID,
                    TagID = tagID
                });
            }
            context.SaveChanges();

        }

        private Tag GetTag(string name)
        {
            using var context= new QADataContext(_connectionString);
            return context.Tags.FirstOrDefault(t => t.Name == name);
        }

        private int AddTag(string name)
        {
            using var context = new QADataContext(_connectionString);
            var tag = new Tag { Name = name };
            context.Tags.Add(tag);
            context.SaveChanges();
            return tag.ID;
        }
        public int GetUserID(string email)
        {
            using var context = new QADataContext(_connectionString);
            return context.Users.Where(u => u.Email == email).Select(u => u.ID).First();
        }

        public Question GetQuestion(int id)
        {
            using var context = new QADataContext(_connectionString);
            return context.Questions.Include(q => q.User).Include(q => q.Likes).ThenInclude(l => l.User)
                .Include(q => q.Answers).ThenInclude(a => a.User)
                .Include(q => q.QuestionTags).ThenInclude(qt => qt.Tag)
                .FirstOrDefault(q => q.ID == id);
        }

        public int GetLikes(int id)
        {
            using var context = new QADataContext(_connectionString);
            return context.Questions.Include(q => q.Likes).Where(q => q.ID == id).Select(q => q.Likes.Count).FirstOrDefault();
        }

        public void LikeQuestion(int userID, int quesiontID)
        {
            using var context = new QADataContext(_connectionString);
            var like = new Like { UserID = userID, QuestionID = quesiontID };
            context.Likes.Add(like);
            context.SaveChanges();
        }
        public List<Question> GetQuestions()
        {
            using var context = new QADataContext(_connectionString);
            return context.Questions.Include(q => q.User).Include(q => q.Likes)
                .Include(q => q.Answers).Include(q => q.QuestionTags).ThenInclude(qt => qt.Tag).ToList();
        }

        public void AddAnswer(Answer answer)
        {
            using var context = new QADataContext(_connectionString);
            context.Answers.Add(answer);
            context.SaveChanges();
        }

    }
}
