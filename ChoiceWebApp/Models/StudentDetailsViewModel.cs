using System.Collections.Generic;

namespace ChoiceWebApp.Models
{
    public class StudentDetailsViewModel
    {
        public Student Student { get; set; }
        public List<Discipline> SelectedDisc { get; set; }
    }
}