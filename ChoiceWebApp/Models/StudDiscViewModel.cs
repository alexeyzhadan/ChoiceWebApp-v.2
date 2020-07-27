using System.Collections.Generic;

namespace ChoiceWebApp.Models
{
    public class StudDiscViewModel
    {
        public Student Student { get; set; }
        public List<Discipline> Selected { get; set; }
        public List<Discipline> NonSelected { get; set; }
    }
}