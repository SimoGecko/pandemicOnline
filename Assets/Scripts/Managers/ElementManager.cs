// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class ElementManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public


    // private


    public static ElementManager instance;
    // references

    [Header("UI")]
    public RectTransform cardUIPrefab;


    [Header("Objects")]
    public BoardPlacement stationPlacement;
    public BoardPlacement[] diseasePlacement, curePlacement;

    [Header("Pieces")]
    public DiseaseCube diseaseCubePrefab;
    public CureMarker cureMarkerPrefab;
    public Marker infectionMarkerPrefab, outbreakMarkerPrefab;
    public ResearchStation stationPrefab;
    public Pawn pawnPrefab;



    // --------------------- BASE METHODS ------------------
    private void Awake() {
        if (instance != null) Destroy(this);
        instance = this;
    }

    void Start() {

    }

    void Update() {

    }

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public T Copy<T>(T prefab) where T : Component {
        return Instantiate(prefab) as T;
    }



    // queries



    // other

}