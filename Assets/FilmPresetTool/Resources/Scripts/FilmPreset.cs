using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;
using UnityEngine.Rendering.Universal;

[ExecuteInEditMode]
public class FilmPreset : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] Volume volume;
    [SerializeField]
    FilmPresetType FilmType = FilmPresetType.None;

    [HideInInspector]
    public int arrayIndex = 0;
    [HideInInspector]
    private GameObject Resources;
    VolumeProfile KodakGold;
    VolumeProfile KodakTMax;
    VolumeProfile FujiPro;
    VolumeProfile IlfordHP5;
    private VolumeProfile _None;
    private VolumeProfile _Custom;

    private ColorAdjustments CA;
    private float currentPostExposure;
    private float currentContrast;
    private float r;
    private float g;
    private float b;
    private float currentHueShift;
    private float currentSaturation;
    private Color currentColor;





    private void Awake()
    {
        volume = gameObject.GetComponent<Volume>();
        
        KodakGold = (VolumeProfile)AssetDatabase.LoadAssetAtPath("Assets/FilmPresetTool/Resources/FilmStockProfiles/KodakGold200.asset", typeof(VolumeProfile));
        KodakTMax = (VolumeProfile)AssetDatabase.LoadAssetAtPath("Assets/FilmPresetTool/Resources/FilmStockProfiles/KodaktMax400.asset", typeof(VolumeProfile));
        FujiPro = (VolumeProfile)AssetDatabase.LoadAssetAtPath("Assets/FilmPresetTool/Resources/FilmStockProfiles/FujiPro400H.asset", typeof(VolumeProfile));
        IlfordHP5 = (VolumeProfile)AssetDatabase.LoadAssetAtPath("Assets/FilmPresetTool/Resources/FilmStockProfiles/IlfordHP5400.asset", typeof(VolumeProfile));
        _None = (VolumeProfile)AssetDatabase.LoadAssetAtPath("Assets/FilmPresetTool/Resources/FilmStockProfiles/None.asset", typeof(VolumeProfile));
        _Custom = (VolumeProfile)AssetDatabase.LoadAssetAtPath("Assets/FilmPresetTool/Resources/FilmStockProfiles/Custom.asset", typeof(VolumeProfile));

    }

    void Update()
    {
        if (FilmType == FilmPresetType.KodakGold200)
        {
            volume.profile = KodakGold;
            
        }

        if (FilmType == FilmPresetType.KodakTMax400)
        {
            volume.profile = KodakTMax;
            
        }

        if (FilmType == FilmPresetType.FujiPro400h)
        {
            volume.profile = FujiPro;
          
        }

        if (FilmType == FilmPresetType.IlfordHP5400)
        {
            volume.profile = IlfordHP5;
          
        }

        if(FilmType == FilmPresetType.None)
        {
            volume.profile = _None;
           
        }
        
        if(FilmType == FilmPresetType.Custom)
        {
            volume.profile = _Custom;

            if (volume.profile.TryGet<ColorAdjustments>(out CA))
            {

            SetCustom();

            }
        }
    }

    public void SettoNew()
    {
        FilmType = FilmPresetType.None;
        volume.profile = _None;
        
    }

    public void SaveCustom()
    {

        if(volume.profile.TryGet<ColorAdjustments>(out CA))
        {
            currentPostExposure = CA.postExposure.value;
            PlayerPrefs.SetFloat("PostExposure", currentPostExposure);
            Debug.Log(currentPostExposure);

            currentContrast = CA.contrast.value;
            PlayerPrefs.SetFloat("Contrast", currentContrast);

            currentHueShift = CA.hueShift.value;
            PlayerPrefs.SetFloat("HueShift", currentHueShift);

            currentSaturation = CA.saturation.value;
            PlayerPrefs.SetFloat("Saturation", currentSaturation);
            
            currentColor = (Color)CA.colorFilter;
            r = currentColor.r;
            g = currentColor.g;
            b = currentColor.b;
            PlayerPrefs.SetFloat("ColorRed", r);
            PlayerPrefs.SetFloat("ColorGreen", g);
            PlayerPrefs.SetFloat("ColorBlue", b);

            PlayerPrefs.Save();

            CA.postExposure.value = 0;
            CA.contrast.value = 0;
            CA.hueShift.value = 0;
            CA.saturation.value = 0;
            CA.colorFilter.value = Color.white;
        }
       
        
    }

    public void SetCustom()
    {
        currentPostExposure = PlayerPrefs.GetFloat("PostExposure");
        CA.postExposure.value = currentPostExposure;

        currentContrast = PlayerPrefs.GetFloat("Contrast");
        CA.contrast.value = currentContrast;

        currentHueShift = PlayerPrefs.GetFloat("HueShift");
        CA.hueShift.value = currentHueShift;

        currentSaturation = PlayerPrefs.GetFloat("Saturation");
        CA.saturation.value = currentSaturation;

        r = PlayerPrefs.GetFloat("ColorRed");
        g = PlayerPrefs.GetFloat("ColorGreen");
        b = PlayerPrefs.GetFloat("ColorBlue");

        currentColor.r = r;
        currentColor.g = g;
        currentColor.b = b;

        CA.colorFilter.value = currentColor;
    }
}
