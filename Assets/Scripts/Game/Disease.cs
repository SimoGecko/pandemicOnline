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
    int availableCubes;
    List<DiseaseCube> cubes;

    public Status status { get; private set; }


    // references



    // --------------------- CUSTOM METHODS ----------------


    // commands
    public Disease(string Nid, char color) {
        this.Nid = Nid;
        this.color = color;

        status = Status.nothing;
        availableCubes = startingCubes;
    }

    public void Setup() {
        cubes = new List<DiseaseCube>();
        //instantiate cube prefabs
        for (int i = 0; i < startingCubes; i++) {
            DiseaseCube newCube = DiseaseManager.instance.DiseaseCubeCopy();
            cubes.Add(newCube);
            newCube.Setup(this, color);
            newCube.MoveTo(Vector3.zero);
        }
    }

    public void Infect(City city) {
        if (Eradicated) return; // cannot infect

        if (availableCubes <= 0) {
            GameManager.instance.Lose("No more disease cubes");
        } else {
            availableCubes--;
            DiseaseCube cube = cubes[0];
            cubes.RemoveAt(0);

            city.AddDisease(cube);
            cube.MoveTo(city);
        }
    }

    public void Infect(string cityNid) {
        Infect(City.Get(cityNid));
    }

    public void Treat(City city) {
        Debug.Assert(city.HasDisease(Nid), "City doesn't have disease to be treated");
        int amount = Cured ? city.NumDisease(Nid) : 1;
        for (int i = 0; i < amount; i++) {
            DiseaseCube cube = city.RemoveDisease(Nid);
            cube.MoveTo(Vector3.zero);
        }
    }

    public void Treat(string cityNid) {
        Treat(City.Get(cityNid));
    }

    public void Cure() { status = Status.cured; }
    public void Eradicate() {
        Debug.Assert(NumDeployedCubes == 0, "Should not be able to eradicate yet");
        status = Status.eradicated;
    }



    // queries
    public int NumDeployedCubes { get { return startingCubes - availableCubes; } }

    public static Disease Get(string diseaseNid) { return DiseaseManager.instance.GetDisease(diseaseNid); }

    public bool Cured { get { return status == Status.cured; } }
    public bool Eradicated { get { return status == Status.eradicated; } }

    // other

}