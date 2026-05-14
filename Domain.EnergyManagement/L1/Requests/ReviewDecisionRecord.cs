namespace Domain.EnergyManagement.L1;

public sealed class ReviewDecisionRecord
{
    public ReviewDecision Decision { get; private set; }
    public EmployeeRef Reviewer { get; private set; }
    public RejectionFeedback? RejectionFeedback { get; private set; }
    public DateTimeOffset DecidedAt { get; private set; }

    private ReviewDecisionRecord(
        ReviewDecision decision,
        EmployeeRef reviewer,
        RejectionFeedback? rejectionFeedback,
        DateTimeOffset decidedAt)
    {
        Decision = decision;
        Reviewer = reviewer;
        RejectionFeedback = rejectionFeedback;
        DecidedAt = decidedAt;
    }

    private ReviewDecisionRecord()
    {
        Reviewer = null!;
    }

    public static ReviewDecisionRecord Approved(
        EmployeeRef reviewer,
        DateTimeOffset decidedAt)
    {
        return new ReviewDecisionRecord(
            ReviewDecision.Approved,
            reviewer,
            null,
            decidedAt);
    }

    public static ReviewDecisionRecord Rejected(
        EmployeeRef reviewer,
        RejectionFeedback? feedback,
        DateTimeOffset decidedAt)
    {
        return new ReviewDecisionRecord(
            ReviewDecision.Rejected,
            reviewer,
            feedback,
            decidedAt);
    }
}
