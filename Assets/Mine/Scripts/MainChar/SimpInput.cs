using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpInput : MonoBehaviour
{
    public static Gamepad gamepad;
    //public static string[] buffer;
    //public static List<string[]> buffer = new List<string[]>();
    public static LinkedList<string[]> buffer = new LinkedList<string[]>();

    void Start()
    {
        gamepad = Gamepad.current;
        //Time.timeScale = 0.01f;

    }

    void Update()
    {
        gamepad = Gamepad.current;




    }

    void FixedUpdate()
    {
        updateBuffer();

        //print(Time.time + " - " + getLastBufferedInput());

        if (!GLOBAL_VARIABLES.debug)
        {
            string i = Time.time.ToString() + " - ";
            foreach (var buff in buffer)
            {

                foreach (string s in buff)
                {
                    i += s + "+";
                }
                i += " ////// ";
            }
            print(i);
            print(Time.time + " - " + arraytostring(getLastBufferedInput()));
        }
    }
    private string arraytostring(string[] array)
    {
        string val = "[";

        foreach (string s in array)
        {
            val += s + " + ";
        }
        val += "]";
        return val;
    }

    private void updateBuffer()
    {
        List<string> listOfInputs = new List<string>(getLastInput());


        //if the buffer size is 5, it will start popping 
        if (buffer.Count > 4)
            buffer = pop(buffer);

        //if there's no current input, it won't add it to the buffer
        //if (listOfInputs.ToArray().Length >= 0)
        buffer = push(buffer, listOfInputs.ToArray());


    }

    public static bool getIfPressed(string[] buttons)
    {
        bool pressed = true;
        if (gamepad != null && buttons != null)
        {
            foreach (string button in buttons)
            {
                switch (button)
                {
                    case "x": if (!gamepad.crossButton.wasPressedThisFrame) pressed = false; break;
                    case "s": if (!gamepad.squareButton.wasPressedThisFrame) pressed = false; break;
                    case "c": if (!gamepad.circleButton.wasPressedThisFrame) pressed = false; break;
                    case "t": if (!gamepad.triangleButton.wasPressedThisFrame) pressed = false; break;
                    case "u": if (!gamepad.dpad.up.wasPressedThisFrame) pressed = false; break;
                    case "d": if (!gamepad.dpad.down.wasPressedThisFrame) pressed = false; break;
                    case "l": if (!gamepad.dpad.left.wasPressedThisFrame) pressed = false; break;
                    case "r": if (!gamepad.dpad.right.wasPressedThisFrame) pressed = false; break;
                    case "r1": if (!gamepad.rightShoulder.wasPressedThisFrame) pressed = false; break;
                    case "r2": if (!gamepad.rightTrigger.wasPressedThisFrame) pressed = false; break;
                    case "l1": if (!gamepad.leftShoulder.wasPressedThisFrame) pressed = false; break;
                    case "l2": if (!gamepad.leftTrigger.wasPressedThisFrame) pressed = false; break;
                    case "rs": if (!gamepad.rightStickButton.wasPressedThisFrame) pressed = false; break;
                    case "ls": if (!gamepad.leftStickButton.wasPressedThisFrame) pressed = false; break;
                    case "st": if (!gamepad.startButton.wasPressedThisFrame) pressed = false; break;
                    case "se": if (!gamepad.selectButton.wasPressedThisFrame) pressed = false; break;
                }
            }
        }
        else
        {
            pressed = false;
        }
        return pressed;
    }


    public static bool getIfReleased(string[] buttons)
    {
        bool released = true;
        if (gamepad != null && buttons != null)
        {
            foreach (string button in buttons)
            {
                switch (button)
                {
                    case "x": if (!gamepad.crossButton.wasReleasedThisFrame) released = false; break;
                    case "s": if (!gamepad.squareButton.wasReleasedThisFrame) released = false; break;
                    case "c": if (!gamepad.circleButton.wasReleasedThisFrame) released = false; break;
                    case "t": if (!gamepad.triangleButton.wasReleasedThisFrame) released = false; break;
                    case "u": if (!gamepad.dpad.up.wasReleasedThisFrame) released = false; break;
                    case "d": if (!gamepad.dpad.down.wasReleasedThisFrame) released = false; break;
                    case "l": if (!gamepad.dpad.left.wasReleasedThisFrame) released = false; break;
                    case "r": if (!gamepad.dpad.right.wasReleasedThisFrame) released = false; break;
                    case "r1": if (!gamepad.rightShoulder.wasReleasedThisFrame) released = false; break;
                    case "r2": if (!gamepad.rightTrigger.wasReleasedThisFrame) released = false; break;
                    case "l1": if (!gamepad.leftShoulder.wasReleasedThisFrame) released = false; break;
                    case "l2": if (!gamepad.leftTrigger.wasReleasedThisFrame) released = false; break;
                    case "rs": if (!gamepad.rightStickButton.wasReleasedThisFrame) released = false; break;
                    case "ls": if (!gamepad.leftStickButton.wasReleasedThisFrame) released = false; break;
                    case "st": if (!gamepad.startButton.wasReleasedThisFrame) released = false; break;
                    case "se": if (!gamepad.selectButton.wasReleasedThisFrame) released = false; break;
                }
            }
        }
        else
        {
            released = false;
        }
        return released;
    }

    public static bool getIfHeld(string[] buttons)
    {
        bool held = true;
        if (gamepad != null && buttons != null)
        {
            foreach (string button in buttons)
            {
                switch (button)
                {
                    case "x": if (!gamepad.crossButton.isPressed) held = false; break;
                    case "s": if (!gamepad.squareButton.isPressed) held = false; break;
                    case "c": if (!gamepad.circleButton.isPressed) held = false; break;
                    case "t": if (!gamepad.triangleButton.isPressed) held = false; break;
                    case "u": if (!gamepad.dpad.up.isPressed) held = false; break;
                    case "d": if (!gamepad.dpad.down.isPressed) held = false; break;
                    case "l": if (!gamepad.dpad.left.isPressed) held = false; break;
                    case "r": if (!gamepad.dpad.right.isPressed) held = false; break;
                    case "r1": if (!gamepad.rightShoulder.isPressed) held = false; break;
                    case "r2": if (!gamepad.rightTrigger.isPressed) held = false; break;
                    case "l1": if (!gamepad.leftShoulder.isPressed) held = false; break;
                    case "l2": if (!gamepad.leftTrigger.isPressed) held = false; break;
                    case "rs": if (!gamepad.rightStickButton.isPressed) held = false; break;
                    case "ls": if (!gamepad.leftStickButton.isPressed) held = false; break;
                    case "st": if (!gamepad.startButton.isPressed) held = false; break;
                    case "se": if (!gamepad.selectButton.isPressed) held = false; break;
                }
            }
        }
        else
        {
            held = false;
        }
        return held;
    }

    public static string[] getLastInput()
    {
        List<string> listOfInputs = new List<string>();
        if (gamepad != null)
        {
            if (gamepad.crossButton.isPressed) listOfInputs.Add("x");
            if (gamepad.squareButton.isPressed) listOfInputs.Add("s");
            if (gamepad.circleButton.isPressed) listOfInputs.Add("c");
            if (gamepad.triangleButton.isPressed) listOfInputs.Add("t");
            if (gamepad.dpad.up.isPressed) listOfInputs.Add("u");
            if (gamepad.dpad.down.isPressed) listOfInputs.Add("d");
            if (gamepad.dpad.left.isPressed) listOfInputs.Add("l");
            if (gamepad.dpad.right.isPressed) listOfInputs.Add("r");
            if (gamepad.rightShoulder.isPressed) listOfInputs.Add("r1");
            if (gamepad.rightTrigger.isPressed) listOfInputs.Add("r2");
            if (gamepad.leftShoulder.isPressed) listOfInputs.Add("l1");
            if (gamepad.leftTrigger.isPressed) listOfInputs.Add("l2");
            if (gamepad.rightStickButton.isPressed) listOfInputs.Add("rs");
            if (gamepad.leftStickButton.isPressed) listOfInputs.Add("ls");
            if (gamepad.startButton.isPressed) listOfInputs.Add("st");
            if (gamepad.selectButton.isPressed) listOfInputs.Add("se");
        }
        return listOfInputs.ToArray();
    }

    public static string[] getLastBufferedInput()
    {
        if (buffer.Count > 0)
        {
            string[] lastNonNull = null;

            foreach (string[] input in buffer)
            {
                if (input.Length > 0)
                {
                    lastNonNull = input;
                    break;
                }
            }


            //return buffer.First.Value;
            return lastNonNull;
        }
        else
        {
            return null;
        }
    }

    public static bool getIfBufferedInput(string[] input)
    {
        bool found = false;
        if (buffer.Count > 0)
        {
            found = Enumerable.SequenceEqual(buffer.First.Value, input);
            //return buffer.First.Value;
        }
        print(found);
        return found;

    }




    private LinkedList<string[]> pop(LinkedList<string[]> array)
    {
        if (array != null && array.Count > 0)
        {
            array.RemoveLast();
        }

        return array;
    }

    private LinkedList<string[]> push(LinkedList<string[]> array, string[] input)
    {
        if (array != null)
        {
            array.AddFirst(input);
        }

        return array;
    }





}
