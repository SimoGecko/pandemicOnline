// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

////////// DESCRIPTION //////////

public class GraphVisualizer : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public


    // private

    // references
    GameObject parent;
    public GameObject vertexPrefab, edgePrefab;


    // --------------------- BASE METHODS ------------------
    void Start () {
        
	}
	
	void Update () {
        
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public void GenerateGraph(Graph g) {
        ColorManager colorManager = GetComponent<ColorManager>();

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
            newVertex.GetComponentInChildren<MeshRenderer>().material.color = colorManager.Char2Color(v.color);

            //instantiate name
            TextMeshProUGUI tmp = newVertex.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>();
            if (tmp == null) Debug.LogError("tmp not found");
            tmp.text = v.name;
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

                    float angle = Mathf.Atan2(delta.z, delta.x)*Mathf.Rad2Deg;
                    newEdge.transform.eulerAngles = new Vector3(0, -angle, 0);
                }
            }
        }
    }



    // queries



    // other

}