using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    public Animator anim;
    public SpriteRenderer lightningBG;

    public int sec;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        sec = Random.Range(20, 120);
        StartCoroutine(WaitXSeconds());
    }

    // Update is called once per frame
    void Update()
    {
        if (sec <= 0)
        {
            lightningBG.enabled = true;
            anim.SetBool("strike", true);
            sec = Random.Range(20, 120);
            StartCoroutine(WaitXSeconds());
        }
    }

    IEnumerator WaitXSeconds()
    {
        while (sec > 0)
        {
            yield return new WaitForSeconds(1);
            sec--;
        }
    }

    public void ShowImage()
    {
        lightningBG.enabled = (true);
        Debug.Log("Image visible");
    }

    public void HideImage()
    {
        lightningBG.enabled = (false);
        anim.SetBool("strike", false);
        Debug.Log("Image hidden");
    }
}
