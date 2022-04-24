using QASite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QASite.Web.Models
{
    public class ViewQuestionViewModel
    {
        public Question Question { get; set; }
        public bool IsAuthenticated { get; set; } 
        public bool LikedQuestion { get; set; }
    }
}
