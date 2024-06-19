using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManagerTester : MonoBehaviour
{
    public SoundManager SoundManager;

    void Start()
    {
        // ゲーム開始時に最初のBGMを再生
        SoundManager.PlayBGM("8-bit_Aggressive1", 1f);
    }

    // シーンが移動するときに、BGMを変更してSEを再生
    public void ToSampleScene()
    {
        SoundManager.PlaySE("Pa", 0.5f); // SEを再生

        // シーンを切り替える
        SceneManager.LoadScene("SampleScene");

        // 切り替えた後に別のBGMを再生
        SoundManager.PlayBGM("Pappa_Parappa", 0.7f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
