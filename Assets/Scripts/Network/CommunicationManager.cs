// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class CommunicationManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public


    // private
    //fName in the form "/folder/file.txt"
    //base folder: pandemic_online
    //string downloadedResult = "";

    // references
    public static CommunicationManager instance;


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
    



    // queries



    // other

}