// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class GraphBuilder : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    public bool addVertex;
    public bool addEdge;

    // private
    Graph graphToBuild;
    Vertex startV;

    // references
    GraphVisualizer visualizer;

    // --------------------- BASE METHODS ------------------
    void Start() {
        graphToBuild = new Graph();
        visualizer = GetComponent<GraphVisualizer>();
    }

    void Update() {
        if (addVertex) {
            if (Input.GetMouseButtonDown(0)) {
                graphToBuild.AddVertex(MousePos);
                visualizer.GenerateGraph(graphToBuild);
            }
        }
        if (addEdge) {
            if (Input.GetMouseButtonDown(0)) {
                startV = ClosestVertex(graphToBuild, MousePos);
            }
            if (Input.GetMouseButtonUp(0)) {
                Vertex endV = ClosestVertex(graphToBuild, MousePos);
                graphToBuild.AddEdge(startV, endV);
                visualizer.GenerateGraph(graphToBuild);
            }
        }
    }

    // --------------------- CUSTOM METHODS ----------------


    // commands

    // queries
    public Graph BuiltGraph { get { return graphToBuild; } }
    Vector3 MousePos { get { return Utility.CamRaycast(); } }

    Vertex ClosestVertex(Graph g, Vector3 point) {
        System.Func<Vertex, float> vDist = (v => Vector3.SqrMagnitude(v.Position - point));
        return Utility.FindMin(g.Vertices, vDist);
    }



    // other

}