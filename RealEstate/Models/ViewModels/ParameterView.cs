using RealEstate.Models.DatabaseModels;

namespace RealEstate.Models.ViewModels
{
	public class ParameterView
	{
        public int IdParameter { get; set; }
        public string ParameterValue { get; set; }
        public string? Value { get; set; }
        public ParameterView(int idParam, string paramValue, string? value)
        {
            this.IdParameter = idParam;
            this.ParameterValue = paramValue;
            this.Value = value;
        }
        public ParameterView()
        {
                
        }
    }
}
