using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CauldronSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameManager gameManager;
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        if(eventData.pointerDrag != null)
        {
            int s =eventData.pointerDrag.GetComponent<Potion>().GetStability();
            gameManager.AddIngredient(eventData.pointerDrag.name);
            Destroy(eventData.pointerDrag);
            gameManager.AddPotion();
            gameManager.AddStability(s);
        }
    }
}
