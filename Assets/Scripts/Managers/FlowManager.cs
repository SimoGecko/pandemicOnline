// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

////////// calls the appropriate functions //////////

public class FlowManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public


    // private
    



    // references


    // --------------------- BASE METHODS ------------------
    void Start () {
        Setup();

    }
	
	void Update () {
        GameManager.instance.CheckEndCondition();
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    void Setup() {
        Board.instance.Setup();
        DiseaseManager.instance.Setup();
        ResearchManager.instance.Setup();
        PlayerManager.instance.Setup();
        GameManager.instance.Setup();
    }

    void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    

    // queries



    // other

}