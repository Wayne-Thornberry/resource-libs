namespace Proline.DBAccess.Data
{
    public class InsertSaveRequest
    {
        public string Data { get; set; }
        public long PlayerId { get; set; }
    }

    public class InsertSaveResponse
    {
        public long Id { get; set; }
        public int ReturnCode { get; set; }
    }

    public class GetSaveRequest
    {
        public long Id { get; set; } 
    }

    public class GetSaveResponse
    {
        public string Data { get; set; }
        public int ReturnCode { get; set; }
    }
}
