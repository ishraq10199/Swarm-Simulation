using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AgentCountSliderHandler : MonoBehaviour
{
    public Flock[] flocks;
    public Slider agentCountSlider;

    public void Start()
    {
        agentCountSlider.wholeNumbers = true;
        agentCountSlider.maxValue = 250;
        agentCountSlider.minValue = 1;
        agentCountSlider.value = 125;
        agentCountSlider.onValueChanged.AddListener(delegate { ValueCheck(); });
    }
    public void ValueCheck()
    {
        foreach(Flock f in flocks)
        {
            f.startingCount = (int)agentCountSlider.value;
        }
    }

}
