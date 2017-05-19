using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScorePanel : MonoBehaviour {

    public ScoreRank[] scoresUI;
    private ScoreProfile[] scoreProfiles;

    
    public InputPanel inputPanel;

    // Use this for initialization
    void Start()
    {
        scoreProfiles = new ScoreProfile[scoresUI.Length];
        intializeRanks();
        setUI();
        this.gameObject.SetActive(false);
        //Got to intialize the scoreProfiles at game launch
    }

    void intializeRanks()
    {
        for (int i = 0; i < scoresUI.Length; i++)
        {
            scoreProfiles[i] = new ScoreProfile(scoresUI[i].nameText.text, scoresUI[i].getTime());
        }
    }

    public IEnumerator setupHighScorePanel(float time)
    {

        int rank = checkIfHighScore(time);
        Debug.Log(rank);
        if(rank != -1)
        {
            inputPanel.gameObject.SetActive(true);

            yield return new WaitUntil(() => inputPanel.isFinishedInput());

            //Get the name of the profile and then remove the input panel
            string name = inputPanel.inputField.text;
            inputPanel.disable();

            ScoreProfile newProfile = new ScoreProfile(name, time);
            insertNewRank(newProfile, rank);

            setUI();
        }
    }

    //Returns -1 if score wasn't good enough to be a highscore, otherwise returns rank on highscore table
    int checkIfHighScore(float time)
    {
        int rank = 0;
        foreach(ScoreProfile scoreProfile in scoreProfiles)
        {
            Debug.Log("Your time: " + time +" Highscore Time: " + scoreProfile.getTime());
            if(scoreProfile.getTime() > time)
            {
                return rank;
            }
            rank++;
        }

        return -1;
    }

    //Takes a new score profile and inserts it at rank, shuffling all the highscore under it down 1
    void insertNewRank(ScoreProfile newHighScore, int rank)
    {
        for(int i = scoreProfiles.Length-1; i > rank; i--)
        {
            scoreProfiles[i] = scoreProfiles[i-1];
        }

        scoreProfiles[rank] = newHighScore;
    }

    void setUI()
    {
        for (int i = 0; i < scoreProfiles.Length; i++)
        {
            scoresUI[i].setDetials(scoreProfiles[i].getName(), scoreProfiles[i].getTime());
        }
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
