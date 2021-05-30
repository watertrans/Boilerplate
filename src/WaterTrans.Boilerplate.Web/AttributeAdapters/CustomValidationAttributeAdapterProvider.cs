using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using WaterTrans.Boilerplate.Web.DataAnnotations;

namespace WaterTrans.Boilerplate.Web.AttributeAdapters
{
    public class CustomValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is AdapteredValidationAttribute)
            {
                return new AdapteredValidationAttributeAdapter(attribute as AdapteredValidationAttribute, stringLocalizer);
            }
            else if (attribute is EnumDataTypeAttribute)
            {
                return new EnumDataTypeAttributeAdapter(attribute as EnumDataTypeAttribute, stringLocalizer);
            }
            else
            {
                return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
            }
        }
    }
}
