using RealEstate.Models.DatabaseModels;

namespace RealEstate.Models.ViewModels
{
	public class ParameterView : Parameter
	{
        public OfferParameter? Parameter { get; set; }

        public ParameterView(Parameter parameter, OfferParameter? offerParam)
        {
            this.Id = parameter.Id;
            this.Value = parameter.Value;
            this.Parameter = offerParam;
        }

    }
}
