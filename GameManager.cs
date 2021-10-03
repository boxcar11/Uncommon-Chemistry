using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject recipes;
    public GameObject[] itemSpaces;
    public GameObject[] itemPrefabs;
    public Canvas canvas;
    public TMP_Text text;
    public Slider slider;
    public GameObject itemHolder;
    public GameObject loseMenu;
    public TMP_Text highscoreText;
    public GameObject PauseMenu;
    public GameObject RecipeChoicePanel;
    public RecipeContoller rc;
    public Image mixture;
    public AudioMixer mixer;

    public RandomRecipe randomRecipe;
    public List<string> ingredients;

    public List<string> mandtoryIngredients;

    private GameObject[] item;
    private int count;
    public int stability;
    private int score;

    private int numItems;
    private int numKnown;
    private Recipe recipe;
    private GameObject recipeGO;

    private bool win = false;
    private bool lose = false;
    private bool pause = false;
    // Start is called before the first frame update
    void Start()
    {
        ingredients = new List<string>();
        mandtoryIngredients = new List<string>();
        item = new GameObject[itemPrefabs.Length];
        NewRecipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        for (int i = 0; i < item.Length; i++)
        {
            if (item[i] == null)
            {
                item[i] = Instantiate(itemPrefabs[i], new Vector3(-320, 60, 0), Quaternion.identity);
                item[i].transform.SetParent(itemSpaces[i].transform, false);
                item[i].transform.position = itemSpaces[i].transform.position;
                item[i].GetComponent<DragDrop>().SetCanvas(canvas);
            }
        }

        if (recipe != null && !win && !lose)
        {
            if (count >= numItems)
            {
                if (CheckIngredients())
                {
                    if (stability <= 1 && stability >= -1)
                    {
                        Debug.Log("WIN");
                        score += Mathf.Max((numItems*4) - (count - numItems), 2);

                        ResetLevel();
                    }
                }
            }
            text.text = score.ToString();

            if (stability > slider.maxValue || stability < slider.minValue)
            {
                Debug.Log("YOU LOSE!!!!!!!!!!!!");
                Lose();
            }


            float value = (float)1 / recipe.range;
            Color c = new Color(1, 1 - (value * Mathf.Abs(stability)), 1 - (value * Mathf.Abs(stability)));
            mixture.color = c;
            //mixture.color = new Color(1, .25f, .25f);
        }
    }

    public void AddPotion()
    {
        count++;
    }

    public void AddStability(int s)
    {
        stability += s;
        slider.value = stability;
    }

    public void AddIngredient(string ingredient)
    {
        ingredients.Add(ingredient);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void SetMasterLevel(float slideValue)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(slideValue) * 20);
    }

    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void Pause()
    {
        pause = !pause;
        PauseMenu.SetActive(pause);
    }

    public void PickRecipe(int itemCount, int numOfKnown)
    {
        recipeGO = Instantiate(recipes, transform.position, Quaternion.identity);
        recipeGO.transform.SetParent(canvas.transform, false);

        for (int i = 0; i < itemCount; i++)
        {
            Image im = NewItem(i, recipeGO);
            im.sprite = rc.GetSprite(i);
        }

        numKnown = numOfKnown;
        for (int i = 0; i < numOfKnown; i++)
        {
            mandtoryIngredients.Add(rc.GetName(i)+("(Clone)"));
        }

        recipe = recipeGO.GetComponent<Recipe>();
        slider.minValue = -10;
        slider.maxValue = 10;
        numItems = itemCount;
        rc.gameObject.SetActive(false);
    }

    private void NewRecipe()
    {
        rc.ClearLists();
        RecipeChoicePanel.SetActive(true);
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

    private void ResetLevel()
    {
        ingredients.Clear();
        Destroy(recipeGO);
        stability = 0;
        count = 0;
        NewRecipe();
    }

    private bool CheckIngredients()
    {
        int required = 0;
        foreach (string ingredient in mandtoryIngredients)
        {
            if (ingredients.Contains(ingredient))
            {
                required++;
                //Debug.Log(required);
            }
        }

        if (required >= numKnown)
        {
            return true;
        }

        return false;
    }

    private void Lose()
    {
        lose = true;
        int highscore = PlayerPrefs.GetInt("Score");

        if (score > highscore)
        {
            PlayerPrefs.SetInt("Score", score);
            highscoreText.text = "NEW Highscore!!!";
            Debug.Log("New Score");
        }
        else
        {
            highscoreText.text = "Nice try.";
            Debug.Log("Nice Try");
        }


        loseMenu.SetActive(true);
    }
}
