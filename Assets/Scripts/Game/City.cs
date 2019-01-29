// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

////////// DESCRIPTION //////////

public class City : Vertex, IElement {
    // --------------------- VARIABLES ---------------------

    // public
    public string Nid { get { return name; } }


    // private
    string fullName;
    ResearchStation researchStation;
    //Player[] stationedPlayers; // not needed
    Dictionary<string, List<DiseaseCube>> diseaseCubes;

    // references

    static List<string> cityNames = new List<string>();
	
	

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public City(Vertex v) : base(v) { // copy constructor
        researchStation = null;
        diseaseCubes = new Dictionary<string, List<DiseaseCube>>();
        Debug.Assert(!cityNames.Contains(Nid), "duplicate nid: " + Nid);
    }

    public void AddResearchStation(ResearchStation rs) {
        Debug.Assert(!HasResearchStation, "Cannot have more than one research station");
        researchStation = rs;
        //it should move there
    }



    public void AddDisease(DiseaseCube dCube) {
        Debug.Assert(NumDiseaseCubes < 3, "Cannot add cube as this should cause outbreak and not extra cube");
        string dNid = dCube.DiseaseParent.Nid;

        if (!diseaseCubes.ContainsKey(dNid))
            diseaseCubes.Add(dNid, new List<DiseaseCube>());

        diseaseCubes[dNid].Add(dCube);
    }

    public DiseaseCube RemoveDisease(string diseaseNid) {
        Debug.Assert(HasDisease(diseaseNid), "doesn't have disease to remove");
        DiseaseCube cube = diseaseCubes[diseaseNid][0];
        diseaseCubes[diseaseNid].RemoveAt(0);
        return cube;
    }

    public void SetFullName(string s) {
        fullName = s;
    }

    // queries
    public bool IsAdjacentTo(City c) {
        return Board.instance.BoardGraph.AreAdjacent(this, c);
    }
    public bool HasResearchStation {
        get {
            return researchStation != null;
        }
    }
    public List<City> AdjacentCities() {
        return Board.instance.BoardGraph.Outgoing(this).Select(v=>(City)v).ToList();
    }

    public static City Get(string cityNid) { return Board.instance.GetCity(cityNid); }

    public int NumDiseaseCubes {
        get {
            int result = 0;
            foreach (var v in diseaseCubes.Values) result += v.Count;
            return result;
        }
    }


    public int NumDisease(string diseaseNid) {
        if (diseaseCubes.ContainsKey(diseaseNid)) return diseaseCubes[diseaseNid].Count;
        return 0;
    }

    public bool HasDisease(string diseaseNid) {
        return NumDisease(diseaseNid) > 0;
    }

    // other

}