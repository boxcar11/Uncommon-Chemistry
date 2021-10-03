using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRecipe : MonoBehaviour
{
    public GameObject[] recipeOptions;
    public Transform parent;

    public void SetRecipes()
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
        
        for (int i = 0; i < 3; i++)
        {
            int r = Random.Range(0, recipeOptions.Length);
            GameObject go = Instantiate(recipeOptions[r], transform.position, Quaternion.identity);
            go.transform.SetParent(parent, false);
        }
    }
}
