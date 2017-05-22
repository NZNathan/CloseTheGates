using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRank : MonoBehaviour {

    public Text nameText;
    public Text timeText;

    private float time;

    void Start()
    {
        time =  float.Parse(timeText.text.Substring(0,2));
    }

    public void setDetials(string name, float time)
    {
        nameText.text = name;
        timeText.text = time.ToString("F2") + " seconds";

        this.time = time;
    }

    public float getTime()
    {
        return time;
    }
}
