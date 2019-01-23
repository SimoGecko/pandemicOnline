// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class Disease : IElement {
    // --------------------- VARIABLES ---------------------

    public enum Status { nothing, cured, eradicated }

    // public
    const int startingCubes = 24;


    // private
    public string Nid { get; set; }
    char color;

    public Status status;
    int availableCubes;


    // references



    // --------------------- CUSTOM METHODS ----------------


    // commands
    public void Setup(string Nid, char color) {
        this.Nid = Nid;
        this.color = color;

        status = Status.nothing;
        availableCubes = startingCubes;

        //instantiate cube prefabs

    }

    public void Infect(City city, int num) {
        if (availableCubes < num) {
            //LOSE
        } else {
            //TODO
        }
    }

    public void Infect(string cityNid, int num) {
        Infect(City.Get(cityNid), num);
    }

    public void Treat(City city, int num) {
        //TODO
    }

    public void Treat(string cityNid, int num) {
        Treat(City.Get(cityNid), num);
    }

    public void Cure() { status = Status.cured; }
    public void Eradicate() {
        Debug.Assert(NumDeployedCubes == 0, "Should not be able to eradicate yet");
        status = Status.eradicated;
    }



    // queries
    public int NumDeployedCubes { get { return startingCubes - availableCubes; } }

    public static Disease Get(string diseaseNid) { return null; }

    // other

}