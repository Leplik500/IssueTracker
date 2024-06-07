using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace IssueTracker.Domain.Extensions;

public static class EnumExtension {
    public static String GetDisplayName(this System.Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.GetName() ?? enumValue.ToString();
    }
}