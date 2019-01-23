// (c) Simone Guggiari 2018

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class Card : IComparable, IElement {
    public int id;
    public char color;
    public string Nid { get; set; }

    public Card() { }

    public Card(int id, char color, string Nid) {
        this.id = id;
        this.color = color;
        this.Nid = Nid;
    }

    public int CompareTo(object obj) {
        Card c = (Card)obj;
        return id.CompareTo(c.id);
    }
}