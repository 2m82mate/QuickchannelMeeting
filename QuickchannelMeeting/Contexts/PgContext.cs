using Microsoft.EntityFrameworkCore;
using QuickchannelMeeting.Enums;
using QuickchannelMeeting.Models.DbModels;

namespace QuickchannelMeeting.Contexts
{
    public class PgContext : DbContext
    {
        //Machine
        public DbSet<State> States { get; set; }
        public DbSet<Trigger> Triggers { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Transition> Transitions { get; set; }
        public DbSet<TransitionCondition> TransitionConditions { get; set; }
        public DbSet<TransitionOperation> TransitionOperations { get; set; }

        //Meeting
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<AgendaPoint> AgendaPoints { get; set; }
        public DbSet<Voting> Votings { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set up many-to-many relationships
            modelBuilder.Entity<Transition>()
                .HasOne(t => t.FromState)
                .WithMany(s => s.Transitions)
                .HasForeignKey(t => t.FromStateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transition>()
                .HasOne(t => t.ToState)
                .WithMany()
                .HasForeignKey(t => t.ToStateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransitionCondition>()
                .HasKey(tc => new { tc.TransitionId, tc.ConditionId });

            modelBuilder.Entity<TransitionCondition>()
                .HasOne(tc => tc.Transition)
                .WithMany(t => t.TransitionConditions)
                .HasForeignKey(tc => tc.TransitionId);

            modelBuilder.Entity<TransitionCondition>()
                .HasOne(tc => tc.Condition)
                .WithMany(c => c.TransitionConditions)
                .HasForeignKey(tc => tc.ConditionId);

            modelBuilder.Entity<TransitionOperation>()
                .HasKey(ta => new { ta.TransitionId, ta.OperationId });

            modelBuilder.Entity<TransitionOperation>()
                .HasOne(ta => ta.Transition)
                .WithMany(t => t.TransitionOperations)
                .HasForeignKey(ta => ta.TransitionId);

            modelBuilder.Entity<TransitionOperation>()
                .HasOne(ta => ta.Operation)
                .WithMany(a => a.TransitionActions)
                .HasForeignKey(ta => ta.OperationId);

            // Seed data for MeetingState and MeetingTrigger enums
            modelBuilder.Entity<State>().HasData(
                Enum.GetValues(typeof(MeetingState))
                    .Cast<MeetingState>()
                    .Select(state => new State { Id = (int)state, Name = state.ToString() })
     );

            modelBuilder.Entity<Trigger>().HasData(
                Enum.GetValues(typeof(MeetingTrigger))
                    .Cast<MeetingTrigger>()
                    .Select(trigger => new Trigger { Id = (int)trigger, Name = trigger.ToString() })
            );
            modelBuilder.Entity<AgendaPoint>()
                .HasOne(ap => ap.Meeting)
                .WithMany(m => m.AgendaPoints)
                .HasForeignKey(ap => ap.MeetingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AgendaPoint>()
                .HasIndex(ap => new { ap.MeetingId, ap.Position })
                .IsUnique()
                .HasDatabaseName("IX_AgendaPoints_MeetingId_Position");

            // Seed data for conditions, actions, and transitions
            modelBuilder.Entity<Condition>().HasData(
                new Condition { Id = (int)MeetingCondition.Test, Name = "Test" }
            );

            modelBuilder.Entity<Operation>().HasData(
                new Operation { Id = (int)MeetingOperation.StartMeeting, Name = "Start meeting" }
            );

            modelBuilder.Entity<Transition>().HasData(
                new Transition
                {
                    Id = 1,
                    FromStateId = (int)MeetingState.Preperation,
                    ToStateId = (int)MeetingState.Idle,
                    TriggerId = (int)MeetingTrigger.StartMeeting
                }
            );

            modelBuilder.Entity<TransitionCondition>().HasData(
                new TransitionCondition
                {
                    TransitionId = 1,
                    ConditionId = 1
                }
            );

            modelBuilder.Entity<TransitionOperation>().HasData(
                new TransitionOperation
                {
                    TransitionId = 1,
                    OperationId = 1
                }
            );

            var meetingId = new Guid("4d336f24-e6de-4d6f-8932-0c6d7bce4993"); 

            // Seed data for agenda points
            var agendaPointId1 = Guid.NewGuid();
            var agendaPointId2 = Guid.NewGuid();

            modelBuilder.Entity<Meeting>().HasData(
               new Meeting { Id = meetingId, Title = "City Council Meeting", Description = "Regular city council meeting to discuss various topics.", StateId = (int)MeetingState.Preperation}
           );
            modelBuilder.Entity<AgendaPoint>().HasData(
                new AgendaPoint { Id = agendaPointId1, Title = "Agenda Point 1", Description = "Discuss the budget for the upcoming year.", MeetingId = meetingId, Position = 1 },
                new AgendaPoint { Id = agendaPointId2, Title = "Agenda Point 2", Description = "Review the city's infrastructure projects.", MeetingId = meetingId, Position = 2 }
            );

           


            // Seed data for participants
            var participantId1 = Guid.NewGuid();
            var participantId2 = Guid.NewGuid();

            modelBuilder.Entity<Participant>().HasData(
                new Participant { Id = participantId1, Name = "John Doe", Role = "Council Member", MeetingId = meetingId },
                new Participant { Id = participantId2, Name = "Jane Smith", Role = "Council Member", MeetingId = meetingId }
            );

            // Seed data for votings
            var votingId1 = Guid.NewGuid();
            var votingId2 = Guid.NewGuid();

            modelBuilder.Entity<Voting>().HasData(
                new Voting { Id = votingId1, Title = "Approve city budget", AgendaPointId = agendaPointId1 },
                new Voting { Id = votingId2, Title = "Vote on infrastructure project A", AgendaPointId = agendaPointId2 }
            );

            // Seed data for vote results
            modelBuilder.Entity<Vote>().HasData(
                new Vote { Id = Guid.NewGuid(), Result = "Yes", ParticipantId = participantId1, VotingId = votingId1 },
                new Vote { Id = Guid.NewGuid(), Result = "No", ParticipantId = participantId2, VotingId = votingId1 },
                new Vote { Id = Guid.NewGuid(), Result = "Yes", ParticipantId = participantId1, VotingId = votingId2 },
                new Vote { Id = Guid.NewGuid(), Result = "Yes", ParticipantId = participantId2, VotingId = votingId2 }
            );
        }
        public PgContext(DbContextOptions<PgContext> opt)
            : base(opt)
        {

        }
    }
}
