using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsBtn : MonoBehaviour {

    private GameObject mainMenu;
    private GameObject optionsMenu;

	// Use this for initialization
	void Start () {
        mainMenu = GameObject.Find("MainMenu");

        optionsMenu = GameObject.Find("OptionsMenu");
        optionsMenu.SetActive(false);
    }
	
	public void openOptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
}
