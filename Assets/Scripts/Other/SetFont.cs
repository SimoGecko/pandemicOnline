// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

////////// DESCRIPTION //////////

public class SetFont : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public


    // private


    // references
    public TMP_FontAsset font;
    public Font textFont;
	
	
	// --------------------- BASE METHODS ------------------
	void Start () {
        
	}
	
	void Update () {
        
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    [ContextMenu("Change Font TMP")]
    void SetAllFont() {
        TextMeshProUGUI[] allTmp= FindObjectsOfType<TextMeshProUGUI>();
        foreach(var t in allTmp) {
            t.font = font;
        }
        Debug.Log("changed tmp font for " + allTmp.Length);
    }
    [ContextMenu("Change Font Text")]
    void SetAllFontText() {
        Text[] allTmp = FindObjectsOfType<Text>();
        foreach (var t in allTmp) {
            t.font = textFont;
        }
        Debug.Log("changed text font for " + allTmp.Length);

    }



    // queries



    // other

}