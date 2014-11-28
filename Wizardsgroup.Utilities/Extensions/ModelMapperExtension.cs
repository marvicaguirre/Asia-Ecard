using System;

namespace Wizardsgroup.Utilities.Extensions
{
    public static class ModelMapperExtension
    {
        public static TU Convert<T, TU>(this T input)
        {
            //1. Create the mapping; this is required
            AutoMapper.Mapper.CreateMap<T, TU>().MaxDepth(1); //.ForAllMembers(options);   
            //2. Do the actual mapping of data.
            TU output = AutoMapper.Mapper.Map<T, TU>(input);
            return output;
        }

        public static TU Convert<T, TU>(this T input, Action<T, TU> customMapper)
        {
            TU output = Convert<T, TU>(input);
            //do some custom property assignment here if there are properties of the DTO that does not come from the Entity.
            if (customMapper != null)
            {
                customMapper(input, output);
            }
            return output;
        }
    }
}