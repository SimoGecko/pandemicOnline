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
        allCommands = new List<Command>();

        Command newC = new Command("disease_infect C D");
        newC.GiveMethod(() => DiseaseManager.instance.Infect(newC.cityNid, 1, newC.diseaseNid));
        allCommands.Add(newC);
        //DiseaseManager.instance.Infect(particleEmitter[0]) // plug
    }

    public void ProcessCommand(string text) {
        List<string> parts = text.Split(' ').ToList();

        if (text == "help") {
            //special case, output help in console
        }

        Command c = FindMatchingCommand(parts[0]);
        if (c == null) {
            Debug.Log("couldn't find command named " + parts[0]);
            return;
        }

        parts.RemoveAt(0);
        string[] parameters = parts.ToArray();
        if (c.IsValid(parameters)) {
            c.Invoke(parameters);
        } else {
            Debug.Log("invalid parameters for command " + c.memo);
        }
    }


    // queries
    Command FindMatchingCommand(string memo) {
        Command result = allCommands.Find(c => c.memo == memo);
        return result;
    }



    // other
    

}