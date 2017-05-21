using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreProfile
{
    [SerializeField]
    private string name;
    [SerializeField]
    private float time;

    public ScoreProfile(string name, float time)
    {
        this.name = name;
        this.time = time;
    }

    public string getName()
    {
        return name;
    }

    public float getTime()
    {
        return time;
    }

}
