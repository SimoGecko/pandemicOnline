// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class Vertex {
    // --------------------- VARIABLES ---------------------
    const int precision = 2;

    public int id;
    public char color;
    public string Nid { get; set; }
    public float posX, posY;

    //CONSTRUCTORS
    public Vertex() {  }

    public Vertex(int id, Vector3 pos) {
        this.id = id;
        Nid = "city";
        Position = pos;
        color = 'k';
    }

    public Vertex(Vertex other) { // copy constructor
        this.id = other.id;
        this.Nid = other.Nid;
        this.posX = other.posX;
        this.posY = other.posY;
        this.color = other.color;
    }

    public Vertex(int id, string Nid, float posX, float posY, char color) { // full constructor
        this.id = id;
        this.Nid = Nid;
        this.posX = posX;
        this.posY = posY;
        this.color = color;
    }

    //property
    public Vector3 Position {
        get {
            return new Vector3(posX, 0, posY);
        }
        set {
            posX = (float)System.Math.Round(value.x, precision);
            posY = (float)System.Math.Round(value.z, precision);
        }
    }
}