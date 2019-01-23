// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class ColorManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    [Header("YRBK")]
    public Color[] colors;


    // private


    // references
    public static ColorManager instance;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        instance = this;
    }

    void Start () {
        
	}
	
	void Update () {
        
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public Color Char2Color(char c) {
        int idx = Char2Int(c);
        if (0 <= idx && idx < colors.Length) return colors[idx];
        return Color.magenta;
    }

    public int Char2Int(char c) {
        switch (c) {
            case 'y':return 0;
            case 'r':return 1;
            case 'b':return 2;
            case 'k':return 3;
            default: return -1;
        }
    }



    // queries



    // other

}