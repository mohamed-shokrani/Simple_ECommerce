using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using Core.Constants;

namespace Core.Attributes;

public class MinimumQuantityAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is int quantity && quantity < ConstantForProducts.MinimumQuantity)
        {
            return false;
        }

        return true;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"The {name} field cannot be less than {ConstantForProducts.MinimumQuantity}.";
    }
}

