using System.Linq.Expressions;
using UltimateRemote.Models;

namespace UltimateRemote.Extensions;
internal static class SelectOptionExtensions
{
    private static SelectOption ToSelectOption<T>(this T obj, Expression<Func<T, string>> labelExpression,
        Expression<Func<T, string>> valueExpression, Expression<Func<T, string?>>? iconCssExpression = null) where T : class
        => iconCssExpression != null
            ? new SelectOption(labelExpression.Compile().Invoke(obj), valueExpression.Compile().Invoke(obj), iconCssExpression.Compile().Invoke(obj)) : 
                new SelectOption(labelExpression.Compile().Invoke(obj), valueExpression.Compile().Invoke(obj));


    public static SelectOption[] ToSelectOptions<T>(this List<T> objArray, Expression<Func<T, string>> labelExpression,
        Expression<Func<T, string>> valueExpression, Expression<Func<T, string?>>? iconCssExpression = null)  where T : class
        => objArray.Select(obj => obj.ToSelectOption<T>(labelExpression, valueExpression, iconCssExpression))
            .ToArray();

    public static SelectOption[] ToSelectOptionsWithDefault<T>(this List<T> objArray,
        Expression<Func<T, string>> labelExpression,
        Expression<Func<T, string>> valueExpression, 
        Expression<Func<T, string?>>? iconCssExpression, 
        string labelDefault, string valueDefault, string? iconCssDefault, bool defaultItemFirst = false)
        where T : class
    {
        var selectOptions = objArray.ToSelectOptions(labelExpression, valueExpression, iconCssExpression).ToList();
        var defaultOption = new SelectOption(labelDefault, valueDefault, iconCssDefault);
        
        if(defaultItemFirst)
            selectOptions.Insert(0, defaultOption);
        else
            selectOptions.Add(defaultOption);

        return selectOptions.ToArray();
    }

    public static SelectOption[] ToSelectOptionsWithDefault<T>(this List<T> objArray,
        Expression<Func<T, string>> labelExpression,
        Expression<Func<T, string>> valueExpression,
        string labelDefault, string valueDefault, bool defaultItemFirst = false)
        where T : class
        => objArray.ToSelectOptionsWithDefault(labelExpression, valueExpression,
            iconCssExpression: null, labelDefault, valueDefault, iconCssDefault: null,
            defaultItemFirst);
    
}
