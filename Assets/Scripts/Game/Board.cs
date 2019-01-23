// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

////////// DESCRIPTION //////////

public class Board : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public



    // private
    Dictionary<string, City> cityDic = new Dictionary<string, City>();

    public Graph BoardGraph { get; private set; }

    // references
    public static Board instance;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    void Start () {
        //Setup(); called from flow to avoid dataraces
	}
	
	void Update () {
        
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public void Setup() {
        BoardGraph = GraphSaveLoad.LoadGraphFromFile(useCity: true);
        GetComponent<GraphVisualizer>().GenerateGraph(BoardGraph);
        foreach(Vertex v in BoardGraph.Vertices) {
            cityDic.Add(v.Nid, (City)v);
        }
    }



    // queries
    public City GetCity(string Nid) {
        Debug.Assert(cityDic.ContainsKey(Nid), "No city exists in dictionary with Nid " + Nid);
        return cityDic[Nid];
    }

    public List<City> AllCities { get {
            return BoardGraph.Vertices.Select(v => (City)v).ToList();
    } }


    // other

}