// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

////////// takes a graph and instantiates vertices and edges from prefabs //////////

public class GraphVisualizer : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    const float longEdgeLength = 6;

    // private

    // references
    GameObject parent;
    [Header("Prefabs")]
    public GameObject vertexPrefab;
    public GameObject edgePrefab;

    // --------------------- BASE METHODS ------------------
    void Start () {
        
	}
	
	void Update () {
        
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public void GenerateGraph(Graph g) {

        //clean previous one
        if (parent != null) {
            Destroy(parent);
        }
        parent = new GameObject("graph parent");

        //instantiate vertices
        foreach(Vertex v in g.Vertices) {
            GameObject newVertex = PrefabUtility.InstantiatePrefab(vertexPrefab) as GameObject;
            newVertex.transform.parent = parent.transform;
            newVertex.transform.position = v.Position;

            //set right color
            ColorManager cm = ColorManager.instance;
            if (ColorManager.instance == null) cm = transform.root.GetComponentInChildren<ColorManager>();

            newVertex.GetComponentInChildren<MeshRenderer>().material = cm.Char2Material(v.color);

            //set name
            TextMeshProUGUI tmp = newVertex.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>();
            if (tmp == null) Debug.LogError("tmp not found");
            tmp.text = v.name.ToUpper();
        }

        //instantiate edges
        foreach (Vertex v in g.Vertices) {
            foreach(Vertex u in g.Outgoing(v)) {
                if (u.id < v.id) {
                    GameObject newEdge = PrefabUtility.InstantiatePrefab(edgePrefab) as GameObject;
                    //GameObject newEdge = Instantiate(edgePrefab, parent.transform);
                    newEdge.transform.parent = parent.transform;

                    newEdge.transform.position = (u.Position + v.Position)/2;

                    Vector3 delta = u.Position - v.Position;
                    newEdge.transform.localScale = new Vector3(delta.magnitude, 1, 1);
                    if (delta.magnitude > longEdgeLength) {
                        newEdge.SetActive(false); // hide the long ones going through the map
                    }

                    float angle = Mathf.Atan2(delta.z, delta.x)*Mathf.Rad2Deg;
                    newEdge.transform.eulerAngles = new Vector3(0, -angle, 0);
                }
            }
        }
    }



    // queries



    // other

}