using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonHandler : MonoBehaviour
{
    public Button pauseButton;
    public TextMeshProUGUI textLabel;

    bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = pauseButton.GetComponent<Button>();
        btn.onClick.AddListener(Pause);
    }

    void Pause(){

        if (!isPaused)
        {
            isPaused = !isPaused;
            textLabel.text = "Resume";
            Time.timeScale = 0;
        }
        else
        {
            isPaused = !isPaused;
            textLabel.text = "Pause";
            Time.timeScale = 1;
        }
        

        
    
    }
}
