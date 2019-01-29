// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

////////// calls the appropriate functions -> all game is run from here //////////

public class FlowManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public


    // private




    // references
    public static FlowManager instance;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        instance = this;
    }

    void Start () {
        Setup();

        PlayerManager.instance.StartTurn();
    }
	
	void Update () {
        GameManager.instance.CheckEndCondition();
        //DEAL WITH TURNS AND CALLS

	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    void Setup() {
        Board.instance.Setup();
        DiseaseManager.instance.Setup();
        ResearchManager.instance.Setup();
        PlayerManager.instance.Setup();
        GameManager.instance.Setup();
        InterfaceManager.instance.Setup();
    }

    public void OnTurnEnd() {
        PlayerManager.instance.CurrentPlayer.Draw(2);
        DiseaseManager.instance.EndTurnInfect();
        PlayerManager.instance.IncreaseTurn();
        PlayerManager.instance.CurrentPlayer.StartTurn();
    }

    void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    

    // queries



    // other
    

}