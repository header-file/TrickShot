using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject BulletPool;
    public GameObject EffectPool;
    public GameObject EnemyPool;
    public GameObject ObjectPool;

    //Bullet Pref
    public GameObject NormalPref;
    public GameObject PiercePref;
    public GameObject TimePref;

    //Enemy Pref
    public GameObject RedPref;

    //Effect Pref
    public GameObject TwinklePref;
    public GameObject ExplosionPref;

    //Object Pref
    public GameObject WallPref;
    public GameObject BreakPref;
    public GameObject MovePref;
    public GameObject SpinPref;
    public GameObject ElevatePref;

    GameObject[] Normals;
    GameObject[] Pierces;
    GameObject[] Times;

    GameObject[] Twinkles;
    GameObject[] Explosions;

    GameObject[] Reds;

    GameObject[] Walls;
    GameObject[] Breaks;
    GameObject[] Moves;
    GameObject[] Spins;
    GameObject[] Elevates;

    GameObject[] TargetPool;

    
    void Awake()
    {
        Normals = new GameObject[5];
        Pierces = new GameObject[5];
        Times = new GameObject[5];

        Reds = new GameObject[10];

        Explosions = new GameObject[5];
        Twinkles = new GameObject[5];

        Walls = new GameObject[10];
        Breaks = new GameObject[10];
        Moves = new GameObject[10];
        Spins = new GameObject[10];
        Elevates = new GameObject[10];

        Generate();
    }

    void Generate()
    {
        for (int i = 0; i < Normals.Length; i++)
        {
            Normals[i] = Instantiate(NormalPref);
            Normals[i].transform.SetParent(BulletPool.transform, false);
            Normals[i].SetActive(false);
        }

        for (int i = 0; i < Pierces.Length; i++)
        {
            Pierces[i] = Instantiate(PiercePref);
            Pierces[i].transform.SetParent(BulletPool.transform, false);
            Pierces[i].SetActive(false);
        }

        for (int i = 0; i < Times.Length; i++)
        {
            Times[i] = Instantiate(TimePref);
            Times[i].transform.SetParent(BulletPool.transform, false);
            Times[i].SetActive(false);
        }


        for (int i = 0; i < Reds.Length; i++)
        {
            Reds[i] = Instantiate(RedPref);
            Reds[i].transform.SetParent(EnemyPool.transform, false);
            Reds[i].SetActive(false);
        }


        for (int i = 0; i < Twinkles.Length; i++)
        {
            Twinkles[i] = Instantiate(TwinklePref);
            Twinkles[i].transform.SetParent(EffectPool.transform, false);
            Twinkles[i].SetActive(false);
        }

        for (int i = 0; i < Explosions.Length; i++)
        {
            Explosions[i] = Instantiate(ExplosionPref);
            Explosions[i].transform.SetParent(EffectPool.transform, false);
            Explosions[i].SetActive(false);
        }


        for (int i = 0; i < Walls.Length; i++)
        {
            Walls[i] = Instantiate(WallPref);
            Walls[i].transform.SetParent(ObjectPool.transform, false);
            Walls[i].SetActive(false);
        }

        for (int i = 0; i < Breaks.Length; i++)
        {
            Breaks[i] = Instantiate(BreakPref);
            Breaks[i].transform.SetParent(ObjectPool.transform, false);
            Breaks[i].SetActive(false);
        }

        for (int i = 0; i < Moves.Length; i++)
        {
            Moves[i] = Instantiate(MovePref);
            Moves[i].transform.SetParent(ObjectPool.transform, false);
            Moves[i].SetActive(false);
        }

        for (int i = 0; i < Spins.Length; i++)
        {
            Spins[i] = Instantiate(SpinPref);
            Spins[i].transform.SetParent(ObjectPool.transform, false);
            Spins[i].SetActive(false);
        }

        for (int i = 0; i < Elevates.Length; i++)
        {
            Elevates[i] = Instantiate(ElevatePref);
            Elevates[i].transform.SetParent(ObjectPool.transform, false);
            Elevates[i].SetActive(false);
        }
    }

    public GameObject MakeObj(string Type)
    {
        switch (Type)
        {
            case "Normal":
                TargetPool = Normals;
                break;

            case "Pierce":
                TargetPool = Pierces;
                break;

            case "Time":
                TargetPool = Times;
                break;


            case "Red":
                TargetPool = Reds;
                break;


            case "Twinkle":
                TargetPool = Twinkles;
                break;

            case "Explosion":
                TargetPool = Explosions;
                break;


            case "Wall":
                TargetPool = Walls;
                break;

            case "Break":
                TargetPool = Breaks;
                break;

            case "Move":
                TargetPool = Moves;
                break;

            case "Spin":
                TargetPool = Spins;
                break;

            case "Elevate":
                TargetPool = Elevates;
                break;
        }

        for (int i = 0; i < TargetPool.Length; i++)
        {
            if (!TargetPool[i].activeSelf)
            {
                TargetPool[i].SetActive(true);
                return TargetPool[i];
            }
        }

        return null;
    }
}
