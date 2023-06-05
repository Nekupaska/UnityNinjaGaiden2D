using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleInput : MonoBehaviour
{
    public static Gamepad gamepad;

    public static int justPressed = 0b0000000000000000; //16 bits, each one represents a button on a controller
    public static int justReleased = 0b0000000000000000; //16 bits, each one represents a button on a controller
    public static int beingHeld = 0b0000000000000000; //16 bits, each one represents a button on a controller

    public static int none = 0b0000000000000000;
    public static int all = 0b1111111111111111;

    public static int x = 0b0000000000000001;
    public static int cir = 0b0000000000000010;
    public static int sq = 0b0000000000000100;
    public static int tr = 0b0000000000001000;

    public static int up = 0b0000000000010000;
    public static int down = 0b0000000000100000;
    public static int left = 0b0000000001000000;
    public static int right = 0b0000000010000000;

    public static int r1 = 0b0000000100000000;
    public static int r2 = 0b0000001000000000;
    public static int l1 = 0b0000010000000000;
    public static int l2 = 0b0000100000000000;

    public static int ls = 0b0001000000000000;
    public static int rs = 0b0010000000000000;
    public static int start = 0b0100000000000000;
    public static int select = 0b1000000000000000;

    void Start()
    {
        gamepad = Gamepad.current;
    }

    void Update()
    {
        pressed();
        released();
        held();
    }

    void pressed()
    {
        justPressed = none; //reset
        if (gamepad.crossButton.wasPressedThisFrame) justPressed += x;
        if (gamepad.circleButton.wasPressedThisFrame) justPressed += cir;
        if (gamepad.squareButton.wasPressedThisFrame) justPressed += sq;
        if (gamepad.triangleButton.wasPressedThisFrame) justPressed += tr;
        //
        if (gamepad.dpad.up.wasPressedThisFrame) justPressed += up;
        if (gamepad.dpad.down.wasPressedThisFrame) justPressed += down;
        if (gamepad.dpad.left.wasPressedThisFrame) justPressed += left;
        if (gamepad.dpad.right.wasPressedThisFrame) justPressed += right;
        //
        if (gamepad.rightShoulder.wasPressedThisFrame) justPressed += r1;
        if (gamepad.rightTrigger.wasPressedThisFrame) justPressed += r2;
        if (gamepad.leftShoulder.wasPressedThisFrame) justPressed += l1;
        if (gamepad.leftTrigger.wasPressedThisFrame) justPressed += l2;
        //
        if (gamepad.leftStickButton.wasPressedThisFrame) justPressed += ls;
        if (gamepad.rightStickButton.wasPressedThisFrame) justPressed += rs;
        if (gamepad.startButton.wasPressedThisFrame) justPressed += start;
        if (gamepad.selectButton.wasPressedThisFrame) justPressed += select;
    }

    void released()
    {
        justReleased = none; //reset
        if (gamepad.crossButton.wasReleasedThisFrame) justReleased += x;
        if (gamepad.circleButton.wasReleasedThisFrame) justReleased += cir;
        if (gamepad.squareButton.wasReleasedThisFrame) justReleased += sq;
        if (gamepad.triangleButton.wasReleasedThisFrame) justReleased += tr;
        //
        if (gamepad.dpad.up.wasReleasedThisFrame) justReleased += up;
        if (gamepad.dpad.down.wasReleasedThisFrame) justReleased += down;
        if (gamepad.dpad.left.wasReleasedThisFrame) justReleased += left;
        if (gamepad.dpad.right.wasReleasedThisFrame) justReleased += right;
        //
        if (gamepad.rightShoulder.wasReleasedThisFrame) justReleased += r1;
        if (gamepad.rightTrigger.wasReleasedThisFrame) justReleased += r2;
        if (gamepad.leftShoulder.wasReleasedThisFrame) justReleased += l1;
        if (gamepad.leftTrigger.wasReleasedThisFrame) justReleased += l2;
        //
        if (gamepad.leftStickButton.wasReleasedThisFrame) justReleased += ls;
        if (gamepad.rightStickButton.wasReleasedThisFrame) justReleased += rs;
        if (gamepad.startButton.wasReleasedThisFrame) justReleased += start;
        if (gamepad.selectButton.wasReleasedThisFrame) justReleased += select;
    }

    void held()
    {
        beingHeld = none; //reset
        if (gamepad.crossButton.isPressed) justPressed += x;
        if (gamepad.circleButton.isPressed) beingHeld += cir;
        if (gamepad.squareButton.isPressed) beingHeld += sq;
        if (gamepad.triangleButton.isPressed) beingHeld += tr;
        //
        if (gamepad.dpad.up.isPressed) beingHeld += up;
        if (gamepad.dpad.down.isPressed) beingHeld += down;
        if (gamepad.dpad.left.isPressed) beingHeld += left;
        if (gamepad.dpad.right.isPressed) beingHeld += right;
        //
        if (gamepad.rightShoulder.isPressed) beingHeld += r1;
        if (gamepad.rightTrigger.isPressed) beingHeld += r2;
        if (gamepad.leftShoulder.isPressed) beingHeld += l1;
        if (gamepad.leftTrigger.isPressed) beingHeld += l2;
        //
        if (gamepad.leftStickButton.isPressed) beingHeld += ls;
        if (gamepad.rightStickButton.isPressed) beingHeld += rs;
        if (gamepad.startButton.isPressed) beingHeld += start;
        if (gamepad.selectButton.isPressed) beingHeld += select;
    }

    public static bool getIfPressed(string[] buttons)
    {
        bool pressed = true;
        foreach (string button in buttons)
        {
            switch (button)
            {
                case "x": break;
                case "s": break;
                case "c": break;
                case "t": break;
                case "u": break;
                case "d": break;
                case "l": break;
                case "r": break;
                case "r1": break;
                case "r2": break;
                case "l1": break;
                case "l2": break;
                case "ls": break;
                case "rs": break;
                case "st": break;
                case "se": break;
            }
        }

        return pressed;
    }
}
