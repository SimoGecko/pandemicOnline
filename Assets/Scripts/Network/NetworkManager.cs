// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// manages the communication of commands for the game //////////

public class NetworkManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    bool logOnline = true;

    // private
    //fName in the form "/folder/file.txt"
    //base folder: pandemic_online
    //string downloadedResult = "";
    SyncedFile sf;

    public bool IsHost { get; private set; }

    // references
    public static NetworkManager instance;
    public ConsoleIO commandConsole;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        if (instance != null) Destroy(this);
        instance = this;
    }

    void Start() {
        IsHost = true;
    }

    void Update() {

    }

    // --------------------- CUSTOM METHODS ----------------



    // commands
    public void Setup(Lobby lobby) {

        sf = gameObject.AddComponent<SyncedFile>();
        sf.Setup(string.Format("games/game_{0}.txt", lobby.lobbyID));

        commandConsole.OnInput += LocalNewInput;
        sf.OnNewRemoteLine += RemoteNewInput;

        CommandManager.instance.OutputEvent += commandConsole.OutputConsole; // out -> give to console
    }

    public void LocalNewInput(string s) {
        CommandManager.instance.ProcessCommand(s); // locally

        //only if performable log it online

        if (logOnline) {
            sf.Write(s);
        }
    }

    void RemoteNewInput(string s) {
        CommandManager.instance.ProcessCommand(s);
    }




    // queries



    // other

}