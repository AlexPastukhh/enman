// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using EnergyManagement.Server.Data;
// using FluentAssertions;
// using Microsoft.Extensions.Diagnostics.HealthChecks;
// using Tests.EnergyManagement.TestHelpers;
// using Xunit;
// using Xunit.Extensions.Ordering;

// namespace Tests.EnergyManagement.Integration
// {
//     public class ConstantsTests
//     {
//         [Fact]
//         public async Task DoctorReturnsHealthyOnCorrectConstants()
//         {
//             var doctor = new FakeDoctor();
//             var healthCheckResult = await doctor.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);
//             healthCheckResult.Status.Should().Be(HealthStatus.Healthy);
//         }
        
//         [Fact]
//         public async Task DoctorReturnsUnHealthyOnInCorrectConstants()
//         {
//             var init = FakeSharedService.InvalidTestConfigs;
//             var doctor = new FakeDoctor();
//             foreach(var invalidConfig in init)
//             {
//                 var healthCheckResult = await doctor.CheckInvalidHealthAsync(
//                     new HealthCheckContext(),
//                     invalidConfig,
//                     CancellationToken.None);
//                 healthCheckResult.Status.Should().Be(HealthStatus.Unhealthy);
//             }
//         }
//     }
// }