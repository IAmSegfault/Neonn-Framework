using System.Collections.Generic;
using Raylib_cs;

namespace NeonnCore.core;

public class Input
{
    public static readonly int NUM_KEYCODES = 256;
    private static List<int> CurrentKeys = new List<int>();
    private static List<int> DownKeys = new List<int>();
    private static List<int> UpKeys = new List<int>();
    public static void update()
    {
        UpKeys.Clear();
        for (int i = 0; i < NUM_KEYCODES; i++)
        {
            if (!GetKey(i) && CurrentKeys.Contains(i))
            {
                UpKeys.Add(i);
            }
        }
        
        DownKeys.Clear();
        
        for (int i = 0; i < NUM_KEYCODES; i++)
        {
            if (GetKey(i) && !CurrentKeys.Contains(i))
            {
                DownKeys.Add(i);
            }
        }
        
        CurrentKeys.Clear();
        for (int i = 0; i < NUM_KEYCODES; i++)
        {
            if (GetKey(i))
            {
                CurrentKeys.Add(i);
            }
        }
    }

    public static bool GetKey(int keyCode)
    {
        return Raylib.IsKeyDown((KeyboardKey)keyCode);
    }

    public static bool GetKeyDown(int keyCode)
    {
        if (DownKeys.Contains(keyCode))
        {
            return true;
        }

        return false;
    }

    public static bool GetKeyUp(int keyCode)
    {
        if (UpKeys.Contains(keyCode))
        {
            return true;
        }

        return false;
    }
}