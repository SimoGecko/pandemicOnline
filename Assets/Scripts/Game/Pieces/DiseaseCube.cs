// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class DiseaseCube : BoardPiece {
    // --------------------- VARIABLES ---------------------

    // public
    float timeToRevolution = 6f;
    public float dist = .15f;

    // private
    public Disease DiseaseParent { get; private set; }
    int ith;

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
    public void Setup(Disease parent) {
        DiseaseParent = parent;
        SetColor(parent.Color);
        MoveAway();
    }


    public void SetIth(int i) {
        ith = i;
    }

    Vector3 Offset() {
        if (CurrentCity == null) return Vector3.zero;

        float rot = (360f / 3 * ith) + 360 * (Time.time / timeToRevolution) % 360;
        return (Quaternion.Euler(0, rot, 0) * Vector3.forward) * dist;
    }



    // queries




    // other

}