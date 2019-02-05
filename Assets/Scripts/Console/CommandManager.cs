// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

////////// has a map of all the possible commands and invokes the appropriately from string //////////



public class CommandManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    readonly string[] commandStrings = new string[] {
        "infect C D N", // C D N
        "outbreak C D",
        "epidemic C",

        "move P C",
        "direct P C",
        "charter P C",
        "shuttle P C",

        "build P C",
        "treat P C D",
        "share P C O",
        "cure P C D",

        "discard P C",
        "endturn P",

    };

    readonly CommandInvoke[] commandInvokes = new CommandInvoke[] {
        (p, c, d, o, n) => DiseaseManager.instance.Infect(c, d, n), // n = num infections
        (p, c, d, o, n) => DiseaseManager.instance.Outbreak(c, d),
        (p, c, d, o, n) => DiseaseManager.instance.Epidemic(c),
                   
        (p, c, d, o, n) => Action.GetAction(Action.Type.Move, p, c).TryPerform(),
        (p, c, d, o, n) => Action.GetAction(Action.Type.Direct, p, c).TryPerform(),
        (p, c, d, o, n) => Action.GetAction(Action.Type.Charter, p, c).TryPerform(),
        (p, c, d, o, n) => Action.GetAction(Action.Type.Shuttle, p, c).TryPerform(),
                  
        (p, c, d, o, n) => Action.GetAction(Action.Type.Build, p, c).TryPerform(),
        (p, c, d, o, n) => Action.GetAction(Action.Type.Treat, p, c, d).TryPerform(),
        (p, c, d, o, n) => Action.GetAction(Action.Type.Share, p, c, o).TryPerform(),
        (p, c, d, o, n) => Action.GetAction(Action.Type.Cure, p, c, d).TryPerform(),
                  
        (p, c, d, o, n) => Player.Get(p).Discard(c),
        (p, c, d, o, n) => Player.Get(p).EndTurn(),

    };


    // private
    List<Command> allCommands;


    // references
    public static CommandManager instance;
    public event StringEvent OutputEvent;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        instance = this;
        CreateCommands();

    }

    void Start() {

    }

    void Update() {

    }

    // --------------------- CUSTOM METHODS ----------------


    // commands
    void CreateCommands() {
        Debug.Assert(commandStrings.Length == commandInvokes.Length, "Size mismatch between command formulation and functions");

        allCommands = new List<Command>();

        for (int i = 0; i < commandStrings.Length; i++) {
            Command newC = new Command(commandStrings[i], commandInvokes[i]);
            allCommands.Add(newC);
        }
    }



    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    void ProcessCommandOld(string text) {
        if (string.IsNullOrEmpty(text)) return;

        //if (text[text.Length - 1] == ' ') text.Remove(text.Length - 1, 1); // remove last space

        OutputTextLocal(text); // ALREADY OUTPUT THE COMMAND PROCESSED

        List<string> parts = text.Split(' ').ToList();


        //special case, output help in console
        if (text == "help") {
            OutputTextLocal("available commands: (P=player, C=city, D=disease, O=other player)");
            foreach (string s in commandStrings) {
                OutputTextLocal(s);
            }
            return;
        }

        Command c = FindMatchingCommand(parts[0]);
        if (c == null) {
            OutputTextLocal("unrecognized command '" + parts[0] + "'. Type 'help'.");
            return;
        }

        parts.RemoveAt(0);
        string[] parameters = parts.ToArray();
        if (!c.IsValid(parameters)) {
            //OutputText(text);
            OutputTextLocal("Invalid parameters for command " + c.memo + ".");
        } else {
            //OutputText(text);
            c.Invoke(parameters);
        }
    }

    public void ProcessCommand(string text) {
        if (string.IsNullOrEmpty(text)) return;

        List<string> parts = text.Split(' ').ToList();

        Command c = FindMatchingCommand(parts[0]);
        if (c == null) return;

        parts.RemoveAt(0);
        string[] parameters = parts.ToArray();
        if (c.IsValid(parameters)) {
            c.Invoke(parameters);
        }
    }


    public bool ValidCommandString(string text) {
        if (string.IsNullOrEmpty(text)) return false;

        List<string> parts = text.Split(' ').ToList();

        Command c = FindMatchingCommand(parts[0]);
        if (c == null) return false;

        parts.RemoveAt(0);
        string[] parameters = parts.ToArray();
        if (parameters == null) return false;
        return c.IsValid(parameters);
    }



    public void OutputTextLocal(string s) {
        if (OutputEvent != null) OutputEvent(s);
    }



    // queries
    public Command FindMatchingCommand(string memo) {
        Command result = allCommands.Find(c => c.memo == memo);
        return result;
    }



    // other


}