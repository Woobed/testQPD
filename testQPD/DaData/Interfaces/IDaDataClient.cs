namespace testQPD.DaData.Interfaces
{
    public interface IDaDataClient
    {
        Task<AddressResponse> CleanAddressAsync(string address);
    }
}
