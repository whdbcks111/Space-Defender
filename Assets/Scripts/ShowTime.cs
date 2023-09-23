using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShowTime : MonoBehaviour
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
        int min = (int)(GameManager.instance.playTime / 60f);
        int sec = (int)GameManager.instance.playTime % 60;
        _text.SetText(string.Format("{0:00}:{1:00}", min, sec));
    }
}
