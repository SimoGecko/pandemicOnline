// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class ResearchStation : BoardPiece {
    // --------------------- VARIABLES ---------------------

    // public


    // private


    // references


    // --------------------- BASE METHODS ------------------
    void Start() {

    }

    protected override void Update() {
        base.Update();
        posOffset = Offset();
    }

    // --------------------- CUSTOM METHODS ----------------


    // commands



    // queries
    Vector3 Offset() {
        return Vector3.left * .2f;
    }


    // other

}