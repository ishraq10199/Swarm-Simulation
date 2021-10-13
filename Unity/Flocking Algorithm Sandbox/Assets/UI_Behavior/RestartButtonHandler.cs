using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestartButtonHandler : MonoBehaviour
{
    public Button restartButton;
    public Flock[] flocks;
    public TMP_InputField inputField;
    public TMP_InputField seedInput;


    // Start is called before the first frame update
    void Start()
    {
        Button btn = restartButton.GetComponent<Button>();
        btn.onClick.AddListener(RestartSimulation);

    }

    void RestartSimulation()
    {
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
        mg.Randomize();

        CameraScaling cam = GameObject.Find("Main Camera").GetComponent<CameraScaling>();
        cam.Reposition();

        foreach(Flock f in flocks)
        {
            f.Redeploy();
        }


    }
}
