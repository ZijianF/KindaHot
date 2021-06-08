using System;
using UnityEngine;

public class BulletTimeWatcher :  IWatcher
{
    private bool IsOn;
    private bool NewState;
    private Func<bool> GetCurrentState;
    private Action<bool> CallBack; 


    /// <summary>
    /// Constructs a ColorWatcher.
    /// </summary>
    /// <param name="getColorValue">A delegate that takes a returns a color
    /// value. This will be used by the watcher to detect value changes.
    /// </param>
    /// <param name="callback">A delegate that is called when the color value
    /// has changed. The newly-changed color value is passed to the callback
    /// via its only parameter.</param>
    public BulletTimeWatcher(Func<bool> getCurrentState, Action<bool> callback )
    {
        this.GetCurrentState = getCurrentState;
        this.CallBack = callback;
        Watch();
    }


     /// <summary>
     /// This method checks for state changes based on the stored value and current
     /// polled value.If a change is detected, it should execute the CallBack
     /// delegate.
     /// This function should be called in the Update function of the script that
     /// is utilizing the ColorWatcher.
     /// </summary>
    public void Watch ()
    {
        // this.NewState = this.GetCurrentState;
        if(NewState != IsOn){
            IsOn = NewState;
            CallBack(IsOn);
        }
        //Poll for a potential update to the color.
        //If the polled value is different than Value:
        //-update via CallBack
        //-store the updated value as Value
    }
}