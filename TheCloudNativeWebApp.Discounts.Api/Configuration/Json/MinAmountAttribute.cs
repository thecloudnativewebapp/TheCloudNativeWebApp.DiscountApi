using System.ComponentModel.DataAnnotations;
using TheCloudNativeWebApp.Discounts.Core;

namespace TheCloudNativeWebApp.Discounts.Api.Configuration.Json;

public class MinAmountAttribute : ValidationAttribute
{
    private Euro _minValue;

    public MinAmountAttribute(double minValue)
    {
        _minValue = minValue;
    }

    public override bool IsValid(object? objectToCompare)
    {
        if (objectToCompare == null)
        {
            return true; // Should be handled using the [Required] attribute
        }

        if (!(objectToCompare is Euro valueToCompare))
        {
            return false;
        }

        return _minValue.IsSmallerThan(valueToCompare) || _minValue.Equals(valueToCompare);
    }
}