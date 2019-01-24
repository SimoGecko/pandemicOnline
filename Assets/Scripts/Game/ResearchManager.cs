// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class ResearchManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    const int numStartingStations = 6;


    // private
    int numAvailableStations;
    List<ResearchStation> stations;


    // references
    public static ResearchManager instance;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    void Start () {
        
	}
	
	void Update () {
        
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public void Setup() {
        CreateStations();
        PlaceStartingStation();
    }

    void CreateStations() {
        //generate stations
        numAvailableStations = numStartingStations;
        stations = new List<ResearchStation>();
        for (int i = 0; i < numStartingStations; i++) {
            ResearchStation newStation = Instantiate(ElementManager.instance.stationPrefab) as ResearchStation;
            stations.Add(newStation);
            //newStation.Setup();
            newStation.MoveAway();
        }
    }

    void PlaceStartingStation() {
        PlaceStation(Board.instance.startingCity);
    }

    public void PlaceStation(string cityNid) {
        Debug.Assert(numAvailableStations > 0, "No additional research station available");
        numAvailableStations--;
        ResearchStation rs = stations[NumDeployedStations];

        City city = City.Get(cityNid);
        city.AddResearchStation(rs);
        rs.MoveTo(city);
    }



    // queries
    public int NumDeployedStations { get { return numStartingStations - numAvailableStations; } }



    // other

}