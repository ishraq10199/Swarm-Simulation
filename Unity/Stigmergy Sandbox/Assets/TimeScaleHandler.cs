using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0, 5)]
    public float timeScale;
    void Start()
    {
        Time.timeScale = timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeScale;
    }
}
