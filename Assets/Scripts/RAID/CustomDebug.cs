using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public static class CustomDebug
{
    public static void Log(string message, Object context = default)
    {
#if UNITY_EDITOR
        Debug.Log($"<color=green>{message}</color>.", context);
#endif
    }

    public static void LogW(string message, Object context = default)
    {
#if UNITY_EDITOR
        Debug.LogWarning($"<color=yellow>{message}</color>.", context);
#endif
    }

    public static void LogE(string message, Object context = default)
    {
#if UNITY_EDITOR
        Debug.LogError($"<color=red>{message}</color>.", context);
#endif
    }

    public static bool LogCheckAssigned<Ty>(Ty item, Object context = default, string message = default) where Ty : UnityEngine.Object
    {
        string msg = default;
        if (string.IsNullOrEmpty(message))
        {
            if (Utils.IsValid(item))
            {
                msg = item.name;
            }
            else
            {
                msg = default;
            }
        }
        else
        {
            msg = message;
        }

        if (false == item || true == ReferenceEquals(item, null))
        {
#if UNITY_EDITOR
            Debug.LogError($"<color=red>{msg}</color> is not assigned! and Type is <color=orange>{item.GetType().Name}</color>.", context);
#endif
            return false;
        }
        else
        {
            return true;
        }
    }
};
