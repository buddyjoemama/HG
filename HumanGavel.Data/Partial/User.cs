using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanGavel.Data.Entities
{
    public partial class User
    {
        public static void CastVote(Guid userId, Guid caseId, Guid participantId)
        {
            using (HGDataContext context = new Data.HGDataContext())
            {
                DateTime now = DateTime.UtcNow;

                var user = context.Users
                    .Include("Votes")
                    .SingleOrDefault(s => s.UserId == userId);

                // Get the participants in this case.
                var participants = context.Participants
                    .Where(s => s.CaseId == caseId)
                    .Select(s => s.ParticipantId)
                    .OrderBy(s => s)
                    .ToList();

                // Get the second participant.
                Guid participantTwoId = participants.FirstOrDefault(s => s != participantId);

                // Check to make sure the participant exists in the case.
                if (!participants.Contains(participantId))
                {
                    throw new ParticipantNotFoundException(participantId, caseId);
                }
                if (!participants.Contains(participantTwoId))
                {
                    throw new ParticipantNotFoundException(participantTwoId, caseId);
                }

                // Find the most recent vote for this case.
                var mostRecent = user.Votes
                    .OrderBy(s => s.VoteCastDateTimeUTC)
                    .LastOrDefault(s=>s.CaseId == caseId);

                // No votes? Record a +1 for the participant/case.
                if (mostRecent == null)
                {
                    user.Votes.Add(new Entities.Votes
                    {
                        Value = 1,
                        VoteCastDateTimeUTC = now,
                        ParticipantId = participantId,
                        CaseId = caseId
                    });
                }
                else if(mostRecent.ParticipantId != participantId) // Dont register dupes.
                {
                    // Unregister the most recent vote with a -1
                    user.Votes.Add(new Entities.Votes
                    {
                        Value = -1,
                        VoteCastDateTimeUTC = now,
                        ParticipantId = participantTwoId,
                        CaseId = caseId
                    });

                    // Register a vote for the specified participant
                    user.Votes.Add(new Entities.Votes
                    {
                        Value = 1,
                        VoteCastDateTimeUTC = now,
                        ParticipantId = participantId,
                        CaseId = caseId
                    });
                }

                user.ModifiedDateTimeUTC = now.ToString("MM/dd/yyyy hh:mm:ss.ms");

                context.SaveChanges();
            }
        }
    }
}
