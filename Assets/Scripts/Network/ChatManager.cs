// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class ChatManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    public string myName = "player";
    public bool logOnline = true;

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

    void Start () {
        
    }

    void Update () {
        
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public void StartChat() {
        chatConsole.OnSubmitString += Chat;

        sf = gameObject.AddComponent<SyncedFile>();
        sf.Setup(string.Format("chat_{0}.txt", LobbyManager.instance.lobbyID));

        sf.OnNewRemoteLine += OnNewLine;
    }


    public void Chat(string s) {
        string toLog = string.Format("[{0}] {1}:\t{2}", Utility.HourStamp(System.DateTime.Now), myName, s);
        chatConsole.Log(toLog);
        if (logOnline) {
            sf.Write(toLog);
        }
    }

    public void OnNewLine(string s) {
        chatConsole.Log(s);
    }



    // queries



    // other

}