using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    int [,] map;
    
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(map == null) map = GameObject.Find("Map Generator").GetComponent<MapGenerator>().getMap();
        Debug.Log(map[0,0]);
        
    }
}
