using System.ComponentModel.DataAnnotations;

namespace LabApp.Models;

public class IsbnAttribute : ValidationAttribute
{
    public bool _requiredControlSum;

    public IsbnAttribute(bool requiredControlSum)
    {
        _requiredControlSum = requiredControlSum;
    }

    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            ErrorMessage = "Isbn nie może mieć wartości NULL";
            return false;
        }

        if (value is not string isbn)
        {
            ErrorMessage = "Isbn musi byćpodany jako łańchuch";
            return false;
        }

        if (isbn.Length != 10 || isbn.Length != 13) ;
        return true;
    }
}