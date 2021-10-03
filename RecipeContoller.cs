using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeContoller : MonoBehaviour
{
    public List<Potion> items;

    public Sprite unknownSprite;

    [SerializeField] List<string> itemNames1;
    [SerializeField] List<string> itemNames2;
    [SerializeField] List<string> itemNames3;
    [SerializeField] List<Sprite> itemSprites1;
    [SerializeField] List<Sprite> itemSprites2;
    [SerializeField] List<Sprite> itemSprites3;

    private int size1;
    private int size2;
    private int size3;

    [SerializeField] List<string> itemName;
    [SerializeField] List<Sprite> itemSprite;

    // Start is called before the first frame update
    void Start()
    {
        itemName = new List<string>();
        itemSprite = new List<Sprite>();
        itemNames1 = new List<string>();
        itemSprites1 = new List<Sprite>();
        itemNames2 = new List<string>();
        itemSprites2 = new List<Sprite>();
        itemNames3 = new List<string>();
        itemSprites3 = new List<Sprite>();

        BuildRecipes();
    }

    public void BuildRecipes()
    {
        Debug.Log("Building Recipes");
        // Build first Recipe
        GameObject child = transform.GetChild(0).gameObject;

        // Randomly pick how many known items
        int randKnown = Random.Range(1, 4);

        // Randomly pick how many unkown items
        int randUnknown = Random.Range(1, 3);

        size1 = randKnown + randUnknown;

        for (int j = 0; j < randKnown; j++)
        {
            // Get random Item
            int randItem = Random.Range(0, items.Count);

            Image im = NewItem(0, child);
            im.sprite = items[randItem].image;
            itemSprites1.Add(items[randItem].image);
            itemNames1.Add(items[randItem].name);
        }

        for (int k = 0; k < randUnknown; k++)
        {
            Image im = NewItem(0, child);
            im.sprite = unknownSprite;
            itemSprites1.Add(unknownSprite);
        }

        // Ativate Button
        child.GetComponent<Button>().onClick.AddListener(() => { ChooseRecipe(0); });

        // Build first Recipe
        child = transform.GetChild(1).gameObject;

        // Randomly pick how many known items
        randKnown = Random.Range(1, 4);

        // Randomly pick how many unkown items
        randUnknown = Random.Range(1, 3);

        size2 = randKnown + randUnknown;


        for (int j = 0; j < randKnown; j++)
        {
            // Get random Item
            int randItem = Random.Range(0, items.Count);

            Image im = NewItem(1, child);
            im.sprite = items[randItem].image;
            itemSprites2.Add(items[randItem].image);
            itemNames2.Add(items[randItem].name);
        }

        for (int k = 0; k < randUnknown; k++)
        {
            Image im = NewItem(1, child);
            im.sprite = unknownSprite;
            itemSprites2.Add(unknownSprite);
        }

        // Ativate Button
        child.GetComponent<Button>().onClick.AddListener(() => { ChooseRecipe(1); });

        // Build first Recipe
        child = transform.GetChild(2).gameObject;

        // Randomly pick how many known items
        randKnown = Random.Range(1, 4);

        // Randomly pick how many unkown items
        randUnknown = Random.Range(1, 3);

        size3 = randKnown + randUnknown;


        for (int j = 0; j < randKnown; j++)
        {
            // Get random Item
            int randItem = Random.Range(0, items.Count);

            Image im = NewItem(2, child);
            im.sprite = items[randItem].image;
            itemSprites3.Add(items[randItem].image);
            itemNames3.Add(items[randItem].name);
        }

        for (int k = 0; k < randUnknown; k++)
        {
            Image im = NewItem(2, child);
            im.sprite = unknownSprite;
            itemSprites3.Add(unknownSprite);
        }

        // Ativate Button
        child.GetComponent<Button>().onClick.AddListener(() => { ChooseRecipe(2); });

    }

    // Update is called once per frame
    public void ClearLists()
    {
        itemName.Clear();
        itemSprite.Clear();
        itemNames1.Clear();
        itemSprites1.Clear();
        itemNames2.Clear(); 
        itemSprites2.Clear();
        itemNames3.Clear(); 
        itemSprites3.Clear();

        foreach (Transform c in transform.GetChild(0))
        {
            Destroy(c.gameObject);
        }
        foreach (Transform c in transform.GetChild(1))
        {
            Destroy(c.gameObject);
        }
        foreach (Transform c in transform.GetChild(2))
        {
            Destroy(c.gameObject);
        }

        BuildRecipes();
    }

    public void ChooseRecipe(int i)
    {
        switch (i)
        {
            case 0:
                itemSprite = itemSprites1;
                itemName = itemNames1;
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().PickRecipe(size1, itemName.Count);
                break;
            case 1:
                itemSprite = itemSprites2;
                itemName = itemNames2;
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().PickRecipe(size2, itemName.Count);
                break;
            case 2:
                itemSprite = itemSprites3;
                itemName = itemNames3;
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().PickRecipe(size3, itemName.Count);
                break;
        }
    }

    public Sprite GetSprite(int i)
    {
        return itemSprite[i];
    }

    public string GetName(int i)
    {
        return itemName[i];
    }

    private Image NewItem(int i, GameObject child)
    {
        // Build new GameObject

        GameObject recipeItem = new GameObject();
        Image im = recipeItem.AddComponent<Image>();
        recipeItem.transform.SetParent(child.transform, false);
        recipeItem.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
        return im;
    }
}
