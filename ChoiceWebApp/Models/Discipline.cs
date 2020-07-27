using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChoiceWebApp.Models
{
    public class Discipline
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Annotation { set; get; }

        [Display(Name = "Teacher")]
        public int TeacherId { set; get; }

        public Teacher Teacher { set; get; }
        public List<StudDisc> StudDiscs { set; get; }
    }
}