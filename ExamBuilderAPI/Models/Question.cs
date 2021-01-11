using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamBuilderAPI.Models
{
    public class Question
    {
        public string Id { get; set; }
        public string ExamId { get; set; }
        public string AddedBy { get; set; }
        public string Content { get; set; }
        public string TypeDescription { get; set; }
        public int TypeCode { get; set; }
        public string[] Options { get; set; }
    }
}
