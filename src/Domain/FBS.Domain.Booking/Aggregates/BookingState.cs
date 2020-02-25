namespace FBS.Domain.Booking
{
    public enum BookingState : byte
    {
        Requested = 0,
        Approved = 1,
        Rejected = 2
    }
}