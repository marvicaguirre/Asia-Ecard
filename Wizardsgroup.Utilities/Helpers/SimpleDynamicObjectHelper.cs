using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Wizardsgroup.Utilities.Helpers
{
    public sealed class SimpleDynamicObjectHelper
    {
        public static TypeBuilder GetTypeBuilder(int hashCode)
        {
            var an = new AssemblyName("DynamicAssembly" + hashCode);
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicGridModule");

            var tb = moduleBuilder.DefineType("DynamicType"
                                , TypeAttributes.Public |
                                TypeAttributes.Class |
                                TypeAttributes.AutoClass |
                                TypeAttributes.AnsiClass |
                                TypeAttributes.BeforeFieldInit |
                                TypeAttributes.AutoLayout
                                , typeof(object));
            return tb;
        }

        public static void CreateProperty(TypeBuilder builder, string propertyName, Type propertyType)
        {
            FieldBuilder fieldBuilder = builder.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);
            PropertyBuilder propertyBuilder = builder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);

            MethodBuilder getPropertyBuiler = CreatePropertyGetter(builder, fieldBuilder);
            MethodBuilder setPropertyBuiler = CreatePropertySetter(builder, fieldBuilder);

            propertyBuilder.SetGetMethod(getPropertyBuiler);
            propertyBuilder.SetSetMethod(setPropertyBuiler);
        }

        private static MethodBuilder CreatePropertyGetter(TypeBuilder typeBuilder, FieldBuilder fieldBuilder)
        {
            MethodBuilder getMethodBuilder =
                typeBuilder.DefineMethod("get_" + fieldBuilder.Name,
                    MethodAttributes.Public |
                    MethodAttributes.SpecialName |
                    MethodAttributes.HideBySig,
                    fieldBuilder.FieldType, Type.EmptyTypes);

            ILGenerator getIL = getMethodBuilder.GetILGenerator();

            getIL.Emit(OpCodes.Ldarg_0);
            getIL.Emit(OpCodes.Ldfld, fieldBuilder);
            getIL.Emit(OpCodes.Ret);

            return getMethodBuilder;
        }

        private static MethodBuilder CreatePropertySetter(TypeBuilder typeBuilder, FieldBuilder fieldBuilder)
        {
            MethodBuilder setMethodBuilder =
                typeBuilder.DefineMethod("set_" + fieldBuilder.Name,
                  MethodAttributes.Public |
                  MethodAttributes.SpecialName |
                  MethodAttributes.HideBySig,
                  null, new Type[] { fieldBuilder.FieldType });

            ILGenerator setIL = setMethodBuilder.GetILGenerator();

            setIL.Emit(OpCodes.Ldarg_0);
            setIL.Emit(OpCodes.Ldarg_1);
            setIL.Emit(OpCodes.Stfld, fieldBuilder);
            setIL.Emit(OpCodes.Ret);

            return setMethodBuilder;
        }

        public static object SetProperty(object target, string name, object value, bool ignoreIfTargetIsNull)
        {
            if (ignoreIfTargetIsNull && target == null) return null;
            object[] values = { value };
            object oldProperty = GetProperty(target, name, true);
            PropertyInfo targetProperty = target.GetType().GetProperty(name);
            if (targetProperty == null)
            {
                throw new Exception("Object " + target + "   does not have Target Property " + name);
            }
            targetProperty.GetSetMethod().Invoke(target, values);
            return oldProperty;
        }

        public static object GetProperty(object target, string name, bool throwError)
        {
            PropertyInfo targetProperty = target.GetType().GetProperty(name);
            if (targetProperty != null) return targetProperty.GetGetMethod().Invoke(target, null);

            if (throwError)
            {
                throw new Exception("Object " + target + "   does not have Target Property " + name);
            }
            return null;
        }
    }
}
