// using System.Reflection;
// using CSharpFunctionalExtensions;
// using Domain.EnergyManagement.Common;
// using EnergyManagement.Server.Data;
// using EnergyManagement.Server.Infrastructure;
// using Microsoft.Extensions.Diagnostics.HealthChecks;
// using static Domain.EnergyManagement.Common.Error.Errors;

// public class ConstantsDoctor :ConstatnsDoctorBase, IHealthCheck
// {
//     private readonly ConstantsConfig _config = SharedFileService.Config;
    
//     public ConstantsDoctor()
//     {

//     }
    
//     public Task<HealthCheckResult> CheckHealthAsync(
//         HealthCheckContext context, 
//         CancellationToken cancellationToken = default)
//     {        
//         try
//         {
            
//             var result =ValidateConstantsHierarchy<ConstantsConfig>(_config );
//             if(result.IsFailure)
//             {
//                 return CreateUnhealthyResult(result.Error);
//             }
            
//             return CreatePerfectHealthResult();
//         }
//         catch (Exception ex)
//         {
//             return Task.FromResult(
//                 HealthCheckResult.Unhealthy($"Exception during constants validation: {ex.Message}"));
            
//         }
//     }
    
//     // ==================== MAIN VALIDATION METHOD ====================
    
    
    
    
//     // ==================== RESULT CREATION METHODS ====================
    
    
    
//     private Task<HealthCheckResult> CreatePerfectHealthResult()
//     {
//         return Task.FromResult(HealthCheckResult.Healthy("✅ All constants loaded correctly!"));
//     }
    
//     private Task<HealthCheckResult> CreateUnhealthyResult(IReadOnlyList<Error> errors)
//     {
//         var errorMessage = string.Join(" | ", errors.Select(e => $"{e.Code}"));
//         return Task.FromResult(HealthCheckResult.Unhealthy(errorMessage));
//     }
    
//     // ==================== HELPER METHODS ====================
   
// }

