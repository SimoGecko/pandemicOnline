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
        //Setup();

        PlayerManager.instance.StartTurn();
    }
	
	void Update () {
        GameManager.instance.CheckEndCondition();
        //DEAL WITH TURNS AND CALLS

	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    void CreateAllObjects() {
        //BOARD
        //Board.instance.Setup();
        Board.instance.GenerateBoardGraph();

        //DISEASE
        //DiseaseManager.instance.Setup();
        DiseaseManager.instance.SetupMarkers();
        DiseaseManager.instance.CreateDiseases();
        DiseaseManager.instance.CreateDecks();

        //RESEARCH
        ResearchManager.instance.Setup();

        //PlAYERS
        PlayerManager.instance.Setup();

        //GAME
        GameManager.instance.Setup();

        //UI
        InterfaceManager.instance.Setup();


        //GAME RELEVANT
    }

    void GameStart() {
        //makes things happen in the game
        DiseaseManager.instance.BaseInfect();

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