// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class ColorManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    [Header("YRBK GPS- 0123")]
    public Color[] colors;

    Material[] materials;

    readonly char[] colorChar = new char[] {
        'y', 'r', 'b', 'k',
        'g', 'p', 's', '-',
        '0', '1', '2', '3'};


    // private


    // references
    public Texture2D colorTexture;
    public Material baseMaterial;
    public static ColorManager instance;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        instance = this;

    }

    void Start () {
        CreateMaterials();
	}
	
	void Update () {
        
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    [ContextMenu("Get Colors from Texture")]
    void SetupColors() {
        colors = colorTexture.GetPixels();
    }

    void CreateMaterials() {
        materials = new Material[colors.Length];
        for (int i = 0; i < materials.Length; i++) {
            materials[i] = Instantiate(baseMaterial);
            materials[i].color = colors[i];
        }
    }

    public Color Char2Color(char c) {
        int idx = Char2Int(c);
        if (0 <= idx && idx < colors.Length) return colors[idx];
        return Color.magenta;
    }

    public Material Char2Material(char c) {
        int idx = Char2Int(c);
        if (0 <= idx && idx < colors.Length) return materials[idx];
        return null;
    }

    public Color PlayerColor(int id) {
        int idx = id + 8;
        if (0 <= idx && idx < colors.Length) return colors[idx];
        return Color.magenta;
    }

    public int Char2Int(char c) {
        int idx = System.Array.FindIndex(colorChar, x => x == c);
        Debug.Assert(idx != -1, "invalid color char: " + c);
        return idx;
    }



    // queries



    // other

}