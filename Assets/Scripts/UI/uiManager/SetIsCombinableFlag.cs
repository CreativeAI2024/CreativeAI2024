using UnityEngine;

public class SetIsCombinableFlag : MonoBehaviour
{
  [SerializeField] private CombineRecipeList combineRecipeList;
  void Start()
  {
    combineRecipeList.SetIsCombinable();
  }
}