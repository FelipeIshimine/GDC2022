using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_MainMenu : MonoBehaviour
{
    public static event Action OnPlayPressed;

    public void Pressed()
    {
        Debug.Log("PRESSED");
        OnPlayPressed?.Invoke();
    }
}
