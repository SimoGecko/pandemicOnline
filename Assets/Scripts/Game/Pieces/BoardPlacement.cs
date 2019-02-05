// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class BoardPlacement : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    public int rows = 6, cols = 1;
    public float offset = .08f;


    // private


    // references


    // --------------------- BASE METHODS ------------------
    void Start() {

    }

    void Update() {

    }

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public void Setup(int rows, int cols, float offset) {
        this.rows = rows;
        this.cols = cols;
        this.offset = offset;
    }



    // queries
    float Width { get { return cols * offset; } }
    float Height { get { return rows * offset; } }
    Vector3 Size { get { return new Vector3(Width, 0, Height); } }
    int MaxNum { get { return rows * cols; } }

    Vector3 GetPosByIndex(int r, int c) { return transform.position + new Vector3(c + .5f, 0, -(r + .5f)) * offset; }// Vector3.right * () * offset + Vector3.down * () * offset; }

    public Vector3 GetPos(int i) {
        Debug.Assert(i < MaxNum, "Index out of range for board placement");
        return GetPosByIndex(i / cols, i % cols);
    }

    // other
    private void OnDrawGizmos() {
        Gizmos.color = Color.white;// Color.Lerp(Color.blue, Color.red, .5f);

        Gizmos.DrawWireCube(transform.position + new Vector3(Width, 0, -Height) / 2, Size);

        for (int i = 0; i < MaxNum; i++) {
            Gizmos.DrawWireSphere(GetPos(i), .01f);

        }
    }

}