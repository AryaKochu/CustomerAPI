namespace CustomerAPI.Models.Response
{
    public class CustomerResponse
    {
        public bool IsSuccess { get; set; }
        public CommonError Error { get; set; }
    }
}
