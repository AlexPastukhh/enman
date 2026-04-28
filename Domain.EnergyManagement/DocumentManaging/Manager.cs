using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;

namespace Domain.EnergyManagement.DocumentManaging
{
    public class Manager:Entity
    {
        public Password Password {get;private set;}
        public Email Email {get;private set;}
        private List<RequestReview>_requestReviews = new List<RequestReview>();
        public IReadOnlyList<RequestReview> RequestReviews =>_requestReviews.AsReadOnly();
         
        
        private Manager(Password password,Email email)
        {
            Password = password;
            Email = email;
        }
        private Manager(){}
        
        public static Result<Manager,IReadOnlyList<Error>>Create(Password password)
        {
            Guard.IsNotNull(password);
            
            var email = Email.Create(Data.CompanyEmail).Value;
            
            return Result.Success<Manager,IReadOnlyList<Error>>(new Manager(password,email));
        }
    }
}