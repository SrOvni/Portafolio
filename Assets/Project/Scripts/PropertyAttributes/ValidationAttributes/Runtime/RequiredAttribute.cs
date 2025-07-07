using UnityEngine;
namespace ValidationAttributes
{
    public class RequiredAttribute : PropertyAttribute
    {
        public readonly string Message;

        public RequiredAttribute(string message = null)
        {
            Message = message;
        }
    }
}