// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class Player : MonoBehaviour, IElement {
    // --------------------- VARIABLES ---------------------

    // public


    // private
    public string Nid { get; set; }


    // references


    // --------------------- BASE METHODS ------------------
    void Start () {
        
	}
	
	void Update () {
        
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public void Move(string cityNid) {

    }



    // queries
    public City CurrentCity {
        get {
           return null;
        }
    }

    public bool HasCard(string cardNid) {
        return false;
    }

    public void Discard(string cardNid) {

    }



    // other

}