using UnityEngine;

public class Logger: MonoBehaviour
{
    [SerializeField] private bool isLoggerActive = false;
    public void Log(object message)
    {
        if(isLoggerActive is false)return;
        Debug.Log(message);
    }
}
