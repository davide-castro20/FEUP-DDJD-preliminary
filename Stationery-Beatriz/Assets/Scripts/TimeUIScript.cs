using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUIScript : MonoBehaviour
{
    private TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        var currentTime = GameData.TimePassed;
        
        var minutes = ((int)(currentTime / 60)).ToString();
        var seconds = ((int)(currentTime % 60)).ToString();

        if (minutes.Length == 1) minutes = "0" + minutes;
        if (seconds.Length == 1) seconds = "0" + seconds;
                
        _text.text = minutes + ":" + seconds;
    }
}
