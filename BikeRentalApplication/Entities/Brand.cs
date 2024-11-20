namespace BikeRentalApplication.Entities
{
    public class Brand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Bike>? Bikes { get; set; }

    }
}
