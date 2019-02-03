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
    public InputField usernameInput, lobbyInput;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        if (instance != null) Destroy(this);
        instance = this;

        existingLobbies = new List<Lobby>();
    }

    void Start() {
        //testLobby = new Lobby(Lobby.defaultID, "my lobby");

        usernameInput.text = "player";
        lobbyInput.text = "aloe";
        connectButton.onClick.AddListener(ConnectTest);

    }

    void Update() {

    }

    // --------------------- CUSTOM METHODS ----------------


    // commands
    void CreateLobby() {
        /*
        string lobbyId = Utility.GetAlphanumericRandomString(6);
        Lobby newLobby = new Lobby(lobbyId, "lobbyName");
        */
    }

    private void ConnectTest() {
        ChatManager.instance.myName = usernameInput.text;

        testLobby = new Lobby(lobbyInput.text, "lobbyName");

        Utility.SetRandomSeed(testLobby.lobbyID.GetHashCode());

        ChatManager.instance.Setup(testLobby);
        NetworkManager.instance.Setup(testLobby);

        GameManager.instance.StartGame();
    }


    // queries



    // other

}