using FluentAssertions;
using Hospital.proj.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.proj.Tests.Unit
{
    public class ErrorCodesTest
    {
        //[Fact]
        //public void Should_Have_Unique_Error_Codes()
        //{
        //    // Use reflection to get all Error objects from the Errors class.
        //    var errorCodes = GetAllErrorCodes();

        //    // Check for duplicates and assert that there are no duplicates.
        //    errorCodes.Distinct().Count().Should().Be(errorCodes.Count(), "because there should be no duplicate error codes.");
        //}

        //private static string[] GetAllErrorCodes()
        //{
        //    // Get all types in the 'Errors' class (Account, User, General, Infrastructure).
        //    var errorTypes = typeof(Errors).GetNestedTypes(BindingFlags.Public | BindingFlags.Static);

        //    // Collect all the 'Error' instances from each nested type.
        //    var errorCodes = errorTypes
        //        .SelectMany(type => type.GetFields(BindingFlags.Public | BindingFlags.Static)
        //                                .Where(field => field.FieldType == typeof(Error))
        //                                .Select(field => field.GetValue(null) as Error))
        //        .Where(error => error != null)
        //        .Select(error => error!.Code)  // Extract the 'Code' property from each Error instance.
        //        .ToArray();

        //    return errorCodes;
        //}
    }
}
