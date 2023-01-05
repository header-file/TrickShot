using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;

    public ObjectManager ObjManager;
    public InputManager IptManager;
    public UiManager UiManager;
    public StageManager StgManager;
    public BulletManager BltManager;
    public DataManager DatManager;
    public HintManager HntManager;

    public Player Player;
    public VolumeProfile VolumeProfile;

    ColorAdjustments ColorAdj;
    bool IsGray;
    bool IsEndGray;


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
        if (IsGray)
            ToGray();
        else if (IsEndGray)
            EndGray();
    }

    public void StartToGray()
    {
        IsGray = true;

        if(VolumeProfile.TryGet(out ColorAdj))
            ColorAdj.saturation.value = 0;
    }

    void ToGray()
    {
        ColorAdj.saturation.value -= 2.0f;

        if(ColorAdj.saturation.value <= -100.0f)
        {
            ColorAdj.saturation.value = -100.0f;
            IsGray = false;
        }
    }

    public void StartEndGray()
    {
        IsGray = false;
        IsEndGray = true;
    }

    void EndGray()
    {
        ColorAdj.saturation.value += 2.0f;

        if (ColorAdj.saturation.value >= 0.0f)
        {
            ColorAdj.saturation.value = 0.0f;
            IsEndGray = false;
        }
    }
}
