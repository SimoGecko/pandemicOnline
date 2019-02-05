// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class Pawn : BoardPiece {
    // --------------------- VARIABLES ---------------------

    // public


    // private


    // references


    // --------------------- BASE METHODS ------------------
    void Start() {

    }

    protected override void Update() {
        base.Update();

    }

    // --------------------- CUSTOM METHODS ----------------


    // commands
    /*
    public void Setup(Player p, char color) {
        base.SetColor(color);
        owner = p;
    }
   
    public override void MoveToCity(City city) {
        if (CurrentCity != null) CurrentCity.RemovePlayer(owner);
        base.MoveToCity(city);
        if (CurrentCity != null) CurrentCity.AddPlayer(owner);
    }*/


    // queries
    


    // other

}