using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSound : MonoBehaviour
{
    [SerializeField]SoundManager soundManager;

    public void ChangeBGM(string fileName)
    {
        for (int i = 0; i < soundManager.audioClipsBGM.Count; i++)
        {
            if (fileName.Equals(soundManager.audioClipsBGM[i].name))
            {
                soundManager.PlayBGM(i);
                return;
            }
        }
    }

    public void ChangeSE(string fileName)
    {
        for (int i = 0; i < soundManager.audioClipsSE.Count; i++)
        {
            if (fileName.Equals(soundManager.audioClipsSE[i].name))
            {
                soundManager.PlaySE(i);
                return;
            }
        }
    }
}
