using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChoiceWebApp.Models
{
    public class Student
    {
        [Key, ForeignKey("User")]
        public string Id { set; get; }
        public string Name { set; get; }
        public string Group { set; get; }

        public List<StudDisc> StudDiscs { set; get; }
        public IdentityUser User { set; get; }
    }
}