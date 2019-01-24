// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class Disease : IElement {
    // --------------------- VARIABLES ---------------------

    public enum Status { nothing, cured, eradicated }

    // public
    const int numStartingCubes = 24;


    // private
    public string Nid { get; private set; }
    char color;
    public Status status { get; private set; }

    List<DiseaseCube> cubes;
    CureMarker cureMarker;

    public int NumAvailableCubes { get; private set; }
    public bool RunOutOfCubes { get; private set; }



    // references



    // --------------------- CUSTOM METHODS ----------------


    // commands
    public Disease(string Nid, char color) {
        this.Nid = Nid;
        this.color = color;
        RunOutOfCubes = false;

        status = Status.nothing;
    }

    public void Setup() {
        NumAvailableCubes = numStartingCubes;
        cubes = new List<DiseaseCube>();
        //instantiate cube prefabs
        for (int i = 0; i < numStartingCubes; i++) {
            DiseaseCube newCube = ElementManager.instance.Copy(ElementManager.instance.diseaseCubePrefab);
            cubes.Add(newCube);
            newCube.Setup(this);
            newCube.SetColor(color);
            newCube.MoveTo(Vector3.zero);
        }

        // setup cure marker
        cureMarker = ElementManager.instance.Copy(ElementManager.instance.cureMarkerPrefab);
        cureMarker.SetColor(color);
    }

    public void Infect(City city) {
        if (Eradicated) return; // cannot infect

        if (NumAvailableCubes <= 0) {
            RunOutOfCubes = true;
        } else {
            NumAvailableCubes--;
            DiseaseCube cube = cubes[NumDeployedCubes];
            //cubes.RemoveAt(0);

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
            //add cube back
        }
        if (NumDeployedCubes == 0 && Cured) Eradicate();
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
    public int NumDeployedCubes { get { return numStartingCubes - NumAvailableCubes; } }

    public static Disease Get(string diseaseNid) { return DiseaseManager.instance.GetDisease(diseaseNid); }

    public bool Cured { get { return status == Status.cured; } }
    public bool Eradicated { get { return status == Status.eradicated; } }
    public char Color { get { return color; } }
    // other

}