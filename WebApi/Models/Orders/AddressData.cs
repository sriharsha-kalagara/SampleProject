namespace WebApi.Models.Orders
{
    public class AddressData
    {
        public AddressData(BusinessEntities.Address address )
        {
            Street = address.Street;
            City =  address.City;
            State = address.State;
            ZipCode = address.ZipCode;
            Country = address.Country;
        }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}