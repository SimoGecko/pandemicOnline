// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

////////// DESCRIPTION //////////

public class EndManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    public delegate bool EndCondition();


    // private
    List<EndCondition> winConditions;
    List<EndCondition> loseConditions;

    // references
    public static EndManager instance;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        if (instance != null) Destroy(this);
        instance = this;
    }

    void Start () {
        
	}
	
	void Update () {
        
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    /*
    public void Setup() {
        CreateWinLoseConditions();
    }*/

    public void CreateWinLoseConditions() {
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
        Debug.Log("===== W I N ! =====");
        GameManager.instance.SetState (GameManager.State.end);
    }
    void Lose() {
        Debug.Log("===== L O S E ! =====");
        GameManager.instance.SetState(GameManager.State.end);
    }


    // queries



    // other

}