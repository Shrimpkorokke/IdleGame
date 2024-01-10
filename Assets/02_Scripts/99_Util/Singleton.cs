using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : new()
{
    private static T m_instance;

    public static T instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new T();
            }

            return m_instance;
        }
    }

    public static T I
    {
        get
        {
            return instance;
        }
    }
}
