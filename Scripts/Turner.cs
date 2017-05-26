using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turner : MonoBehaviour {

    public float angle;
    private Transform playerDir;

    private void Start()
    {
        playerDir = GameObject.Find("PlayerBaseDir").transform;
    }
    
    //Add lerp in?? make less jarring
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //other.transform.eulerAngles = new Vector3(0, angle, 0);
            playerDir.eulerAngles = new Vector3(0, angle, 0);
            other.GetComponent<PlayerMovement>().setYAngle(angle);
        }
    }
}
