using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    public float closeTime = 20f;
    private float t = 0;

    private Vector3 openPos;
    private Vector3 closedPos;

	// Use this for initialization
	void Start ()
    {
        openPos = transform.position;
        closedPos = new Vector3(transform.position.x, transform.position.y - 5f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime / closeTime;
        transform.position = Vector3.Lerp(openPos, closedPos, t);
    }
}
