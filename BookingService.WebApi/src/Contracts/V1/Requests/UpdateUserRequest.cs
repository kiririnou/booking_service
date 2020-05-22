namespace BookingService.WebApi.Contracts.V1.Requests
{
    public class UpdateUserRequest
    {
        public string Name      { get; set; }
        public string TgUid    { get; set; }
    }
}