namespace Proline.DBAccess.Data
{
    public class PlacePlayerDataRequest
    {
        public string Data { get; set; }
    }

    public class PlacePlayerDataResponse
    {
        public long Id { get; set; }
    }

    public class GetPlayerDataInParameters
    {
        public long Id { get; set; }
        public string Data { get; set; }
    }
}
