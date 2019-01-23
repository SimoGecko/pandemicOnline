// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public abstract class Action {
    // --------------------- VARIABLES ---------------------

    // public
    public enum Type { Move, Direct, Charter, Shuttle, Build, Treat, Share, Cure};
    public Type type;


    // private
    protected Player player;
    protected string param1, param2; // only some are used

    // references



    // --------------------- CUSTOM METHODS ----------------


    // commands
    public Action GetAction(Type type, Player player, string param1, string param2) {
        //create object, set parameters, give it back
        return null;
    }

    public abstract void Perform();



    // queries
    public abstract bool CanPerform();
    public abstract string LogString();

    public string[] AllPerformableParameters() { return null; }

    // other
    public string CityToNid { get { return param1; } }
    public string DiseaseNid { get { return param1; } }
    public string PlayerNid { get { return param2; } }


}