using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEffect : MonoBehaviour
{
    public Animator Anim;


    public void EndAnimation()
    {
        gameObject.SetActive(false);
    }
}
