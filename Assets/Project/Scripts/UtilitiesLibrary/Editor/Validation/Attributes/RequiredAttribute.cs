using System;
using UnityEngine;

namespace UtilitiesLibrary.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class RequiredAttribute : PropertyAttribute
    {
        public string Message { get; }
        public RequiredAttribute()
        {
            Message = "Field referece or value is required";
        }
        public RequiredAttribute(string message)
        {
            Message = message;
        }
        public bool LogToConsoleIfNullOrEmpty { get; set; } = false;
        public LogType LogType { get; set; } = LogType.Warning;
    }
}
