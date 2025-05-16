using System.ComponentModel;

namespace Policies.Core.Helpers
{
    public static class EnumExtensions
    {
        public static T ObtenerValor<T>(string descripcion) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == descripcion)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == descripcion)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException(nameof(descripcion));
        }
        public static string ObtenerDescripcionEnum<T>(T enumerationValue) where T : struct
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException($"{nameof(enumerationValue)}");
            }
            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return enumerationValue.ToString();
        }
    }
}
