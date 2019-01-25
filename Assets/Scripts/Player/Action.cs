// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public abstract class Action {
    // --------------------- VARIABLES ---------------------

    // public
    public enum Type { Move, Direct, Charter, Shuttle, Build, Treat, Share, Cure};
    //public Type type;


    // private
    protected Player player; // owner
    protected string[] param/*, param2*/; // only some are used

    // references



    // --------------------- CUSTOM METHODS ----------------


    // commands
    public void Setup(string pNid, string cNid, string dNid, string oNid) {
        player = Player.Get(pNid);
        param = new string[] { pNid, cNid, dNid, oNid };
    }


    public static Action GetAction(Type type, string pNid, string cNid="", string dNid="", string oNid = "") {
        //create object, set parameters, give it back
        Action result=null;
        switch (type) {
            case Type.Move:     result = new MoveAction(); break;
            case Type.Direct:   result = new DirectAction(); break;
            case Type.Charter: result = new CharterAction(); break;
            case Type.Shuttle: result = new ShuttleAction(); break;
            case Type.Build: result = new BuildAction(); break;
            case Type.Treat: result = new TreatAction(); break;
            case Type.Share: result = new ShareAction(); break;
            case Type.Cure: result = new CureAction(); break;
        }
        result.Setup(pNid, cNid, dNid, oNid);
        return result;
    }



    public void TryPerform() {
        if (CanPerform()) Perform();
        else {
            Debug.Log("couldn't perform action");
        }
    }

    void Perform() {
        player.DoAction();
        PerformCustom();
        //LogString();
    }
    public abstract void PerformCustom();



    // queries
    bool CanPerform() {
        return player.CanDoAction() && CanPerformCustom();
    }
    public abstract bool CanPerformCustom();



    public abstract string LogString();

    public string[] AllPerformableParameters() { return null; } // TODO

    // other
    public string PlayerParam { get { return param[0]; } }
    public string CityParam { get { return param[1]; } }
    public string DiseaseParam { get { return param[2]; } }
    public string OtherParam { get { return param[3]; } }

}