namespace Public.DTO.v1;

public class CarrierMin
{
    public required string City { get; set; }
    public required string Number { get; set; }
    public string? BusStopName { get; set; }
    public string? Street { get; set; }
    public string? Direction { get; set; }
}