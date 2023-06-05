using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GLOBAL_VARIABLES : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool debug = true;

    public int desiredFPS = 60;


    void Awake()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = desiredFPS;
#endif
    }

    void Update()
    {
        if (SimpInput.getIfPressed(new string[] { "st" }))
        {
            isPaused = !isPaused;
        }

        if (SimpInput.getIfPressed(new string[] { "se" }))
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
    }






}
