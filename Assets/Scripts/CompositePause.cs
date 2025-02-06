using System.Collections.Generic;
using UnityEngine;

public class CompositePause : Pause
{
    [SerializeField] List<Pause> uiPauses = new();
    public void PauseOthers(Pause uiPause)
    {
        foreach (Pause pause in uiPauses)
        {
            pause.PauseAll();

        }
    }
    public void UnPauseOthers()
    {
        foreach (Pause pause in uiPauses)
        {
            pause.UnPauseAll();
        }
    }
}
