using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    private static int seconds;

    public static void SetSeconds(int newSeconds)
    {
        seconds = newSeconds;
    }

    public static int GetSeconds()
    {
        return seconds;
    }

    public static void Reset()
    {
        seconds = 0;
    }
}
