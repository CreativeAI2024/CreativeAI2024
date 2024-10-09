using Unity.VisualScripting;
using UnityEngine;

public class CombineRecipeDatabaseSetup : MonoBehaviour
{
  [SerializeField] private CombineRecipeDatabase combineRecipeDatabase;

  void Start()
  {
    combineRecipeDatabase.Setup();
  }
}
