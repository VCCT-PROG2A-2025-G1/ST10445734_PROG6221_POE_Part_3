using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10445734_PROG6221_POE_Part_3.Models
{
    public class QuizResult
    {
        public int TotlQuestions { get; set; }
        public int CorrectAnswers { get; set; }

        public string Feedback 
        {
            get
            {
                double score = (double) CorrectAnswers / TotlQuestions * 100;
                if (score >= 80.0 )
                    return "Excellent work! You have a strong understanding of cybersecurity concepts.";
                else if (score >= 50.0)
                    return "Good job! You have a solid understanding of cybersecurity concepts, but there's room for improvement.";
                else
                    return "Unfortunately, you did not pass. Please review the material and try again.";
            }
        }
    }
}
