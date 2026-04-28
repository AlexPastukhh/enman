using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error.Errors.Passport;

namespace Domain.EnergyManagement.DocumentManaging
{
public class PassportData : ValueObject
{
    /// <summary>
    /// Данные паспорта — серия, номер, кем и когда выдан.
    /// </summary>
    /// <remarks>
    /// Процесс проверки и нормализации паспортных данных:
    /// ![Passport flow](../docs/images/passport_flow.svg)
    /// </remarks>
    public string Series { get; }
    public string Number { get; }
    public string IssuedBy { get; }
    public string Code { get; }
    public DateOnly? IssuedAtTime { get; }

    private PassportData(
        string series,
        string number, 
        string issuedBy,
        string code,
        DateOnly issuedAtTime)
    {
        Series = series;
        Number = number;
        IssuedBy = issuedBy;
        Code = code;
        IssuedAtTime = issuedAtTime;
    }
    private PassportData(){}

    public static Result<PassportData, IReadOnlyList<Error>> Create(
        string series,
        string number,
        string issuedBy,
        string code,
        string issuedAtTime)
    {
        var errors = new List<Error>();

        var validateSeries=ValidateSeries(series);
        if(validateSeries.IsFailure)
        {
            errors=errors.Concat(validateSeries.Error).ToList();
        }
        
        var validateNumber=ValidateNumber(number);
        if(validateNumber.IsFailure)
        {
            errors=errors.Concat(validateNumber.Error).ToList();
        }
        
        var validateCode=ValidateCode(code);
        if(validateCode.IsFailure)
        {
            errors=errors.Concat(validateCode.Error).ToList();
        }
        
        var validateIssuedBy=ValidateIssuedBy(issuedBy);
        if(validateIssuedBy.IsFailure)
        {
            errors=errors.Concat(validateIssuedBy.Error).ToList();
        }
        
        var validateTime=ValidateIssuedAtTime(issuedAtTime);
        if(validateTime.IsFailure)
        {
            errors=errors.Concat(validateTime.Error).ToList();
        }
        
        if (errors.Count > 0)
        {
            return Result.Failure<PassportData, IReadOnlyList<Error>>(errors);
        }

        string normalizedSeries = NormalizeSeries(series);
        string normalizedNumber = NormalizeNumber(number);
        string normalizedCode = NormalizeCode(code);

        var parsedTime = validateTime.Value;

        return Result.Success<PassportData, IReadOnlyList<Error>>(
            new PassportData(normalizedSeries, normalizedNumber, issuedBy, normalizedCode, parsedTime));
    }

    private static UnitResult<IReadOnlyList<Error>> ValidateSeries(string series)
    {   
        var errors = new List<Error>();
        
        if (string.IsNullOrWhiteSpace(series))
        {
            errors.Add(SeriesIsRequired);
        }

        string seriesDigits = Regex.Replace(series, @"\s", "");
        if (!Regex.IsMatch(seriesDigits, @"^\d{4}$"))
        {
            errors.Add(SeriesIsInvalid);
        }
        
        if(errors.Any())
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(errors);
        }
        
        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    private static UnitResult<IReadOnlyList<Error>> ValidateNumber(string number)
    {
        var errors = new List<Error>();
        if (string.IsNullOrWhiteSpace(number))
        {
            errors.Add(NumberIsRequired);
           
        }

        string numberDigits = Regex.Replace(number, @"\s", "");
        if (!Regex.IsMatch(numberDigits, @"^\d{6}$"))
        {
            errors.Add(NumberIsInvalid);
        }
        
        if(errors.Any())
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(errors);
        }
        
        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    private static UnitResult<IReadOnlyList<Error>> ValidateCode(string code)
    {
        var errors=new List<Error>();
        if (string.IsNullOrWhiteSpace(code))
        {
            errors.Add(CodeIsRequired);
        }

        string codeDigits = Regex.Replace(code, @"\s|-", "");
        if (!Regex.IsMatch(codeDigits, @"^\d{6}$"))
        {
            errors.Add(CodeIsInvalid);
        }
        
        if(errors.Any())
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(errors);
        }
        
        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    private static UnitResult<IReadOnlyList<Error>> ValidateIssuedBy(string issuedBy)
    {
        var errors=new List<Error>();
        if (string.IsNullOrWhiteSpace(issuedBy))
        {
            errors.Add(IssuedByIsRequired);
        }
        
        if(errors.Any())
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(errors);
        }
        
        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    private static Result<DateOnly, IReadOnlyList<Error>> ValidateIssuedAtTime(string issuedAtTime)
    {
        var errors=new List<Error>();
        if (string.IsNullOrWhiteSpace(issuedAtTime))
        {
            errors.Add(IssuedAtTimeIsRequired);
        }
    
        if (!DateOnly.TryParse(issuedAtTime, out DateOnly time))
        {
            errors.Add(IssuedAtTimeIsInvalid);
        }
        
        if(time > DateOnly.FromDateTime(DateTime.Now))
        {
            errors.Add(IssuedAtTimeIsInTheFuture);
        }

        if(errors.Any())
        {
            return Result.Failure<DateOnly,IReadOnlyList<Error>>(errors);
        }
        
        return Result.Success<DateOnly,IReadOnlyList<Error>>(time);
    }


    private static string NormalizeSeries(string series)
    {
        return series.Replace(" ","");
    }

    private static string NormalizeNumber(string number)
    {
        return number.Replace(" ","");
    }

    private static string NormalizeCode(string code)
    {
        var codeNoSpaces = code.Replace(" ","");
        return codeNoSpaces.Replace("-", "");
    }
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Series;
        yield return Number;
        yield return IssuedBy;
        yield return Code;
        yield return IssuedAtTime;
    }
}
}