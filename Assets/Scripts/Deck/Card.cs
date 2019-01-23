// (c) Simone Guggiari 2018

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class Card : IComparable {
    public int id;
    public char color;
    public string name;

    public Card() { }

    public Card(int id, char color, string name) {
        this.id = id;
        this.color = color;
        this.name = name;
    }

    public int CompareTo(object obj) {
        Card c = (Card)obj;
        return id.CompareTo(c.id);
    }
}