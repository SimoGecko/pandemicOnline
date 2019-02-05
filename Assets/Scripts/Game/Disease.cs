// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// represents a disease in  //////////

public class Disease : IElement {
    // --------------------- VARIABLES ---------------------

    public enum Status { nothing, cured, eradicated }
    const int numStartingCubes = 24;

    // public


    // private
    public string Nid { get; private set; }
    public char Color { get; private set; }

    public Status status { get; private set; }

    List<DiseaseCube> cubes;
    CureMarker cureMarker;
    BoardPlacement diseasePlacement, curePlacement;


    public int NumAvailableCubes { get; private set; }
    public bool RunOutOfCubes { get; private set; }



    // references



    // --------------------- CUSTOM METHODS ----------------


    // commands
    public Disease(string Nid, char color, BoardPlacement diseasePlacement, BoardPlacement curePlacement) {
        this.Nid = Nid;
        this.Color = color;

        status = Status.nothing;

        NumAvailableCubes = numStartingCubes;
        RunOutOfCubes = false;

        this.diseasePlacement = diseasePlacement;
        this.curePlacement = curePlacement;

        InstantiateCubesAndMarkers();
    }

    public void InstantiateCubesAndMarkers() {
        //instantiate cube prefabs
        cubes = new List<DiseaseCube>();
        for (int i = 0; i < numStartingCubes; i++) {
            DiseaseCube newCube = ElementManager.instance.Copy(ElementManager.instance.diseaseCubePrefab);
            cubes.Add(newCube);
            newCube.Setup(this);
            newCube.TeleportTo(diseasePlacement.GetPos(i));
        }

        // instantiate cure marker
        cureMarker = ElementManager.instance.Copy(ElementManager.instance.cureMarkerPrefab);
        cureMarker.SetColor(Color); // setup
        cureMarker.TeleportTo(curePlacement.GetPos(0));

    }


    //commands

    public void Infect(City city) {
        if (Eradicated) return; // cannot infect

        if (NumAvailableCubes <= 0) {
            RunOutOfCubes = true; // game over
        } else {
            Debug.Assert(0 <= NumDeployedCubes && NumDeployedCubes < numStartingCubes, "NumDeployedCubes out of range: " + NumDeployedCubes);
            if (NumAvailableCubes <= 0) return;

            NumAvailableCubes--;
            DiseaseCube cube = cubes[NumAvailableCubes];

            cube.SetIth(city.NumTotalDiseaseCubes);
            city.AddDisease(cube);
            cube.MoveToCity(city);
            cube.placed = true;
        }
    }

    public void Infect(string cityNid) {
        Infect(City.Get(cityNid));
    }

    public void Treat(City city) {
        Debug.Assert(city.HasDisease(Nid), "City doesn't have disease to be treated");

        int amount = Cured ? city.NumDiseaseCubes(Nid) : 1;
        for (int i = 0; i < amount; i++) {
            DiseaseCube cube = city.RemoveDisease(Nid);
            cube.MoveTo(diseasePlacement.GetPos(NumAvailableCubes));
            cube.placed = false;

            NumAvailableCubes++;
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

    public string StatusString() { return Nothing ? "-" : Cured ? "Cured" : "Eradicated"; }

    public bool Nothing { get { return status == Status.nothing; } }
    public bool Cured { get { return status == Status.cured; } }
    public bool Eradicated { get { return status == Status.eradicated; } }

    // other

    public static Disease Get(string diseaseNid) { return DiseaseManager.instance.GetDisease(diseaseNid); }


}