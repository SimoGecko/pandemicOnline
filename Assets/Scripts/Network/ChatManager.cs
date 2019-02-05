// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// manages the communication of chat lines //////////

public class ChatManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    public string myName = "player";
    public bool logOnline = true; // else log to file

    // private
    SyncedFile sf;

    // references
    public static ChatManager instance;
    public ConsoleIO chatConsole;

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
        sf.Setup(string.Format("data/chat_{0}.txt", lobby.lobbyID));

        chatConsole.OnInput += LocalNewInput; // local input
        sf.OnNewLine += RemoteNewInput; // remote input
    }


    public void LocalNewInput(string s) {
        string toLog = string.Format("[{0}] {1}:\t{2}", Utility.HourStamp(System.DateTime.Now), myName, s);
        //chatConsole.OutputConsole(toLog);

        if (logOnline) sf.Write(toLog);
    }

    public void RemoteNewInput(string s) {
        chatConsole.OutputConsole(s);
    }



    // queries



    // other

}