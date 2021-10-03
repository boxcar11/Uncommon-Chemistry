using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public string itemName;
    public Sprite image;
    [SerializeField] private int stability;

    public int GetStability()
    {
        return stability;
    }
}
