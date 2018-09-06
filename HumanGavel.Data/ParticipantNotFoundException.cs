using System;
using System.Runtime.Serialization;

namespace HumanGavel.Data.Entities
{
    [Serializable]
    public  class ParticipantNotFoundException : Exception
    {
        private Guid caseId;
        private Guid participantId;

        public ParticipantNotFoundException()
        {
        }

        public ParticipantNotFoundException(string message) : base(message)
        {
        }

        public ParticipantNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ParticipantNotFoundException(Guid participantId, Guid caseId)
        {
            this.participantId = participantId;
            this.caseId = caseId;
        }

        protected ParticipantNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}