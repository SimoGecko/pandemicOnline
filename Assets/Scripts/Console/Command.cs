// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
////////// DESCRIPTION //////////

public class Command {
    readonly char[] paramChars = new char[] { 'P', 'C', 'D', 'O' }; // player city disease otherPlayer

    public string memo;
    public int[] paramIndex; // -1 = not used

    string[] param; // given when invoking
    public int NumRequiredParams { get; private set; }

    public System.Action f;

    public Command(string formulation) {
        //formulation = formulation.Replace(" ", "").Replace("\t", ""); // remove spaces & tabs

        List<string> parts = formulation.Split(' ').ToList();

        memo = parts[0];
        parts.RemoveAt(0);

        paramIndex = new int[] { -1, -1, -1, -1 };

        NumRequiredParams = parts.Count;
        for (int i = 0; i < NumRequiredParams; i++) {
            char C = parts[i][0];
            int idx = Array.FindIndex(paramChars, c => c == C);
            Debug.Assert(idx != -1, "wrong formulation instruction for parameter");
            paramIndex[idx] = i;
        }
    }

    public void GiveMethod(System.Action method) {
        f = method;
    }

    public bool IsValid(string[] parameters) {
        param = parameters;
        for (int i = 0; i < paramIndex.Length; i++) {
            if (paramIndex[i] != -1) {
                if (Elem(i) == null) return false;
            }
        }
        return true;
    }

    public void Invoke(string[] parameters) {
        Debug.Assert(IsValid(parameters), "Passed parameters to invoke are not valid");
        if (IsValid(parameters)) {
            param = parameters;
            f();
        }
    }

    //access
    public IElement Elem(int i) {
        switch (i) {
            case 0: return player;
            case 1: return city;
            case 2: return disease;
            case 3: return otherPlayer;
            default: return null;
        }
    }

    #region elementAccess
    public Player player {
        get {
            if (paramIndex[0] != -1)
                return Player.Get(param[paramIndex[0]]);
            return null;
        }
    }
    public City city {
        get {
            if (paramIndex[1] != -1)
                return City.Get(param[paramIndex[1]]);
            return null;
        }
    }
    public Disease disease {
        get {
            if (paramIndex[2] != -1)
                return Disease.Get(param[paramIndex[2]]);
            return null;
        }
    }
    public Player otherPlayer {
        get {
            if (paramIndex[3] != -1)
                return Player.Get(param[paramIndex[3]]);
            return null;
        }
    }
    #endregion

    #region nidAccess
    public string playerNid { get { return NidAccess(0); } }
    public string cityNid { get { return NidAccess(1); } }
    public string diseaseNid { get { return NidAccess(2); } }
    public string otherPlayerNid { get { return NidAccess(3); } }

    public string NidAccess(int i) {
        if (paramIndex[i] != -1)
            return param[paramIndex[i]];
        return null;
    }
    #endregion

}