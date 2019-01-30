// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

////////// DESCRIPTION //////////

public class LobbyManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    public bool randomId = false;

    // private
    List<Lobby> existingLobbies;
    Lobby testLobby;

    // references
    public static LobbyManager instance;
    public Button connectButton; 
    public InputField username, inputID;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        if (instance != null) Destroy(this);
        instance = this;

        //if(randomId) lobbyID = 
        existingLobbies = new List<Lobby>();
    }

    void Start () {
        connectButton.onClick.AddListener(Connect);
        username.text = "player";
        //inputID.text = lobbyID;
    }
	
	void Update () {
        
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    void CreateLobby() {
        string lobbyId = Utility.GetAlphanumericRandomString(6);
        Lobby newLobby = new Lobby(lobbyId, "lobbyName");
    }

    private void Connect() {
        //lobbyID = inputID.text;
        ChatManager.instance.myName = username.text;

        ChatManager.instance.StartChat(testLobby);
    }


    // queries



    // other

}