using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyActions
{
    Up,
    Down,
    Left,
    Right,
    Action
}

public static class InputManager
{
    private static KeyCode[][] playerKeys = new KeyCode[4][];

    public static void Initialize()
    {
        playerKeys[0] = new KeyCode[]
        {
            KeyCode.W,
            KeyCode.S,
            KeyCode.A,
            KeyCode.D,
            KeyCode.LeftShift
        };

        playerKeys[1] = new KeyCode[]
{
            KeyCode.UpArrow,
            KeyCode.DownArrow,
            KeyCode.LeftArrow,
            KeyCode.RightArrow,
            KeyCode.Keypad0
};

        playerKeys[2] = new KeyCode[]
{
            KeyCode.I,
            KeyCode.K,
            KeyCode.J,
            KeyCode.L,
            KeyCode.Space
};

        playerKeys[3] = new KeyCode[]
{
            KeyCode.Keypad8,
            KeyCode.Keypad5,
            KeyCode.Keypad4,
            KeyCode.Keypad6,
            KeyCode.KeypadEnter
};
    }

    public static bool GetKey(int player, KeyActions action)
    {
        if (Input.GetKey(playerKeys[player][(int)action]))
        {
            return true;
        }
        return false;
    }

    public static bool GetKeyDown(int player, KeyActions action)
    {
        if (Input.GetKeyDown(playerKeys[player][(int)action]))
        {
            return true;
        }
        return false;
    }

    public static bool GetKeyUp(int player, KeyActions action)
    {
        if (Input.GetKeyUp(playerKeys[player][(int)action]))
        {
            return true;
        }
        return false;
    }

    public static void SetKey(int player, KeyActions action, KeyCode keyCode)
    {
        playerKeys[player][(int)action] = keyCode;
    }

    public static KeyCode CheckKey(int player, KeyActions action)
    {
        return playerKeys[player][(int)action];
    }
}
