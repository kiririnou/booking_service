namespace BookingService.WebApi.Contracts.V1.Requests
{
    public class CreateUserRequest
    {
        public string Name      { get; set; }
        public string TgUid    { get; set; }
    }
}