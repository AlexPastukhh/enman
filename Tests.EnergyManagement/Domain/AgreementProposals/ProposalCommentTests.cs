using Domain.EnergyManagement;
using FluentAssertions;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.Domain.AgreementProposals;

public class ProposalCommentTests
{
    [Fact]
    public void Create_succeeds_and_trims_value()
    {
        var result = ProposalComment.Create(" Please review. ");

        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be("Please review.");
    }

    [Fact]
    public void Create_fails_for_blank_value()
    {
        var result = ProposalComment.Create(" ");

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.ProposalCommentIsRequired);
    }

    [Fact]
    public void Create_fails_when_value_is_too_long()
    {
        var result = ProposalComment.Create(new string('a', ProposalComment.MaxLength + 1));

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.ProposalCommentIsTooLong);
    }
}
