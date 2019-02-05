// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

////////// generic city with name and list of diseases //////////

public class City : Vertex, IElement {
    // --------------------- VARIABLES ---------------------

    // public
    public string Nid { get { return name; } }

    public string FullName { get; private set; }

    // private

    ResearchStation researchStation;
    List<Player> stationedPlayers;
    Dictionary<string, List<DiseaseCube>> diseaseCubes;

    // references

    //static
    static List<string> cityNidsTemp = new List<string>(); // temp



    // --------------------- CUSTOM METHODS ----------------


    // constructors
    public City(Vertex v) : base(v) { // copy constructor
        FullName = "";
        researchStation = null;
        diseaseCubes = new Dictionary<string, List<DiseaseCube>>();
        stationedPlayers = new List<Player>();

        Debug.Assert(!cityNidsTemp.Contains(Nid), "duplicate nid: " + Nid);
        cityNidsTemp.Add(Nid);
    }

    public void SetFullName(string s) {
        FullName = s;
    }



    //commands
    public void AddResearchStation(ResearchStation rs) {
        Debug.Assert(!HasResearchStation, "Cannot have more than one research station");
        researchStation = rs;
    }


    public void AddDisease(DiseaseCube dCube) {
        Debug.Assert(NumTotalDiseaseCubes < 3, "Cannot add cube as this should cause outbreak and not extra cube");
        string dNid = dCube.DiseaseParent.Nid;

        if (!diseaseCubes.ContainsKey(dNid)) {
            diseaseCubes.Add(dNid, new List<DiseaseCube>());
        }

        diseaseCubes[dNid].Add(dCube);
    }

    public DiseaseCube RemoveDisease(string diseaseNid) {
        Debug.Assert(HasDisease(diseaseNid), "doesn't have disease to remove");

        int lastIdx = diseaseCubes[diseaseNid].Count - 1;
        DiseaseCube cube = diseaseCubes[diseaseNid][lastIdx];
        diseaseCubes[diseaseNid].RemoveAt(lastIdx);

        return cube;
    }

    public void AddPlayer(Player p) {
        stationedPlayers.Add(p);
    }
    public void RemovePlayer(Player p) {
        if (stationedPlayers.Contains(p)) {
            stationedPlayers.Remove(p);
        } else {
            Debug.LogError("stationed player not present at "+Nid);
        }
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
        return Board.instance.BoardGraph.Outgoing(this).Select(v => (City)v).ToList();
    }


    public int NumTotalDiseaseCubes { // for all diseases
        get {
            int result = 0;
            foreach (var v in diseaseCubes.Values) result += v.Count;
            return result;
        }
    }


    public int NumDiseaseCubes(string diseaseNid) {
        if (diseaseCubes.ContainsKey(diseaseNid)) return diseaseCubes[diseaseNid].Count;
        return 0;
    }

    public bool HasDisease(string diseaseNid) {
        return NumDiseaseCubes(diseaseNid) > 0;
    }

    public Disease DefaultDisease { get { return DiseaseManager.instance.GetDisease(color.ToString()); } }


    // other
    public static City Get(string cityNid) { return Board.instance.GetCity(cityNid); }


}