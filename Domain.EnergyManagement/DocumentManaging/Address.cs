using System.Text.RegularExpressions;
using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error;
namespace Domain.EnergyManagement.DocumentManaging
{
    /// <summary>
    /// Представляет почтовый адрес как объект-значение.
    /// Содержит обязательные поля (индекс, регион, город, улица, дом)
    /// и необязательные — корпус и квартира.
    /// </summary>
    /// <remarks>
    /// Визуализация структуры адреса и рекомендуемая нормализация:
    /// ![Address structure](../docs/images/address_map.svg)
    /// </remarks>
    public class Address:ValueObject
    {
        /// <summary>Почтовый индекс (6 цифр).</summary>
        public string PostalCode { get; set; }
        public static Regex PostalCodeRegex = new Regex(@"^\d{6}$");
        /// <summary>Регион проживания.</summary>
        public string Region { get; set; }
        /// <summary>Город.</summary>
        public string City { get; set; }
        /// <summary>Улица.</summary>
        public string Street { get; set; }
        /// <summary>Номер дома.</summary>
        public string House { get; set; }
        private string? _building; 
        /// <summary>Корпус (если есть).</summary>
        public Maybe<string> Building =>Maybe.From(_building!);
        private string? _apartment;
        /// <summary>Квартира (если есть).</summary>
        public Maybe<string> Apartment =>Maybe.From(_apartment!);
        private Address(
            string postalCode,
            string region,
            string city,
            string street,
            string house,
            string? building,
            string? apartment)
        {
            PostalCode = postalCode;
            Region = region;
            City = city;
            Street = street;
            House = house;
            _building = building;
            _apartment = apartment;
        }
        private Address(){}
        /// <summary>
        /// Фабричный метод для создания <see cref="Address"/> с валидацией полей.
        /// Возвращает ошибки валидации в случае некорректных входных данных.
        /// </summary>
        public static Result<Address, IReadOnlyList<Error>> Create(
            string postalCode,
            string region,
            string city,
            string street,
            string house,
            string? building,
            string? apartment)
        {
            var errors = new List<Error>();
            var validateCode =ValidatePostalCode(postalCode);
            if(validateCode.IsFailure)
            {
                errors = errors.Concat(validateCode.Error).ToList();
            }  
            
            var validateRegion=ValidateRegion(region);
            if(validateRegion.IsFailure)
            {
                errors = errors.Concat(validateRegion.Error).ToList();
            } 
            
            var validateCity=ValidateCity(city);
            if(validateCity.IsFailure)
            {
                errors = errors.Concat(validateCity.Error).ToList();
            } 
            
            var validateStreet=ValidateStreet(street);
            if(validateStreet.IsFailure)
            {
                errors = errors.Concat(validateStreet.Error).ToList();
            } 
            
            var validateHouse=ValidateHouse(house);
            if(validateHouse.IsFailure)
            {
                errors = errors.Concat(validateHouse.Error).ToList();
            } 
            
            if(building is not null)
            {
                var validateBuilding=ValidateBuildingOrThrow(building);
                if(validateBuilding.IsFailure)
                {
                    errors = errors.Concat(validateBuilding.Error).ToList();
                }
            }
            
            if(apartment is not null)
            {
                var validateApartment=ValidateAppartmentOrThrow(apartment);
                if(validateApartment.IsFailure)
                {
                    errors = errors.Concat(validateApartment.Error).ToList();
                }
            }
            
            if(errors.Any())
            {
                return Result.Failure<Address, IReadOnlyList<Error>>(errors);
            }
            
            return Result.Success<Address, IReadOnlyList<Error>>(new Address(postalCode, region, city, street, house, building, apartment));
            
        }

        private static UnitResult<IReadOnlyList<Error>> ValidateAppartmentOrThrow(string apartment)
        {
            Guard.IsNotNull(apartment);
            
            var errors = new List<Error>();
            
            if(apartment.Length > 50)
            {
                errors.Add(Errors.AddressErrors.ApartmentIsTooLong);
            }
            
            if(errors.Any())
            {
                return UnitResult.Failure<IReadOnlyList<Error>>(errors);
            }
            
            return UnitResult.Success<IReadOnlyList<Error>>();
        }

        private static UnitResult<IReadOnlyList<Error>> ValidateBuildingOrThrow(string building)
        {
            Guard.IsNotNull(building);
            
            var errors = new List<Error>();
            
            if(building.Length > 50)
            {
                errors.Add(Errors.AddressErrors.BuildingIsTooLong);
            }
            
            if(errors.Any())
            {
                return UnitResult.Failure<IReadOnlyList<Error>>(errors);
            }
            
            return UnitResult.Success<IReadOnlyList<Error>>();
        }

        private static UnitResult<IReadOnlyList<Error>> ValidateStreet(string street)
        {
            var errors = new List<Error>();
            if(string.IsNullOrWhiteSpace(street))
            {
                errors.Add(Errors.AddressErrors.StreetIsRequired);
            }
            
            if(street.Length > 50)
            {
                errors.Add(Errors.AddressErrors.StreetIsTooLong);
            }
            
            if(errors.Any())
            {
                return UnitResult.Failure<IReadOnlyList<Error>>(errors);
            }
            
            return UnitResult.Success<IReadOnlyList<Error>>();
        }

        private static UnitResult<IReadOnlyList<Error>> ValidateHouse(string house)
        {
            var errors = new List<Error>();
            if(string.IsNullOrWhiteSpace(house))
            {
                errors.Add(Errors.AddressErrors.HouseIsRequired);
            }
            
            if(house.Length > 50)
            {
                errors.Add(Errors.AddressErrors.HouseIsTooLong);
            }
            
            if(errors.Any())
            {
                return UnitResult.Failure<IReadOnlyList<Error>>(errors);
            }
            
            return UnitResult.Success<IReadOnlyList<Error>>();
        }

        private static UnitResult<IReadOnlyList<Error>> ValidateCity(string city)
        {
            var errors = new List<Error>();
            if(string.IsNullOrWhiteSpace(city))
            {
                errors.Add(Errors.AddressErrors.CityIsRequired);
            }
            
            if(city.Length > 50)
            {
                errors.Add(Errors.AddressErrors.CityIsTooLong);
            }
            
            if(errors.Any())
            {
                return UnitResult.Failure<IReadOnlyList<Error>>(errors);
            }
            
            return UnitResult.Success<IReadOnlyList<Error>>();
        }

        private static UnitResult<IReadOnlyList<Error>> ValidateRegion(string region)
        {
            var errors = new List<Error>();
            if(string.IsNullOrWhiteSpace(region))
            {
                errors.Add(Errors.AddressErrors.RegionIsRequired);
            }
            
            if(region.Length > 50)
            {
                errors.Add(Errors.AddressErrors.RegionIsTooLong);
            }
            
            if(errors.Any())
            {
                return UnitResult.Failure<IReadOnlyList<Error>>(errors);
            }
            
            return UnitResult.Success<IReadOnlyList<Error>>();
        }

        private static UnitResult<IReadOnlyList<Error>> ValidatePostalCode(string postalCode)
        {
            var errors = new List<Error>();
            if(string.IsNullOrWhiteSpace(postalCode))
            {
                errors.Add(Errors.AddressErrors.PostalCodeIsRequired);
            }
            
            if(!PostalCodeRegex.IsMatch(postalCode))
            {
                errors.Add(Errors.AddressErrors.PostalCodeIsInvalid);
            }
            
            if(errors.Any())
            {
                return UnitResult.Failure<IReadOnlyList<Error>>(errors);
            }
            
            return UnitResult.Success<IReadOnlyList<Error>>();
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return PostalCode;
            yield return Region;
            yield return City;
            yield return Street;
            yield return House;
            if(_building is not null)
            {
                yield return _building;
            }
            if(_apartment is not null)
            {
                yield return _apartment;
            }
        }
    }
}