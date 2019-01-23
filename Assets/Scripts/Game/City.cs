// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

////////// DESCRIPTION //////////

public class City : Vertex, IElement {
    // --------------------- VARIABLES ---------------------

    // public


    // private


    // references
    
	
	

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public City(Vertex v) : base(v) { // copy constructor

    }



    // queries
    public bool IsAdjacentTo(City c) {
        return Board.instance.BoardGraph.AreAdjacent(this, c);
    }
    public bool HasResearchStation() {
        return false;
    }
    public List<City> AdjacentCities() {
        return Board.instance.BoardGraph.Outgoing(this).Select(v=>(City)v).ToList();
    }

    public static City Get(string cityNid) { return Board.instance.GetCity(cityNid); }


    // other

}