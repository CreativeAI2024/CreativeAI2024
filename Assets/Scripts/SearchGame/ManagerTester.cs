using UnityEngine;

public class ManagerTester : MonoBehaviour
{
    //サーチゲームの呼び出し&消しをテストするためのもの
    [SerializeField] private SearchGameManager searchGameManager;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            searchGameManager.Activate();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            searchGameManager.Inactivate();
        }
    }
}