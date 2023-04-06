using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickchannelMeeting.Models.DbModels
{
    public class Transition
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("FromState")]
        public int FromStateId { get; set; }

        public State FromState { get; set; }

        [ForeignKey("ToState")]
        public int ToStateId { get; set; }

        public State ToState { get; set; }

        [ForeignKey("Trigger")]
        public int TriggerId { get; set; }

        public Trigger Trigger { get; set; }

        public IEnumerable<TransitionCondition> TransitionConditions { get; set; }
        public IEnumerable<TransitionOperation> TransitionOperations { get; set; }
    }
}
