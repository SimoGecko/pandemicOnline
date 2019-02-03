// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// general graph class with vertex and edges, adding and queries //////////

public class Graph {
    // --------------------- VARIABLES ---------------------

    // public


    // private
    bool directed;

    List<Vertex> vertices;
    Dictionary<Vertex, List<Vertex>> edges;

    // references



    // --------------------- CUSTOM METHODS ----------------


    // commands
    public Graph(bool directed = false) {
        vertices = new List<Vertex>();
        edges = new Dictionary<Vertex, List<Vertex>>();
        this.directed = directed;
        NumEdges = 0;
    }

    public void AddVertex(Vertex v) {
        Debug.Assert(v != null, "No null vertex allowed");
        Debug.Assert(!vertices.Contains(v), "No vertex duplicate allowed");

        vertices.Add(v);
        edges.Add(v, new List<Vertex>());
    }

    public void AddVertex(Vector3 p) {
        Vertex v = new Vertex(NumVertices, p);
        AddVertex(v);
    }

    public void AddEdge(Vertex u, Vertex v) {
        Debug.Assert(u != null && v != null, "No null vertex allowed");
        Debug.Assert(vertices.Contains(u) && vertices.Contains(v), "vertices must be in graph");

        edges[u].Add(v);
        if (!directed) edges[v].Add(u);
        NumEdges++;
    }

    public void AddEdge(int u, int v) {
        Debug.Assert(u < NumVertices && v < NumVertices, "Vertex index out of range");

        AddEdge(vertices[u], vertices[v]);
    }




    // queries
    public int NumVertices { get { return vertices.Count; } }
    public int NumEdges { get; private set; }

    public List<Vertex> Vertices { get { return vertices; } }

    public List<Vertex> Outgoing(Vertex v) {
        Debug.Assert(vertices.Contains(v), "V must be present");
        return edges[v];
    }

    public bool AreAdjacent(Vertex u, Vertex v) {
        return edges[u].Contains(v);
    }

    // other

}