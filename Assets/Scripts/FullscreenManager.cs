using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenManager : MonoBehaviour
{
    private bool currentlyFullscreen;
    private Resolution originalResolution;

    private void Start()
    {
        originalResolution = new Resolution();
        originalResolution.width = Screen.width;
        originalResolution.height = Screen.height;

        currentlyFullscreen = Screen.fullScreen;
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.F11) )
        {
            if( currentlyFullscreen )
            {
                currentlyFullscreen = false;
                //Screen.fullScreen = false;
                Screen.SetResolution( ( int )( Screen.currentResolution.width * 0.8f), ( int )( Screen.currentResolution.height * 0.8f), false );
            }
            else // if not currently full screen
            {
                currentlyFullscreen = true;
                //Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                //Screen.fullScreen = false;
                //Screen.fullScreen = true;

                Screen.SetResolution( originalResolution.width, originalResolution.height, true );
            }
        }
    }
}
