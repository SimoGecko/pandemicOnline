// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// manages the communication of commands for the game //////////

public class NetworkManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public


    // private
    //fName in the form "/folder/file.txt"
    //base folder: pandemic_online
    //string downloadedResult = "";
    SyncedFile sf;

    // references
    public static NetworkManager instance;
    public ConsoleIO commandConsole;


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
    public void StartNetworkedGame(Lobby lobby) {
        commandConsole.OnSubmitString += CommandManager.instance.ProcessCommand;

        sf = gameObject.AddComponent<SyncedFile>();
        sf.Setup(string.Format("games/game_{0}.txt", lobby.lobbyID));

        sf.OnNewRemoteLine += CommandManager.instance.ProcessCommand;
    }




    // queries



    // other

}