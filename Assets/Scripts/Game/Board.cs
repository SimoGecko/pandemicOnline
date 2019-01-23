// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class Board : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public



    // private


    // references
    public static Board instance;


    // --------------------- BASE METHODS ------------------
    void Start () {
        Setup();
	}
	
	void Update () {
        
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public void Setup() {
        Graph boardGraph = GraphSaveLoad.LoadGraphFromFile(useCity: false);
        Debug.Log("bgo0 " + boardGraph.NumVertices);
        GetComponent<GraphVisualizer>().GenerateGraph(boardGraph);
    }



    // queries



    // other

}