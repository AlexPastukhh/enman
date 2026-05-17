using FluentAssertions;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error.Errors.Account;
using System.Collections.Generic;
using Tests.EnergyManagement.Legacy.TestHelpers;
using Tests.EnergyManagement.TestHelpers;
using Domain.EnergyManagement.DocumentManaging;

namespace Tests.EnergyManagement.Legacy.Domain;


public static class IndividualClientNullCases
{
    // Null parameter test cases
    public static readonly object[] NullPassword = { true, false};
    public static readonly object[] NullEmail = { false, true };
    public static readonly object[] AllNull = { true, true};
}





// ============================================
// Main Test Class
// ============================================

public class UserTests
{
    
    public class IndividualCientValuObjects
    {
        public  FullName? FullName {get;private set;}
        public  Email? Email  {get;private set;}
        public  PhoneNumber? PhoneNumber  {get;private set;}
        public  Password? Password  {get;private set;}
        private IndividualCientValuObjects(FullName? fullName, Email? email, PhoneNumber? phoneNumber, Password? password)
        {
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
        }
        public static IndividualCientValuObjects GetWithNulls(bool isFullnameNull=false,bool isEmailNull=false,bool isPhoneNull=false,bool isPasswordNull=false)
        {
            (var fullName,var email,var phone,var password) = ValidTestData.GetAllIndividualsValues();
            if(isFullnameNull) fullName = null;
            if(isEmailNull) email = null;
            if(isPhoneNull) phone = null;
            if(isPasswordNull) password = null;
            
            return new IndividualCientValuObjects(fullName, email, phone, password);
        }
        
        public static IndividualCientValuObjects Get()
        {   
            
            (var fullName,var email,var phone,var password) = ValidTestData.GetAllIndividualsValues();
            return new IndividualCientValuObjects(fullName, email, phone, password);
        }
        
        
    }
    
    
    public static IEnumerable<object[]> GetInvalidFullNameTestData()
    {
        yield return new object[]
        {
            InvalidTestData.Whitespace, ValidTestData.MiddleName, ValidTestData.LastName, new List<Error> { FirstNameIsRequired }
        };
        
        yield return new object[]
        {
            ValidTestData.FirstName, InvalidTestData.Whitespace, ValidTestData.LastName, new List<Error> { MiddleNameIsRequired }
        };
        
        yield return new object[]
        {
            ValidTestData.FirstName, ValidTestData.MiddleName, InvalidTestData.Whitespace, new List<Error> { LastNameIsRequired }
        };
        
        yield return new object[]
        {
            ValidTestData.ValidLongName, ValidTestData.MiddleName, ValidTestData.LastName, new List<Error> { FirstNameIsTooLarge }
        };
        
        yield return new object[]
        {
            ValidTestData.FirstName, ValidTestData.ValidLongName, ValidTestData.LastName, new List<Error> { MiddleNameIsTooLarge }
        };
        
        yield return new object[]
        {
            ValidTestData.FirstName, ValidTestData.MiddleName, ValidTestData.ValidLongName, new List<Error> { LastNameIsTooLarge }
        };
    }
    
    public static IEnumerable<object[]> GetCreateIndividualClientNullCases()
    {
        yield return IndividualClientNullCases.AllNull;
        yield return IndividualClientNullCases.NullEmail;
        yield return IndividualClientNullCases.NullPassword;

    
    }
    
    
    
    public IEnumerable<object[]>GetStringForCountOfCharacters(int count)
    {
        yield return new object[]
        {
            new string('A',count)
        };
    }
    
    [Fact]
    public void CreateIndividualClientSuccessfully()
    {
        // Arrange 
        (var fullName, var email, var phone, var password) = ValidTestData.GetAllIndividualsValues();
        
        // Act
        var createClient = IndividualClient.Create( email, password);
        
        // Assert
        createClient.IsSuccess.Should().BeTrue();
        createClient.Value.Should().BeOfType<IndividualClient>();
        
        var client=createClient.Value;
        client.Email.Should().NotBeNull();
        client.Password.Should().NotBeNull();

        client.FullName.HasValue.Should().BeFalse();
        client.PhoneNumber.HasValue.Should().BeFalse();
    }
    
    
    [Theory]
    [MemberData(nameof(GetCreateIndividualClientNullCases))]
    public void CantCreateIndividualClient(
        bool isPasswordNull,
        bool isEmailNull)
    {
        // Arrange 
        var clientValueObjects = IndividualCientValuObjects.GetWithNulls(isEmailNull:isEmailNull,isPasswordNull: isPasswordNull);
        
        // Act
        Func<Result<IndividualClient, IReadOnlyList<Error>>> createClient
            = () => IndividualClient.Create(clientValueObjects.Email!, clientValueObjects.Password!);

        // Assert
        createClient.Should().Throw<Exception>();
    }
    [Fact]
    public void ProvideIndividualClientsDataSuccessfully()
    {
        // Arrange 
        var client = ValidTestData.GetIndividualWithoutFullData();
        (var fullName, var phone) = ValidTestData.GetDataForIndividualToProvide();

        
        //Act
        client.ProvideDataOrThrow(phone,fullName);
        
        // Assert
        client.FullName.HasValue.Should().BeTrue();
        var fName =client.FullName.Value;
        
        fName.FirstName.Should().Be(fullName.FirstName);
        fName.MiddleName.Should().Be(fullName.MiddleName);
        fName.LastName.Should().Be(fullName.LastName);
        
        client.PhoneNumber.HasValue.Should().BeTrue();
        var clientsPhone = client.PhoneNumber.Value;
        
        clientsPhone.Value.Should().Be(phone.Value);
    }
    
    [Theory]
    [InlineData(ValidTestData.TestPhoneNumber)]
    public void CreatePhoneNumberSuccessfully(string phone)
    {
        // Act 
        var createPN = PhoneNumber.Create(phone);
        
        // Assert
        createPN.IsSuccess.Should().BeTrue();
        createPN.Value.Should().BeOfType<PhoneNumber>();
    }
    
    [Theory]
    [InlineData(InvalidTestData.InvalidPhoneString)]
    [InlineData(InvalidTestData.EmptyPhone)]
    [InlineData(InvalidTestData.TooLongPhone)]
    public void CantCreatePhoneNumber(string phone)
    {
        // Act 
        var createPN = PhoneNumber.Create(phone);
        
        // Assert
        createPN.IsSuccess.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(ValidTestData.ValidEmail)]
    public void CreateEmailSuccessfully(string email)
    {
        // Act 
        var createEmail = Email.Create(email);
        
        // Assert
        createEmail.IsSuccess.Should().BeTrue();
        createEmail.Value.Should().BeOfType<Email>();
    }
    
    [Theory]
    [InlineData(InvalidTestData.InvalidEmailNoAt)]
    [InlineData(InvalidTestData.InvalidEmailNoDomain)]
    [InlineData(InvalidTestData.InvalidEmailNoAddress)]
    [InlineData("")]
    [InlineData(" ")]

    public void CantCreateEmail(string email)
    {
        // Act 
        var createEmail = Email.Create(email);
        
        // Assert
        createEmail.IsSuccess.Should().BeFalse();
    }

    [Theory]
    [InlineData(ValidTestData.FirstName, ValidTestData.MiddleName, ValidTestData.LastName)]
    public void CreateFullNameSuccessfully(string firstName, string middleName, string lastName)
    {
        // Act
        var create = FullName.Create(firstName, middleName, lastName);
        
        // Assert
        create.IsSuccess.Should().BeTrue();
        create.Value.Should().BeOfType<FullName>();
    }

    [Theory]
    [MemberData(nameof(GetInvalidFullNameTestData))]
    public void CantCreateFullName(string firstName, string middleName, string lastName, List<Error> errors)
    {
        // Act
        var create = FullName.Create(firstName, middleName, lastName);

        // Assert
        create.IsSuccess.Should().BeFalse();
        create.Error.All(e => errors.Contains(e)).Should().BeTrue();
    }
    
    [Theory]
    [InlineData(" ", "", ValidTestData.LastName, 2)]
    [InlineData(null, " ", ValidTestData.LastName, 2)]
    [InlineData(ValidTestData.ValidLongName, ValidTestData.ValidLongName, " ", 3)]
    public void CantCreateFullNameWithMultipleIssues(string firstName, string middleName, string lastName, int errorsCount)
    {
        // Act
        var create = FullName.Create(firstName, middleName, lastName);
        
        // Assert
        create.IsSuccess.Should().BeFalse();
        create.Error.Count.Should().Be(errorsCount);
    }
}


