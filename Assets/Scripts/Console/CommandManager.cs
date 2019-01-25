// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

////////// DESCRIPTION //////////



public class CommandManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    //help command
    readonly string[] commandStrings = new string[] {
        "infect C D",
        "outbreak C",
        "epidemic C",

        "move P C",
        "direct P C",
        "charter P C",
        "shuttle P C",

        "build P C",
        "treat P C D",
        "share P C O",
        "cure P C D",

        /*
        "draw P",
        "discard P C",
        "shuffle O", // should be N with deckID*/
    };

    readonly CommandInvoke[] commandInvokes = new CommandInvoke[] {
        (p, c, d, o) => DiseaseManager.instance.Infect(c, 1, d),
        (p, c, d, o) => DiseaseManager.instance.Outbreak(c, d),
        (p, c, d, o) => DiseaseManager.instance.Epidemic(c),

        (p, c, d, o) => Action.GetAction(Action.Type.Move, p, c).TryPerform(),
        (p, c, d, o) => Action.GetAction(Action.Type.Direct, p, c).TryPerform(),
        (p, c, d, o) => Action.GetAction(Action.Type.Charter, p, c).TryPerform(),
        (p, c, d, o) => Action.GetAction(Action.Type.Shuttle, p, c).TryPerform(),
                        
        (p, c, d, o) => Action.GetAction(Action.Type.Build, p, c).TryPerform(),
        (p, c, d, o) => Action.GetAction(Action.Type.Treat, p, c, d).TryPerform(),
        (p, c, d, o) => Action.GetAction(Action.Type.Share, p, c, o).TryPerform(),
        (p, c, d, o) => Action.GetAction(Action.Type.Cure, p, c, d).TryPerform(),
        /*
        (p, c, d, o) => ,
        (p, c, d, o) => ,
        (p, c, d, o) => ,*/
    };


    // private
    List<Command> allCommands;


    // references


    // --------------------- BASE METHODS ------------------
    void Start() {
        CreateCommands();
    }

    void Update() {

    }

    // --------------------- CUSTOM METHODS ----------------


    // commands
    void CreateCommands() {
        Debug.Assert(commandStrings.Length == commandInvokes.Length, "Size mismatch between command formulation and functions");

        allCommands = new List<Command>();

        for (int i = 0; i < commandStrings.Length; i++) {
            Command newC = new Command(commandStrings[i]);
            newC.GiveMethod(commandInvokes[i]);// (() => newC.playerNid() + "", newC.city, newC.disease, newC.otherPlayer)); // plug it here
            allCommands.Add(newC);
        }
        //CommandInvoke i = (p, c, d, o) => DiseaseManager.instance.Infect(c.Nid, 1, d.Nid);
    }
   

    public void ProcessCommand(string text) {
        List<string> parts = text.Split(' ').ToList();

        if (text == "help") {
            //special case, output help in console
            ConsoleIO.instance.Log("available commands: (P=player, C=city, D=disease, O=other player)");
            foreach(string s in commandStrings) {
                ConsoleIO.instance.Log(s);
            }
            return;
        }

        Command c = FindMatchingCommand(parts[0]);
        if (c == null) {
            ConsoleIO.instance.Log("unrecognized command '" + parts[0] + "'. Type 'help'.");
            return;
        }

        parts.RemoveAt(0);
        string[] parameters = parts.ToArray();
        if (c.IsValid(parameters)) {
            c.Invoke(parameters);
        } else {
            ConsoleIO.instance.Log("Invalid parameters for command " + c.memo + ".");
        }
    }


    // queries
    Command FindMatchingCommand(string memo) {
        Command result = allCommands.Find(c => c.memo == memo);
        return result;
    }


    // other
    

}