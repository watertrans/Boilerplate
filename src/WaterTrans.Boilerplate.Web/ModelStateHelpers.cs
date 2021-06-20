using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using WaterTrans.Boilerplate.CrossCuttingConcerns.Utils;

namespace WaterTrans.Boilerplate.Web
{
    public static class ModelStateHelpers
    {
        public static string SerializeModelState(ModelStateDictionary modelState)
        {
            var errorList = modelState
                .Select(kvp => new ModelStateTransferValue
                {
                    Key = kvp.Key,
                    AttemptedValue = kvp.Value.AttemptedValue,
                    RawValue = kvp.Value.RawValue,
                    ErrorMessages = kvp.Value.Errors.Select(err => err.ErrorMessage).ToList(),
                });

            return JsonUtil.Serialize(errorList);
        }

        public static ModelStateDictionary DeserializeModelState(string serializedErrorList)
        {
            var errorList = JsonUtil.Deserialize<List<ModelStateTransferValue>>(serializedErrorList);
            var modelState = new ModelStateDictionary();

            foreach (var item in errorList)
            {
                modelState.SetModelValue(item.Key, item.RawValue, item.AttemptedValue);
                foreach (var error in item.ErrorMessages)
                {
                    modelState.AddModelError(item.Key, error);
                }
            }
            return modelState;
        }
    }
}
