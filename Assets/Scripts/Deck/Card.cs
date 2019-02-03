// (c) Simone Guggiari 2018

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// generic card class //////////

public class Card : IComparable, IElement {

    public int id;
    public char color;
    public string Nid { get; private set; }


    //------------------
    public Card() { }

    public Card(int id, char color, string Nid) {
        this.id = id;
        this.color = color;
        this.Nid = Nid;
    }

    public Card(Card other) {
        this.id = other.id;
        this.color = other.color;
        this.Nid = other.Nid; // attention: copying nid
    }

    //------------------

    public int CompareTo(object obj) {
        Card c = (Card)obj;
        return id.CompareTo(c.id);
    }
}