using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {

    public HighScorePanel highScorePanel;

    // Use this for initialization
    void Start ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //Player has crossed finish line
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().setRunning(false);
            levelFinished();
        }
    }

    void levelFinished()
    {
        Timer.instance.setTimerRunning(false);
        highScorePanel.gameObject.SetActive(true);
        highScorePanel.StartCoroutine("setupHighScorePanel", Timer.instance.getTime());
    }
}
