namespace RealEstate.Models
{
    public class FilterModel
    {
        public ulong? PriceMin { get; set; }
        public ulong? PriceMax { get; set; }
        public string? Region { get; set; }
        public int? AreaMin { get; set; }
        public int? AreaMax { get; set; }
        public char? EnergyClass { get; set; }
    }
}
