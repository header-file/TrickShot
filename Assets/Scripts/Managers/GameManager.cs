using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;

    public ObjectManager ObjManager;
    public InputManager IptManager;
    public UiManager UiManager;
    public StageManager StgManager;
    public BulletManager BltManager;

    public Player Player;

    public GrayScaleCamera GrayCamera;


    public static GameManager Inst() { return Instance; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

   void Start()
    {
        UiManager = ObjManager.MakeObj("UI").GetComponent<UiManager>();
        UiManager.transform.parent = transform.parent;
    }

    void Update()
    {
        
    }
}
