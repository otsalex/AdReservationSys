namespace Public.DTO.v1;

public class AdSpaceMin{
  
    public Guid Id { get; set; }
    public required string Side { get; set; }
    public  CarrierMin? Carrier { get; set; }
}