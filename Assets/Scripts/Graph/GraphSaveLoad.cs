// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// saves and loads graph to/from text file //////////

public class GraphSaveLoad : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    public bool allowSaveLoad = false;
    public const string defaultSavePath = "Assets/Text/graph.txt";


    // private


    // references
    GraphBuilder builder;


    // --------------------- BASE METHODS ------------------
    void Start() {
        builder = GetComponent<GraphBuilder>();
    }

    void Update() {
        if (allowSaveLoad) {
            if (Input.GetKeyDown(KeyCode.S)) SaveToText(builder.BuiltGraph, defaultSavePath);
            if (Input.GetKeyDown(KeyCode.L)) LoadAndDisplayGraphFromFile();
        }
    }

    // --------------------- CUSTOM METHODS ----------------


    // commands
    [ContextMenu("Create graph from file")]
    void LoadAndDisplayGraphFromFile() {
        Graph loadedGraph = LoadGraphFromFile(defaultSavePath);
        GetComponent<GraphVisualizer>().GenerateGraph(loadedGraph);
    }





    public static void SaveToText(Graph graphToSave, string savePath = defaultSavePath) {
        string result = string.Format("{0},{1},{2}\n", "graphName", graphToSave.NumVertices, graphToSave.NumEdges);

        //save vertices
        foreach (Vertex v in graphToSave.Vertices) {
            result += Utility.Serialize(v) + "\n";
        }
        //save edges
        foreach (Vertex v in graphToSave.Vertices) {
            foreach (Vertex u in graphToSave.Outgoing(v)) {
                if (u.id < v.id) {
                    result += string.Format("{0},{1}\n", u.id, v.id);
                }
            }
        }
        //store to file
        System.IO.File.WriteAllText(savePath, result);
    }

    public static Graph LoadGraph(string graph_encoding, bool useCity = false) {
        string[] lines = graph_encoding.GetLines();//graph_encoding.Split(new[] { System.Environment.NewLine, "\n" }, System.StringSplitOptions.RemoveEmptyEntries);

        Graph newGraph = new Graph();
        var vals = lines[0].Split(',');
        //string graphName = vals[0];
        int n = int.Parse(vals[1]);
        int m = int.Parse(vals[2]);

        for (int i = 1; i < 1 + n; i++) {
            //read vertex
            Vertex v = Utility.Deserialize<Vertex>(lines[i]);

            //use city instead
            if (useCity) {
                City c = new City(v);
                newGraph.AddVertex(c);
            } else {
                newGraph.AddVertex(v);
            }

        }
        for (int j = 1 + n; j < 1 + n + m; j++) {
            //read edge
            vals = lines[j].Split(',');
            int u = int.Parse(vals[0]);
            int v = int.Parse(vals[1]);
            newGraph.AddEdge(u, v);
        }
        return newGraph;
    }

    public static Graph LoadGraphFromFile(string savePath = defaultSavePath, bool useCity = false) {
        string text = System.IO.File.ReadAllText(savePath);
        return LoadGraph(text, useCity);
    }


    // queries



    // other

}