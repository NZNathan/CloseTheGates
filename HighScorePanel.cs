using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScorePanel : MonoBehaviour {

    public ScoreRank[] scoresUI;
    private ScoreProfile[] scoreProfiles;

    // Use this for initialization
    void Start()
    {
        scoreProfiles = new ScoreProfile[scoresUI.Length];
    }

    // Update is called once per frame
    void Update()
    {

    }

    private class ScoreProfile
    {
        private string name;
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
}
