// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

////////// difficulty, win/lose conditions (should only control the play/pause/state of game) //////////

public class GameManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------
    public enum Difficulty { easy, medium, hard}
    public delegate bool EndCondition();

    // public
    public Difficulty difficulty;
    public bool DEBUG = true;

    // private
    List<EndCondition> winConditions;
    List<EndCondition> loseConditions;

    // references
    public static GameManager instance;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    void Start () {
        
	}
	
	void Update () {
        //CheckEnd
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public void Setup() {
        CreateWinLoseConditions();
    }

    void CreateWinLoseConditions() {
        winConditions = new List<EndCondition>();
        EndCondition allCured = () => DiseaseManager.instance.AllDiseases.Select(d => d.Cured).Aggregate((b1, b2) => b1 && b2);
        winConditions.Add(allCured);

        loseConditions = new List<EndCondition>();
        EndCondition noMorePlayerCards = () => PlayerManager.instance.RunOutOfCards;
        EndCondition noDiseaseCubes = () => DiseaseManager.instance.AllDiseases.Select(d => d.RunOutOfCubes).Aggregate((b1, b2) => b1 || b2);
        EndCondition tooManyOutbreaks = () => DiseaseManager.instance.OutbreakNum >= 8;

        loseConditions.Add(noMorePlayerCards);
        loseConditions.Add(noDiseaseCubes);
        loseConditions.Add(tooManyOutbreaks);
    }

    public void CheckEndCondition() {
        if (winConditions == null || loseConditions == null) return;
        foreach (var c in winConditions) if (c()) Win();
        foreach (var c in loseConditions) if (c()) Lose();
    }
    void Win() {
        Debug.Log("WIN!");
    }
    void Lose() {
        Debug.Log("Lose!");
    }



    // queries



    // other

}