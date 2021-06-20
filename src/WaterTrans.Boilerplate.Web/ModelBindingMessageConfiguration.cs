using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using WaterTrans.Boilerplate.Web.Resources;

namespace WaterTrans.Boilerplate.Web
{
    public class ModelBindingMessageConfiguration : IConfigureOptions<MvcOptions>
    {
        private readonly IStringLocalizer<ErrorMessages> _stringLocalizer;
        public ModelBindingMessageConfiguration(IStringLocalizer<ErrorMessages> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }
        public void Configure(MvcOptions options)
        {
            options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => _stringLocalizer.GetString("ModelBindingValueIsInvalidAccessor", x));
            options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => _stringLocalizer.GetString("ModelBindingValueMustBeANumberAccessor", x));
            options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => _stringLocalizer.GetString("ModelBindingMissingBindRequiredValueAccessor", x));
            options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => _stringLocalizer.GetString("ModelBindingAttemptedValueIsInvalidAccessor", x, y));
            options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => _stringLocalizer.GetString("ModelBindingMissingKeyOrValueAccessor"));
            options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(x => _stringLocalizer.GetString("ModelBindingUnknownValueIsInvalidAccessor", x));
            options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => _stringLocalizer.GetString("ModelBindingValueMustNotBeNullAccessor", x));
            options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(x => _stringLocalizer.GetString("ModelBindingNonPropertyAttemptedValueIsInvalidAccessor", x));
            options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => _stringLocalizer.GetString("ModelBindingNonPropertyUnknownValueIsInvalidAccessor"));
            options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => _stringLocalizer.GetString("ModelBindingNonPropertyValueMustBeANumberAccessor"));
            options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => _stringLocalizer.GetString("ModelBindingMissingRequestBodyRequiredValueAccessor"));
        }
    }
}
