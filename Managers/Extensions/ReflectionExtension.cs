using System.Collections.Generic;

namespace SirenixPowered.ExtensionMethods
{
    public static class ReflectionExtension
    {
        public static System.Type[] GetAllDerivedTypes(this System.AppDomain aAppDomain, System.Type aType)
        {
            List<System.Type> result = new List<System.Type>();
            System.Reflection.Assembly[] assemblies = aAppDomain.GetAssemblies();

            foreach (System.Reflection.Assembly assembly in assemblies)
            {
                System.Type[] types = assembly.GetTypes();

                foreach (System.Type type in types)
                {
                    if (type.IsSubclassOf(aType))
                    {
                        result.Add(type);
                    }                
                }
            }

            return result.ToArray();
        }

        public static System.Type[] GetAllDerivedTypes<T>(this System.AppDomain aAppDomain)=> GetAllDerivedTypes(aAppDomain, typeof(T));

        public static System.Type[] GetTypesWithInterface(this System.AppDomain aAppDomain, System.Type aInterfaceType)
        {
            List<System.Type> result = new List<System.Type>();
            System.Reflection.Assembly [] assemblies = aAppDomain.GetAssemblies();

            foreach (System.Reflection.Assembly assembly in assemblies)
            {
                System.Type[] types = assembly.GetTypes();

                foreach (System.Type type in types)
                {
                    if (aInterfaceType.IsAssignableFrom(type))
                    {
                        result.Add(type);
                    }  
                }
            }
            return result.ToArray();
        }

        public static System.Type[] GetTypesWithInterface<T>(this System.AppDomain aAppDomain) => GetTypesWithInterface(aAppDomain, typeof(T));
    }
}