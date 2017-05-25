using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour {

    //MainMenu
    private GameObject mainMenu;

    //UI Components
    public Dropdown resolutionDropdown;

    //Screen Resolution Variables
    private bool fullscreen;
    private int[] width;
    private int[] height;

	// Use this for initialization
	void Start ()
    {
        mainMenu = GameObject.Find("MainMenu");
    }

    public void changeFullScreen()
    {
        fullscreen = !fullscreen;
    }

    public void setResolution()
    {
        int resolution = resolutionDropdown.value;
        Screen.SetResolution(width[resolution], height[resolution], fullscreen);
    }
	
	public void closeOptionsMenu()
    {
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
