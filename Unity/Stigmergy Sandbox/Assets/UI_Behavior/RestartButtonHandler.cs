using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButtonHandler : MonoBehaviour
{
    public Button restartButton;
    public TMP_InputField inputField;
    public TMP_InputField seedInput;

    public Flock[] flocks;


    // Start is called before the first frame update
    void Start()
    {
        Button btn = restartButton.GetComponent<Button>();
        btn.onClick.AddListener(RestartSimulation);

    }

    void RestartSimulation()
    {
        //Debug.Log("---------------------RESTART BUTTON EVENT----------------");
        float mapScale = 1f;
        MapGenerator mg = GameObject.Find("Map Generator").GetComponent<MapGenerator>();
        if(inputField.text.Length!=0) mapScale = float.Parse(inputField.text, CultureInfo.InvariantCulture.NumberFormat);
        if (seedInput.text.Length != 0)
        {
            mg.useRandomSeed = false;
            mg.seed = seedInput.text;
        }
        else
        {
            mg.useRandomSeed = true;
        }
        mg.scale = mapScale;
        //Debug.Log("-------------------MAP INPUT VALUES TAKEN------------------");
        CameraScaling cam = GameObject.Find("Main Camera").GetComponent<CameraScaling>();
        cam.Reposition();
        //Debug.Log("-------------------CAMERA REPOSITIONED------------------");
        mg.Randomize();
        //Debug.Log("-------------------MAP RANDOMIZED VALUES TAKEN------------------");
        for (int i = 0; i < flocks.Length; i++)
        {
            flocks[i].Clean();
            flocks[i].Deploy();
        }
        //Debug.Log("-------------------FLOCKS REDEPLOYED------------------");
    }
}
