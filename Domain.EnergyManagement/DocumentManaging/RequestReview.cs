using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Domain.EnergyManagement.DocumentManaging
{
    public class RequestReview:Entity
    {
        public long RequestId {get;private set;}
        public Manager Manager{get;private set;}
        public DateTimeOffset ReviewDateTime {get;private set;}
        public bool IsApproved {get;private set;}
        public string Commentary {get;private set;}

        private RequestReview(Manager manager, long requestId, DateTimeOffset reviewDateTime, bool isApproved, string commentary)
        {
            Manager = manager;
            RequestId = requestId;
            ReviewDateTime = reviewDateTime;
            IsApproved = isApproved;
            Commentary = commentary;
        }
        
        private RequestReview(){}


    }
}