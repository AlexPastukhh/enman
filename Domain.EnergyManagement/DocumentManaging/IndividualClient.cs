using System.Text.RegularExpressions;
using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using static Domain.EnergyManagement.Common.Error.Errors.Account;

namespace Domain.EnergyManagement.DocumentManaging
{
public class IndividualClient:Client
{
    private PhoneNumber? _phoneNumber {get;set;}
    /// <summary>Номер телефона клиента (возможно отсутствует).</summary>
    public Maybe<PhoneNumber> PhoneNumber =>Maybe.From(_phoneNumber!);
    private FullName? _fullName {get;set;}
    /// <summary>Ф.И.О. клиента (возможно отсутствует).</summary>
    public Maybe<FullName> FullName=>Maybe.From(_fullName!);
    private PassportData? _passportData {get;set;}
    /// <summary>Данные паспорта (возможно отсутствуют).</summary>
    public Maybe<PassportData> PassportData =>Maybe.From(_passportData!);
    // private List<ConnectedObject> _connectedObjects = new();
    // public IReadOnlyList<ConnectedObject> ConnectedObjects => _connectedObjects.AsReadOnly();
    private List<IndividualRequest> _clientRequests = new();
    /// <summary>Список запросов, созданных клиентом.</summary>
    public virtual IReadOnlyList<IndividualRequest> ClientRequests => _clientRequests.AsReadOnly();
    private IndividualClient(
        Email email,
        Password password)
    : base(password, email)
    {
    }
    private IndividualClient(
        Email email,
        FullName fullName,
        Password password)
    : base(password, email)
    {
        _fullName =fullName;
    }
    private IndividualClient():base(){}
    
    /// <summary>
    /// Фабричный метод создания `IndividualClient`.
    /// Валидирует обязательные поля и возвращает объект или ошибки.
    /// </summary>
    public static Result<IndividualClient, IReadOnlyList<Error>> Create(
        
        Email email,
        Password password)
    {

        Guard.IsNotNull(email);
        Guard.IsNotNull(password);
        
        return Result.Success<IndividualClient, IReadOnlyList<Error>>(
            new IndividualClient(email, password));
    }
    
    /// <summary>
    /// Заполняет дополнительные данные клиента (телефон и Ф.И.О.).
    /// Бросает исключение Guard при некорректных аргументах.
    /// </summary>
    public void ProvideDataOrThrow(
        PhoneNumber phoneNumber,
        FullName fullName)
    {

        Guard.IsNotNull(phoneNumber);
        Guard.IsNotNull(fullName);
        
        _fullName = fullName;
        _phoneNumber = phoneNumber;
    }
    
    /// <summary>
    /// Создаёт подключение (запрос) от данного клиента с указанными деталями и адресом.
    /// Возвращает ошибки в случае неуспеха создания запроса.
    /// </summary>
    public Result<IndividualRequest,IReadOnlyList<Error>> CreateConnectionRequest(string requestDetails, Address address)
    {
        var createNewRequest = IndividualRequest.Create(this,address, DateTimeOffset.Now, RequestType.Connection, requestDetails);
        
        if(createNewRequest.IsFailure)
        {
            return Result.Failure<IndividualRequest, IReadOnlyList<Error>>(createNewRequest.Error);
        }
        
        return Result.Success<IndividualRequest, IReadOnlyList<Error>>(createNewRequest.Value);
    }
    public void AddRequestOrThrow(IndividualRequest request)
    {
        Guard.IsNotNull(request);
        if(request.Client!=this)
        {
            throw new ArgumentException("Request client does not match.");
        }
        
        _clientRequests.Add(request);
    }
}


}




