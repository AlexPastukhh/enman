
using System.Security.Cryptography;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error;
using static Domain.EnergyManagement.Common.Error.Errors.Account;
using CommunityToolkit.Diagnostics;

namespace Domain.EnergyManagement.DocumentManaging
{
/// <summary>
/// Сущность клиента — содержит идентификатор, email и пароль.
/// </summary>
/// <remarks>
/// Структура сущности клиента и связанные объекты:
/// ![Client entity](../docs/images/client_entity_2.svg)
/// </remarks>
public class Client:Entity
{    
    /// <summary>
    /// Электронная почта клиента.
    /// </summary>
    public Email Email { get; private set;}

    /// <summary>
    /// Пароль клиента в виде объекта-значения `Password`.
    /// </summary>
    public Password Password{get;private set;}

    /// <summary>
    /// Конструктор для создания сущности клиента с обязательными данными.
    /// Защищённый — используется фабриками внутри домена.
    /// </summary>
    protected Client(Password password,
        Email email)
    {
        Email = email;
        Password = password;
    }
    protected Client()
    {
    }

}
}
