using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AhMedAladdinMVC.PL.ViewModels.Custom_Validation
{
	public class MustBeTrueAttribute : ValidationAttribute, IClientModelValidator
	{
		public MustBeTrueAttribute()
		{
			ErrorMessage = "You must agree to the terms and conditions.";
		}

		public override bool IsValid(object value)
		{
			return value is bool boolValue && boolValue;
		}

		public void AddValidation(ClientModelValidationContext context)
		{
			context.Attributes.Add("data-val", "true");
			context.Attributes.Add("data-val-mustbetrue", ErrorMessage);
		}
	}
}
