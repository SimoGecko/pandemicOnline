// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class DiseaseCube : BoardPiece {
    // --------------------- VARIABLES ---------------------

    // public


    // private
    public Disease DiseaseParent { get; private set; }

    // references
	
	
	// --------------------- BASE METHODS ------------------
	void Start () {
        
	}
	
	void Update () {
        
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public void Setup(Disease parent, char color) {
        DiseaseParent = parent;
        GetComponentInChildren<MeshRenderer>().material = ColorManager.instance.Char2Material(color);
    }



    // queries



    // other

}