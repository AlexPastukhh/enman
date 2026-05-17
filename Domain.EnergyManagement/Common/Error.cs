using CSharpFunctionalExtensions;

namespace Domain.EnergyManagement.Common;

public class Error : ValueObject
{
    public string Code { get; }
    public int StatusCode { get; }
    
    private Error(
        string code,
        int statusCode)
    {
        Code = code;
        StatusCode = statusCode;
    }
    
    private static Error Create(
        string code,
        int statusCode)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Error code cannot be null or empty.", nameof(code));
        }

        if (statusCode < 400 || statusCode > 599)
        {
            throw new ArgumentException(
                "Status code must be a valid HTTP error status code (400-599).",
                nameof(statusCode));
        }

        return new Error(code, statusCode);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Code;
        yield return StatusCode;
    }

    public static class Errors
    {
        public static class Account
        {
            public static readonly Error FirstNameIsRequired = Create("account.firstName.is.required", 400);
            public static readonly Error FirstNameIsTooLarge = Create("account.firstName.is.too.large", 400);
            public static readonly Error MiddleNameIsRequired = Create("account.middleName.is.required", 400);
            public static readonly Error MiddleNameIsTooLarge = Create("account.middleName.is.too.large", 400);
            public static readonly Error LastNameIsRequired = Create("account.lastName.is.required", 400);
            public static readonly Error LastNameIsTooLarge = Create("account.lastName.is.too.large", 400);
            public static readonly Error EmailIsRegisteredAlready = Create("account.email.is.registered.already", 400);
            public static readonly Error EmailWasntRegistered = Create("account.email.wasnt.registered", 400);
            public static readonly Error EmailIsRequired = Create("account.email.is.required", 400);
            public static readonly Error EmailIsInvalid = Create("account.email.value.is.invalid", 400);
            public static readonly Error PhoneNumberIsRequired = Create("account.phoneNumber.is.required", 400);
            public static readonly Error PhoneNumberIsInvalid = Create("account.phoneNumber.is.invalid", 400);
            public static readonly Error SNILSIsRequired = Create("snils.is.required", 400);
            public static readonly Error SNILSIsInvalid = Create("snils.is.invalid", 400);
            public static readonly Error PasswordIsRequired = Create("account.password.is.required", 400);
            public static readonly Error PasswordConfirmationIsRequired = Create("account.passwordConfirmation.is.required", 400);
            public static readonly Error PasswordIsTooShort = Create("account.password.is.too.short", 400);
            public static readonly Error PasswordIsTooLong = Create("account.password.is.too.long", 400);
            public static readonly Error PasswordLacksSpecialCharacters = Create("account.password.lacks.special.characters", 400);
            public static readonly Error PasswordIsWrong = Create("account.password.is.wrong", 400);

            public static readonly Error PasswordConfirmationDoesntMatch = Create("account.passwordConfirmation.doesnt.match", 400);
        }
        
        public static class ConstantsErrors
        {
            public static Error ConstantNotFound(string path) => Create($"constants.{path}.not.found", 500);
            public static readonly Error RootNotFound = Create("constants.root.not.found", 500);
            

        }
        
        public static class ClientErrors 
        {
            public static readonly Error ClientNotFound = Create("client.not.found", 400);

        }
        
        public static class AddressErrors
        {
        public static readonly Error AddressIsRequired = Create("address.is.required", 400);
        public static readonly Error PostalCodeIsRequired = Create("address.postalCode.is.required", 400);
        public static readonly Error PostalCodeIsInvalid = Create("address.postalCode.is.invalid", 400);
        
        public static readonly Error RegionIsRequired = Create("address.region.is.required", 400);
        public static readonly Error RegionIsTooLong = Create("address.region.is.too.long", 400);
        public static readonly Error CityIsRequired = Create("address.city.is.required", 400);
        public static readonly Error CityIsTooLong = Create("address.city.is.too.long", 400);
        public static readonly Error StreetIsRequired = Create("address.street.is.required", 400);
        public static readonly Error StreetIsTooLong = Create("address.street.is.too.long", 400);
        public static readonly Error HouseIsRequired = Create("address.house.is.required", 400);
        public static readonly Error HouseIsTooLong = Create("address.house.is.too.long", 400);
        public static readonly Error BuildingIsTooLong = Create("address.building.is.too.long", 400);
        public static readonly Error ApartmentIsTooLong = Create("address.apartment.is.too.long", 400);
        }

        

        public static class Passport
        {
            public static readonly Error SeriesIsRequired = Create("passport.series.is.required", 400);
            public static readonly Error SeriesIsInvalid = Create("passport.series.is.invalid", 400);
            public static readonly Error NumberIsRequired = Create("passport.number.is.required", 400);
            public static readonly Error NumberIsInvalid = Create("passport.number.is.invalid", 400);
            public static readonly Error CodeIsRequired = Create("passport.code.is.required", 400);
            public static readonly Error CodeIsInvalid = Create("passport.code.is.invalid", 400);
            public static readonly Error IssuedByIsRequired = Create("passport.issuedBy.is.required", 400);
            public static readonly Error IssuedAtTimeIsRequired = Create("passport.issuedAtTime.is.required", 400);
            public static readonly Error IssuedAtTimeIsInvalid = Create("passport.issuedAtTime.is.invalid", 400);
            public static readonly Error IssuedAtTimeIsInTheFuture = Create("passport.issuedAtTime.is.in.the.past", 400);
            public static readonly Error IssuedAtLocationIsRequired = Create("passport.issuedAtLocation.is.required", 400);
            
        }

        public static class General
        {
            public static readonly Error ExceptionBeingThrown = Create("exception.being.thrown", 500);
            public static readonly Error ValueIsRequired = Create("value.is.required", 400);
            public static readonly Error ValueIsInvalid = Create("value.is.invalid", 400);
            public static readonly Error StringIsTooSmall = Create("string.is.too.small", 400);
            public static readonly Error StringIsTooLarge = Create("string.is.too.large", 400);
            public static readonly Error NotFound = Create("record.not.found", 404);
            public static readonly Error InternalServerError = Create("internal.server.error", 500);
            public static readonly Error RequestBodyIsNull = Create("request.body.is.null", 400);
            public static readonly Error DateTimeIsInFuture = Create("date.time.is.in.future", 500);
        }
        public static class ClientRequestErrors
        {
            public static readonly Error ClientRequestTextIsRequired = Create("client.request.text.is.required", 400);
            public static readonly Error ClientRequestTextIsTooLong = Create("client.request.text.is.too.long", 400);

        }

        public static class L1Domain
        {
            public static readonly Error AccountNotActivated = Create("l1.account.not.activated", 400);
            public static readonly Error ClientAccountIsRequired = Create("l1.client.account.is.required", 400);
            public static readonly Error ApplicantPartyIsIncomplete = Create("l1.applicant.party.is.incomplete", 400);
            public static readonly Error ApplicantPartyIsRequired = Create("l1.applicant.party.is.required", 400);
            public static readonly Error ApplicantPartyMustBePersisted = Create("l1.applicant.party.must.be.persisted", 400);
            public static readonly Error RequestObjectAddressIsRequired = Create("l1.request.object.address.is.required", 400);
            public static readonly Error ReviewerIsRequired = Create("l1.request.reviewer.is.required", 400);
            public static readonly Error OnlyInReviewRequestCanBeApproved = Create("l1.request.only.in.review.can.be.approved", 400);
            public static readonly Error OnlyInReviewRequestCanBeRejected = Create("l1.request.only.in.review.can.be.rejected", 400);
            public static readonly Error RejectionFeedbackIsTooLong = Create("l1.request.rejection.feedback.is.too.long", 400);

            public static readonly Error EmployeeIsRequired = Create("l1.employee.is.required", 400);
            public static readonly Error EmployeeAccountIsRequired = Create("l1.employee.account.is.required", 400);
            public static readonly Error EmployeeFullNameIsRequired = Create("l1.employee.full.name.is.required", 400);
            public static readonly Error EmployeeIsNotActive = Create("l1.employee.is.not.active", 400);

            public static readonly Error OnlyInReviewRequestCanStartReview = Create("l1.request.only.in.review.can.start.review", 400);
            public static readonly Error RequestReviewAlreadyStarted = Create("l1.request.review.already.started", 400);
            public static readonly Error RequestReviewMustBeStarted = Create("l1.request.review.must.be.started", 400);
            public static readonly Error OnlyStartedReviewCanBeCompleted = Create("l1.request.review.only.started.can.be.completed", 400);
            public static readonly Error RequestReviewStartedByAnotherEmployee = Create("l1.request.review.started.by.another.employee", 400);

            public static readonly Error AgreementProposalExchangeIsRequired = Create("l1.agreement.proposal.exchange.is.required", 400);
            public static readonly Error OnlyApprovedRequestCanBeMarkedAgreementExchangeFailed = Create("l1.request.only.approved.can.be.marked.agreement.exchange.failed", 400);
            public static readonly Error RequestIsRequired = Create("l1.request.is.required", 400);
            public static readonly Error AgreementExchangeRequiresApprovedRequest = Create("l1.agreement.exchange.requires.approved.request", 400);
            public static readonly Error AgreementDocumentIsRequired = Create("l1.agreement.document.is.required", 400);
            public static readonly Error OnlyAwaitingClientConfirmationCanBeAccepted = Create("l1.agreement.exchange.only.awaiting.client.confirmation.can.be.accepted", 400);
            public static readonly Error ClientVersionCanBeSentOnlyWhenAwaitingClientConfirmation = Create("l1.agreement.exchange.client.version.only.awaiting.client.confirmation", 400);
            public static readonly Error ClientCanRespondOnlyToEmployeeProposal = Create("l1.agreement.exchange.client.can.respond.only.to.employee.proposal", 400);
            public static readonly Error EmployeeCanRespondOnlyWhenAwaitingEmployeeResponse = Create("l1.agreement.exchange.employee.can.respond.only.awaiting.employee.response", 400);
            public static readonly Error EmployeeCanSupersedeOnlyClientProposal = Create("l1.agreement.exchange.employee.can.supersede.only.client.proposal", 400);
            public static readonly Error AcceptedAgreementExchangeCannotBeRefused = Create("l1.agreement.exchange.accepted.cannot.be.refused", 400);
            public static readonly Error AgreementExchangeAlreadyFinallyRefused = Create("l1.agreement.exchange.already.finally.refused", 400);
            public static readonly Error AgreementExchangeCannotBeFinallyRefusedNow = Create("l1.agreement.exchange.cannot.be.finally.refused.now", 400);

            public static readonly Error OnlyEmployeeProposalCanBeAccepted = Create("l1.agreement.proposal.only.employee.can.be.accepted", 400);
            public static readonly Error OnlyAwaitingClientConfirmationProposalCanBeAccepted = Create("l1.agreement.proposal.only.awaiting.client.confirmation.can.be.accepted", 400);
            public static readonly Error AcceptedProposalCannotBeSuperseded = Create("l1.agreement.proposal.accepted.cannot.be.superseded", 400);
            public static readonly Error ProposalAlreadySuperseded = Create("l1.agreement.proposal.already.superseded", 400);

            public static readonly Error AgreementDocumentStorageKeyIsRequired = Create("l1.agreement.document.storage.key.is.required", 400);
            public static readonly Error AgreementDocumentFileNameIsRequired = Create("l1.agreement.document.file.name.is.required", 400);
            public static readonly Error AgreementDocumentContentTypeIsRequired = Create("l1.agreement.document.content.type.is.required", 400);
            public static readonly Error AgreementDocumentSizeIsRequired = Create("l1.agreement.document.size.is.required", 400);

            public static readonly Error ProposalCommentIsRequired = Create("l1.agreement.proposal.comment.is.required", 400);
            public static readonly Error ProposalCommentIsTooLong = Create("l1.agreement.proposal.comment.is.too.long", 400);
            public static readonly Error FinalRefusalReasonIsRequired = Create("l1.agreement.final.refusal.reason.is.required", 400);
            public static readonly Error FinalRefusalReasonIsTooLong = Create("l1.agreement.final.refusal.reason.is.too.long", 400);
        }
    }
}


