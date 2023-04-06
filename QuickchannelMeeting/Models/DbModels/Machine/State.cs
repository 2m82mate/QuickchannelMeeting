using System.ComponentModel.DataAnnotations;

namespace QuickchannelMeeting.Models.DbModels
{
    public class State
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Transition> Transitions { get; set; }
    }
}
