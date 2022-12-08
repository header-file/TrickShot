using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public ParticleSystem PS;


    void Update()
    {
        if (PS.isStopped)
        {
            transform.parent = GameManager.Inst().ObjManager.EffectPool.transform;
            gameObject.SetActive(false);
        }
    }
}