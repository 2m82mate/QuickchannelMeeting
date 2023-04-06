using System.ComponentModel.DataAnnotations;

namespace QuickchannelMeeting.Models.DbModels
{
    public class Condition
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public IEnumerable<TransitionCondition> TransitionConditions { get; set; }
    }
}
