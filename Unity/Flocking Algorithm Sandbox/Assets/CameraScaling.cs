using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaling : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera;
    void Start()
    {
        GameObject mapGeneratorObject = GameObject.Find("Map Generator");
        MapGenerator mg = mapGeneratorObject.GetComponent<MapGenerator>();
        float scaleMultiplier = mg.scale;
        mainCamera.orthographic = true;
        mainCamera.orthographicSize = 31 * scaleMultiplier;
        mainCamera.transform.position = new Vector3(13f * scaleMultiplier, 45, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject mapGeneratorObject = GameObject.Find("Map Generator");
            MapGenerator mg = mapGeneratorObject.GetComponent<MapGenerator>();
            float scaleMultiplier = mg.scale;
            mainCamera.orthographic = true;
            mainCamera.orthographicSize = 31 * scaleMultiplier;
            mainCamera.transform.position = new Vector3(13f * scaleMultiplier, 45, 0);
        }


    }
}
