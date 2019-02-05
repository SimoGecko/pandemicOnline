// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

////////// difficulty, win/lose conditions (should only control the play/pause/state of game) //////////

public class GameManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------
    public enum Difficulty { easy, medium, hard }
    public enum State { lobby, play, pause, end }

    // public
    public Difficulty difficulty;
    public State state { get; private set; }
    public bool DEBUG = true;

    // private
    DateTime gameStartTimer;

    // references
    public static GameManager instance;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        if (instance != null) Destroy(this);
        instance = this;
    }

    void Start() {
        //StartGame();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.S) && !Playing) {
            //StartGame();
        }
        /*
        if (Playing) {
            GameTimer += Time.deltaTime;
        }*/
    }

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public void StartGame() {
        FlowManager.instance.GameSetup();
        FlowManager.instance.GameStart();
        state = State.play;
        gameStartTimer = DateTime.Now;
    }
    

    public void SetState(State s) {
        state = s;
    }

    // queries
    public bool Playing { get { return state == State.play; } }
    public TimeSpan ElapsedGameTime { get { return DateTime.Now - gameStartTimer; } }




    // other

}