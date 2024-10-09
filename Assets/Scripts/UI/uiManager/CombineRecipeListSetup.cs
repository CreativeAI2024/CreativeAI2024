using Unity.VisualScripting;
using UnityEngine;

public class CombineRecipeListSetup : MonoBehaviour
{
  [SerializeField] private CombineRecipeList combineRecipeList;

  void Start()
  {
    combineRecipeList.Setup();
  }
}
