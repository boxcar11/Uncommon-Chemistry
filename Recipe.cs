using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    public int range;
    public int numIngredients;
    public int maxScore;
    public int minScore;

    public List<string> mandtoryIngredients;

    void Start()
    {
        int count = this.transform.childCount;
        int width = (count * 50) + (10 * count) + 20;
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 75);
    }
}
