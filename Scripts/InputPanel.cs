using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPanel : MonoBehaviour {

    public Text inputField;

    private bool finishedInput = false;

    public void setFinishedInput(bool finished)
    {
        finishedInput = finished;
    }

    public bool isFinishedInput()
    {
        return finishedInput;
    }

    //Resets the panel, and then disables it
    public void disable()
    {
        inputField.text = "";
        finishedInput = false;
        this.gameObject.SetActive(false);
    }
}
