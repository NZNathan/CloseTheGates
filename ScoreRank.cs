using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRank : MonoBehaviour {

    public Text nameText;
    public Text timeText;

	public void setDetials(string name, float time)
    {
        nameText.text = name;
        timeText.text = time.ToString("F2") + " seconds";
    }
}
