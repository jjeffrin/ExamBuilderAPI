using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamBuilderAPI.Models
{
    public class Exam
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
    }
}
