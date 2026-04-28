using Xunit;

[assembly: TestCaseOrderer(
    "Xunit.Extensions.Ordering.TestCaseOrderer", 
    "Xunit.Extensions.Ordering")]
[assembly: TestCollectionOrderer(
    "Xunit.Extensions.Ordering.TestCollectionOrderer", 
    "Xunit.Extensions.Ordering")]