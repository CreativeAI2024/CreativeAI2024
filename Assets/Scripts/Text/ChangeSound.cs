using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSound : MonoBehaviour
{
    [SerializeField]SoundManager soundManager;
    // Start is called before the first frame update
    public void ChangeBGM(string fileName)
    {
        for (int i = 0; i < soundManager.audioClipsBGM.Count; i++)
        {
            if (fileName.Equals(soundManager.audioClipsBGM[i].name))
            {
                soundManager.PlayBGM(i);
                return;
            }else if (fileName.Equals("stop"))  //jsonのBGM欄にstopと記述したときBGMを止める
            {
                soundManager.StopBGM();
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
