// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// general vertex class with id, name, pos, color //////////

public class Vertex {
    // --------------------- VARIABLES ---------------------
    const int precision = 2;

    public int id;
    public char color;
    public string name;
    public float posX, posY;

    //CONSTRUCTORS
    public Vertex() {  }

    public Vertex(int id, Vector3 pos) {
        this.id = id;
        name = "city";
        Position = pos;
        color = 'k';
    }

    public Vertex(Vertex other) { // copy constructor
        this.id = other.id;
        this.name = other.name;
        this.posX = other.posX;
        this.posY = other.posY;
        this.color = other.color;
    }

    public Vertex(int id, string name, float posX, float posY, char color) { // full constructor
        this.id = id;
        this.name = name;
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