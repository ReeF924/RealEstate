namespace RealEstate.Models.ComponentModels
{
    public class FilterComponentParameter
    {
        public FilterModel Filter { get; set; }
        public List<string> Regions { get; set; }
        public ulong MaxPrice { get; set; }

        public FilterComponentParameter(FilterModel filter, List<string> regions, ulong maxPrice)
        {
            this.Filter = filter;
            this.Regions = regions;
            this.MaxPrice = maxPrice;
        }
    }
}
