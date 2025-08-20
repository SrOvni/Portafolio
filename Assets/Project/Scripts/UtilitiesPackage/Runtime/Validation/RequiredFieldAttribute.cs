using UnityEngine;
using System;
namespace UtilitiesLibrary.Validation
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class RequiredFieldAttribute : PropertyAttribute
    {
        public RequiredFieldAttribute()
        {
            
        }
    }
}
