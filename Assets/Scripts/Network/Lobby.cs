// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class Lobby {
    // --------------------- VARIABLES ---------------------

    // public
    const int maxNumUsersPerLobby = 4;

    public string lobbyID = "oBsZXE";// { get; private set; }
    public string lobbyName = "defaultName";


    // private
    List<User> users;


    // references


    // --------------------- CUSTOM METHODS ----------------


    // commands
    public Lobby(string lobbyID, string lobbyName) {
        this.lobbyID = lobbyID;
        this.lobbyName = lobbyName;

        users = new List<User>();
    }

    public void Join(User u) {
        if (!CanJoin(u)) return;

        users.Add(u);
    }
    public void LeaveLobby(User u) {
        if (!CanLeave(u)) return;
        users.Remove(u);
    }



    // queries
    public int NumPlayers { get { return users.Count; } }
    bool CanJoin(User u) {
        return !u.IsInLobby && NumPlayers < maxNumUsersPerLobby;
    }
    bool CanLeave(User u) {
        return users.Contains(u) && true;
    }


    // other

}