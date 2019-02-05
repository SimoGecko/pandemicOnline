// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// manages the communication of commands for the game //////////

public class NetworkManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    bool logOnline = true;

    public const string separator = "-----";

    // private
    SyncedFile sf;

    public bool IsHost { get; set; }

    // references
    public static NetworkManager instance;
    public ConsoleIO commandConsole;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        if (instance != null) Destroy(this);
        instance = this;
    }

    void Start() {

    }

    void Update() {

    }

    // --------------------- CUSTOM METHODS ----------------



    // commands
    public void Setup(Lobby lobby) {
        sf = gameObject.AddComponent<SyncedFile>();
        sf.Setup(string.Format("data/game_{0}.txt", lobby.lobbyID));

        commandConsole.OnInput += LocalNewInput;
        sf.OnNewLine += RemoteNewInput;

        //CommandManager.instance.OutputEvent += commandConsole.OutputConsole; // out -> give to console
    }

    public void LocalNewInput(string s) {

        //only if performable log it online

        if (logOnline && CommandManager.instance.ValidCommandString(s)) {
            sf.Write(s);
        }
        //CommandManager.instance.ProcessCommand(s);

        //CommandManager.instance.ProcessCommand(s); // locally
    }

    void RemoteNewInput(string s) {
        commandConsole.OutputConsole(s);
        CommandManager.instance.ProcessCommand(s);
        /*
        if (s.Equals(separator)) commandConsole.OutputConsole(separator);
        else {
            
        }*/
    }

    public void LogSeparator() {
        if (IsHost && logOnline) {
            sf.Write(separator);
            //commandConsole.OutputConsole(separator);
        }
    }




    // queries



    // other

}