using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerTester : DontDestroySingleton<ManagerTester>
{
    //サーチゲームの呼び出し&消しをテストするためのもの
    //[SerializeField] private SearchGameManager searchGameManager;

    public override void Awake()
    {
        base.Awake();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene("SearchGame");
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            //searchGameManager.Inactivate();
        }
    }
}
