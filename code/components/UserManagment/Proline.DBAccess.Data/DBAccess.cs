namespace Proline.DBAccess.Data
{
    public class InsertSaveRequest
    {
        public string Identity { get; set; }
        public string Data { get; set; }
        public long PlayerId { get; set; }
    }

    public class BaseResponse
    {
        public int ReturnCode { get; set; } 
    }

    public class InsertSaveResponse : BaseResponse
    {
        public long Id { get; set; }
        public string Identity { get; set; }
        public long SaveId { get; set; }
    }

    public class RegisterPlayerRequest
    {
        public string Name { get; set; }
    }

    public class RegisterPlayerResponse :  BaseResponse
    {
        public long Id { get; set; }
    }

    public class GetSaveRequest
    {
        public string Identity { get; set; } 
        public string Username { get; set; }
        public long Id { get; set; }
    }

    public class GetSaveResponse : BaseResponse
    {
        public SaveFile[] SaveFiles { get; set; }
    }

    public class SaveFile
    { 
        public string Identity { get; set; }
        public string Data { get; set; }
    }

    public class BaseRequest
    {
    }

    public class GetPlayerRequest : BaseRequest
    { 
        public string Username { get; set; } 
    }

    public class GetPlayerResponse : BaseResponse
    {
        public long PlayerId { get; set; } 
    }
}
