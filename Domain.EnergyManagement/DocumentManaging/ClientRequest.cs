using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Domain.EnergyManagement.DocumentManaging
{
    public class ClientRequest:Entity
    {
        public DateTimeOffset RequestDateTime { get; }
        public RequestType Type { get; }
        public string RequestDetails { get; }
        private List<RequestReview>_reviews = new List<RequestReview>();
        public virtual IReadOnlyList<RequestReview> Reviews =>_reviews.AsReadOnly();
        protected ClientRequest(string requestDetails, DateTimeOffset requestDate, RequestType type)
        {
            RequestDetails = requestDetails;
            RequestDateTime = requestDate;
            Type = type;
        }
        protected ClientRequest()
        {
        }
    }
}