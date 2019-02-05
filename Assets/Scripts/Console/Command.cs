// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

////////// represents a command with a memo, variable parameters and an invoke //////////

public delegate void CommandInvoke(string p, string c, string d, string o, string n);


public class Command {
    readonly char[] paramChars = new char[] { 'P', 'C', 'D', 'O', 'N' }; // player city disease otherPlayer number

    //-----------------------------

    public string memo;

    public int[] paramIndexMap; // -1 = not used // maps a string parameter to the appropriate type
    string[] param; // given when invoking
    public int NumRequiredParams { get; private set; }

    public CommandInvoke f;


    //-----------------------------

    public Command(string formulation, CommandInvoke method) {
        List<string> parts = formulation.Split(' ').ToList();

        //memo
        memo = parts[0];
        parts.RemoveAt(0);

        //params
        paramIndexMap = new int[] { -1, -1, -1, -1, -1 };

        NumRequiredParams = parts.Count;
        for (int i = 0; i < NumRequiredParams; i++) {
            char C = parts[i][0];
            int idx = Array.FindIndex(paramChars, c => c == C);
            Debug.Assert(idx != -1, "wrong formulation instruction for parameter");
            paramIndexMap[idx] = i;
        }

        //method
        f = method;
    }

    public bool IsValidAllParams(string[] parameters) {
        //filter to only needed params
        return IsValid(FilterParams(parameters));
    }

    string[] FilterParams(string[] parameters) {
        Debug.Assert(parameters.Length == paramChars.Length, "Not all parameters were supplied");
        List<string> needed = new List<string>();
        for (int i = 0; i < paramIndexMap.Length; i++) {
            if (paramIndexMap[i] != -1) {
                needed.Add(needed[paramIndexMap[i]]);
            }
        }
        return needed.ToArray();
    }

    public bool IsValid(string[] parameters) {
        param = parameters;
        if (param.Length != NumRequiredParams) return false;
        for (int i = 0; i < paramIndexMap.Length; i++) {
            if (paramIndexMap[i] != -1) {
                if (Elem(i) == null) return false;
            }
        }
        return true;
    }

    public void InvokeAllParams(string[] parameters) {
        Invoke(FilterParams(parameters));
    }

    public void Invoke(string[] parameters) {
        Debug.Assert(IsValid(parameters), "Passed parameters to invoke are not valid");
        if (IsValid(parameters)) {
            param = parameters;
            f(playerNid, cityNid, diseaseNid, otherPlayerNid, numberNid); // invoke
        }
    }

    //access
    public IElement Elem(int i) {
        switch (i) {
            case 0: return player;
            case 1: return city;
            case 2: return disease;
            case 3: return otherPlayer;
            case 4: return number;
            default: return null;
        }
    }

    #region elementAccess
    Player player {
        get {
            if (paramIndexMap[0] != -1) {
                return Player.Get(param[paramIndexMap[0]]);
            }
            return null;
        }
    }
    City city {
        get {
            if (paramIndexMap[1] != -1) {
                return City.Get(param[paramIndexMap[1]]);
            }
            return null;
        }
    }
    Disease disease {
        get {
            if (paramIndexMap[2] != -1) {
                return Disease.Get(param[paramIndexMap[2]]);
            }
            return null;
        }
    }
    Player otherPlayer {
        get {
            if (paramIndexMap[3] != -1) {
                return Player.Get(param[paramIndexMap[3]]);
            }
            return null;
        }
    }
    NumberInt number {
        get {
            if (paramIndexMap[4] != -1) {
                return NumberInt.Get(param[paramIndexMap[4]]);
            }
            return null;
        }
    }
    #endregion

    #region nidAccess
    string playerNid { get { return NidAccess(0); } }
    string cityNid { get { return NidAccess(1); } }
    string diseaseNid { get { return NidAccess(2); } }
    string otherPlayerNid { get { return NidAccess(3); } }
    string numberNid { get { return NidAccess(4); } }

    string NidAccess(int i) {
        if (paramIndexMap[i] != -1)
            return param[paramIndexMap[i]];
        return null;
    }
    #endregion

    public class NumberInt : IElement { // HACK
        public int n;
        public NumberInt(int n) {
            this.n = n;
        }

        public string Nid { get { return n.ToString(); } }

        public static NumberInt Get(string s) {
            int a;
            if(int.TryParse(s, out a)) {
                return new NumberInt(a);
            }
            return null;
        }
    }

}