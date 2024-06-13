using UnityEngine;

public class ResetWindowActiveAction : MonoBehaviour
{
    private bool initialActive;
    private OnWindowBoxDeactivated onWindowBoxDeactivated;
    void Start()
    {
        initialActive = gameObject.activeSelf;
        onWindowBoxDeactivated.Init(ResetWindowActive);
    }
    private void ResetWindowActive()
    {
        if (gameObject.activeSelf != gameObject.activeInHierarchy) {
            gameObject.SetActive(initialActive);
        }
    }
}
