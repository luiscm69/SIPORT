using System;
using System.Globalization;
using System.Web.Mvc;

namespace $rootnamespace$.Models.Binds
{
    public class DecimalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            ModelState modelState = new ModelState { Value = valueResult };

            object actualValue = null;

            if (!string.IsNullOrEmpty(valueResult.AttemptedValue))
            {
                try
                {

                    // Tries to figure out how many decimal characters we have after the decimal separator.
                    string value = valueResult.AttemptedValue;

                    int decimals = 0;

                    // Make sure it will break the checking right after discovering how many decimal characters there is after the decimal character. 
                    while (true)
                    {

                        if (value[value.Length - 3] == ',' || value[value.Length - 3] == '.')
                        {
                            decimals = 2;
                            break;
                        }

                        if (value[value.Length - 4] == ',' || value[value.Length - 4] == '.')
                        {
                            decimals = 3;
                            break;
                        }

                        if (value[value.Length - 5] == ',' || value[value.Length - 5] == ',')
                        {
                            decimals = 4;
                            break;
                        }

                        break;
                    }


                    // Replace all special characters in order to make the proper conversion from the string to the decimal.
                    string final_value;

                    final_value = value.Replace(",", "").Replace(".", "");

                    // Insert the decimal character at the correct position given the ammount of decimal characters we retrieved.
                    final_value = final_value.Insert(final_value.Length - decimals, ".");

                    // Converts the actual decimal.
                    actualValue = Convert.ToDecimal(final_value, CultureInfo.InvariantCulture);
                }
                catch (FormatException e)
                {
                    modelState.Errors.Add(e);
                }
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);

            return actualValue;
        }
    }
}