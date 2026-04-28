using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace Tests.EnergyManagement.TestHelpers
{
    public class ExceptionInfo
    {
        public string? ExceptionType { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public List<ExceptionInfo> InnerExceptions { get; set; } = new();
        public bool hasPrintedStackTrace { get; set; } = false;

        

        public string ToConsoleString()
        {
            var sb = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(ExceptionType))
                sb.AppendLine($"Type: {ExceptionType}");
            if (!string.IsNullOrEmpty(Message))
                sb.AppendLine($"Message: {Message}");
            if (!hasPrintedStackTrace && !string.IsNullOrEmpty(StackTrace))
            {
                sb.AppendLine($"StackTrace: {StackTrace}");
            }

            if (InnerExceptions.Count > 0)
            {
                sb.AppendLine("InnerExceptions:");
                for (int i = 0; i < InnerExceptions.Count; i++)
                {
                    sb.AppendLine($"  [{i}] {InnerExceptions[i].ToConsoleString()}");
                }
            }

            return sb.ToString();
        }
        

        
    }

    public static class IntegrationTestHelper
    {

        public static async Task<ProblemDetails> GetProblemDetailsAsync(HttpResponseMessage response)
        {
            var pb = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            return pb;
            
        }
        private static IReadOnlyList<TExtType>GetCollectionFromProblemsExtension<TExtType>(
            ProblemDetails problemDetails,
            string extensionName)
        {
            // Get the raw JSON for the "errors" extension
            if (problemDetails!.Extensions
                .TryGetValue(extensionName, out var extensionObject) 
                && extensionObject is JsonElement jsonElement)
            {
                // Use PropertyNamingPolicy = null to preserve PascalCase
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = null, // This tells it NOT to use camelCase,
                    PropertyNameCaseInsensitive = true // Still allow case-insensitive matching
                };
                
                var errors = jsonElement.Deserialize<IEnumerable<TExtType>>(options);
                return errors!.ToList();
            }
            
            throw new InvalidOperationException($"Extension '{extensionName}' not found in ProblemDetails.");
            
        }
        
        
        public static IReadOnlyList<ServerValidationError>GetValidationErrors(ProblemDetails? problemDetails)
        {
            return IntegrationTestHelper
                .GetCollectionFromProblemsExtension<ServerValidationError>
                    (problemDetails!,
                    SharedConst.GeneralConstants.ErrorsCollectionName);
        }
        public static IReadOnlyList<Claim>GetClaimsForIndividual(IndividualClient individual)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,individual.Id.ToString()),
                new Claim(ClaimTypes.Email,individual.Email.Value)
            };
            return claims;
        }

        /// <summary>
        /// Attempts to extract an exception message from arbitrary JSON content.
        /// This does not attempt to deserialize into <see cref="System.Exception"/> (which is not
        /// reliably deserializable). Instead it searches for common property names
        /// <summary>
        /// Extracts Message and StackTrace from JSON using standard .NET exception property names.
        /// Returns a formatted string with "Message: ..." and "StackTrace: ..." on separate lines.
        /// </summary>
        // public static string? GetExceptionMessageFromJson(string json)
        // {
        //     if (string.IsNullOrWhiteSpace(json)) return null;

        //     try
        //     {
        //         using var doc = JsonDocument.Parse(json);
        //         var root = doc.RootElement;

        //         // Extract using standard .NET exception property names
        //         string? message = GetStringProperty(root, SharedConstants.GeneralConstants.MessagePropertyName);
        //         string? stackTrace = GetStringProperty(root, SharedConstants.ExceptionExtensionConstants.StackTracePropertyName);

        //         if (message == null && stackTrace == null) return null;

        //         // Flatten stack trace to single line
        //         if (!string.IsNullOrEmpty(stackTrace))
        //         {
        //             stackTrace = stackTrace.Replace("\r\n", " → ")
        //                                   .Replace("\n", " → ")
        //                                   .Replace("\r", " → ");
        //         }

        //         var sb = new System.Text.StringBuilder();
        //         if (!string.IsNullOrEmpty(message))
        //             sb.AppendLine($"Message: {message}");
        //         if (!string.IsNullOrEmpty(stackTrace))
        //             sb.AppendLine($"StackTrace: {stackTrace}");

        //         return sb.ToString();
        //     }
        //     catch (JsonException)
        //     {
                
        //         throw;
        //     }
        // }

        private static string? GetStringProperty(JsonElement el, string propertyName)
        {
            if (el.ValueKind != JsonValueKind.Object) 
                throw new InvalidOperationException("JSON element is not an object.");

            var prop =el.GetProperty(propertyName);
            
            if(prop.ValueKind != JsonValueKind.String)
            {
                throw new InvalidOperationException($"Property '{propertyName}' is not a string.");
            }

            return prop.GetString();
            
        }

        private static bool TryGetStringProperty(JsonElement el, string propertyName, out string? value)
        {
            value = null;
            if (el.ValueKind != JsonValueKind.Object) return false;
            if (el.TryGetProperty(propertyName, out var prop) 
                && prop.ValueKind == JsonValueKind.String)
            {
                value = prop.GetString();
                return true;
            }

            // Case-insensitive search
            foreach (var p in el.EnumerateObject())
            {
                if (string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase) && p.Value.ValueKind == JsonValueKind.String)
                {
                    value = p.Value.GetString();
                    return true;
                }
            }

            return false;
        }

        

      
       

        private static ExceptionInfo? ExtractExceptionInfo(JsonElement el)
        {
            if (el.ValueKind != JsonValueKind.Object) return null;

            var info = new ExceptionInfo();

            // Look for type/ExceptionType
            var type = GetStringPropertyFromEl(el, "type") ?? GetStringPropertyFromEl(el, "ExceptionType") ?? GetStringPropertyFromEl(el, "exceptionType");
            if (type != null) info.ExceptionType = type;

            // Look for message/Message
            var msg = GetStringPropertyFromEl(el, "message") ?? GetStringPropertyFromEl(el, "Message") ?? GetStringPropertyFromEl(el, "detail");
            if (msg != null) info.Message = msg;

            // Look for stackTrace/StackTrace
            var st = GetStringPropertyFromEl(el, "stackTrace") ?? GetStringPropertyFromEl(el, "StackTrace");
            if (st != null) info.StackTrace = st;

            // Look for innerException(s)
            if (el.TryGetProperty("innerException", out var innerProp) && innerProp.ValueKind == JsonValueKind.Object)
            {
                var inner = ExtractExceptionInfo(innerProp);
                if (inner != null) info.InnerExceptions.Add(inner);
            }
            else if (el.TryGetProperty("innerExceptions", out var innersProp) && innersProp.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in innersProp.EnumerateArray())
                {
                    var inner = ExtractExceptionInfo(item);
                    if (inner != null) info.InnerExceptions.Add(inner);
                }
            }

            // Return the info if we found at least one field
            if (!string.IsNullOrEmpty(info.ExceptionType) || !string.IsNullOrEmpty(info.Message) || !string.IsNullOrEmpty(info.StackTrace) || info.InnerExceptions.Count > 0)
                return info;

            return null;
        }

        private static string? GetStringPropertyFromEl(JsonElement el, string propertyName)
        {
            if (el.ValueKind != JsonValueKind.Object) return null;

            if (el.TryGetProperty(propertyName, out var prop) && prop.ValueKind == JsonValueKind.String)
                return prop.GetString();

            // Case-insensitive search
            foreach (var p in el.EnumerateObject())
            {
                if (string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase) && p.Value.ValueKind == JsonValueKind.String)
                    return p.Value.GetString();
            }

            return null;
        }
    }
}