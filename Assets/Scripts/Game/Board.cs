// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

////////// generates board at the beginning of the game //////////

public class Board : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    public string startingCity = "at"; // atlanta


    // private
    Dictionary<string, City> cityDic = new Dictionary<string, City>();

    public Graph BoardGraph { get; private set; }

    // references
    public static Board instance;

    public TextAsset graphText, citiesText;


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
        BoardGraph = GraphSaveLoad.LoadGraph(graphText.text, useCity: true);
        transform.root.GetComponentInChildren<GraphVisualizer>().GenerateGraph(BoardGraph);
        foreach(Vertex v in BoardGraph.Vertices) {
            cityDic.Add(v.name, (City)v);
        }
        ReadCitiesNames();
    }

    //read cities name
    void ReadCitiesNames() {
        string[] lines = citiesText.text.RemoveSpaceAndTabs().GetLines();
        for (int i = 0; i < lines.Length; i++) {
            string[] content = lines[i].Split(',');

            string id = content[0];
            string fullname = content[1];
            if (cityDic.ContainsKey(id)) {
                cityDic[id].SetFullName(fullname);
            } else {
                Debug.LogError("no city with nid " + id);
            }
        }
    }



    // queries
    public City GetCity(string Nid) {
        Debug.Assert(cityDic.ContainsKey(Nid), "No city exists in dictionary with Nid " + Nid);
        if (!cityDic.ContainsKey(Nid)) return null;
        return cityDic[Nid];
    }

    public List<City> AllCities { get {

            return BoardGraph.Vertices.Select(v => (City)v).ToList();
    } }


    // other

}