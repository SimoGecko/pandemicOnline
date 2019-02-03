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

    void Start() {

    }

    void Update() {
        if (GameManager.instance.Playing) GameUpdate();
    }

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public void GameSetup() {
        //BOARD
        Board.instance.LoadBoardGraph();
        Board.instance.ReadCitiesNames();
        Board.instance.GenerateBoardGraph();

        //DISEASE
        DiseaseManager.instance.SetupMarkers();
        DiseaseManager.instance.CreateDiseases();
        DiseaseManager.instance.CreateDecks();

        //RESEARCH
        ResearchManager.instance.CreateStations();
        ResearchManager.instance.PlaceStartingStation();

        //PlAYERS
        PlayerManager.instance.Setup();

        //GAME
        //GameManager.instance.Setup();

        //END
        EndManager.instance.CreateWinLoseConditions();

        //UI
        InterfaceManager.instance.LinkEverything();


        //GAME RELEVANT
    }

    public void GameStart() {
        //makes things happen in the game
        DiseaseManager.instance.BaseInfect();

        PlayerManager.instance.StartTurn();

    }


    void GameUpdate() {
        //DEAL WITH TURNS AND CALLS

        EndManager.instance.CheckEndCondition();

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