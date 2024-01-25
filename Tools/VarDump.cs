using System.Reflection;

namespace CWeb.Tools
{
    public class VarDump
    {
        public static void Dump(object obj)
        {
            if (obj == null)
            {
                Console.WriteLine("null");
                return;
            }

            Type type = obj.GetType();
            Console.WriteLine($"Type: {type.FullName}");

            PropertyInfo[] properties = type.GetProperties();

            foreach (var property in properties)
            {
                object value = property.GetValue(obj, null);
                Console.WriteLine($"{property.Name}: {value}");
            }
        }
    }
}
