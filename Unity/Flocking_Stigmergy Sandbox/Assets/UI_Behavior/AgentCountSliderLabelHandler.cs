using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AgentCountSliderLabelHandler : MonoBehaviour
{
    public Slider agentCountSlider;
    public TextMeshProUGUI sliderLabel;
    // Start is called before the first frame update
    void Start()
    {
        sliderLabel.text = "" + (int)agentCountSlider.value;
        agentCountSlider.onValueChanged.AddListener(delegate { updateSliderLabel(); });
    }

    public void updateSliderLabel()
    {
        sliderLabel.text = "" + (int)agentCountSlider.value;
    }


}
