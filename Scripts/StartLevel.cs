using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLevel : MonoBehaviour {

    public PlayerMovement player;
    public Text levelIntroText;

	// Use this for initialization
	void Start ()
    {
        Invoke("startLevel", 2f);
	}

    public void startLevel()
    {
        player.setRunning(true);
        levelIntroText.gameObject.SetActive(false);
        Timer.instance.setTimerRunning(true);
    }

}
