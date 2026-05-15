using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.Data;

namespace EnergyManagement.Server.Api.Contracts.Auth;

public static class AuthFieldNames
{
    public static class Register
    {
        public static string Email => JsonField.Of<RegisterClientDto>(x => x.Email);
        public static string Password => JsonField.Of<RegisterClientDto>(x => x.Password);
        public static string PasswordConfirmation => JsonField.Of<RegisterClientDto>(x => x.PasswordConfirmation);
    }

    public static class Login
    {
        public static string Email => JsonField.Of<LoginDto>(x => x.Email);
        public static string Password => JsonField.Of<LoginDto>(x => x.Password);
    }

    public static class ProvideIndividualClientData
    {
        public static string Phone => JsonField.Of<ProvideIndividualClientsDataDto>(x => x.PhoneNumber);
        public static string FullName => JsonField.Of<ProvideIndividualClientsDataDto>(x => x.FullNameDto);
    }
}
