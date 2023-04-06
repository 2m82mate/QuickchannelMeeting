using System.ComponentModel.DataAnnotations;

namespace QuickchannelMeeting.Models.DbModels
{
    public class Trigger
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
