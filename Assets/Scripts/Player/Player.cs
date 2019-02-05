// (c) Simone Guggiari 2018

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class Player : IElement {
    // --------------------- VARIABLES ---------------------

    // public
    const int maxActions = 4;
    const int handLimit = 7;

    public DateTime startTurnTime;// { get; private set; }


    // private
    public string Nid { get; private set; }
    public Deck personalDeck { get; private set; }

    int numPerformedActions = 0;
    public bool isTurn { get; private set; }
    int id;
    char colorChar;

    // references
    City currentCity;
    Pawn pawn;



    // --------------------- CUSTOM METHODS ----------------

    public Player(int id, Color color, string nid) {
        this.id = id;
        //this.color = color;
        colorChar = id.ToString()[0];
        Nid = nid;
    }


    // commands
    public void Setup() {
        //create pawn
        pawn = ElementManager.instance.Copy(ElementManager.instance.pawnPrefab);
        pawn.SetColor(colorChar);
        pawn.offsetPos = Quaternion.Euler(0, 60 * (id + 1), 0) * Vector3.forward * .15f;
        pawn.placed = true;
    }

    public void Move(string cityNid) {
        //move pawn
        if (currentCity != null) currentCity.RemovePlayer(this);
        currentCity = City.Get(cityNid);
        currentCity.AddPlayer(this);

        pawn.MoveToCity(cityNid);
    }
    public void GiveInitialDeck(Deck deck) {
        personalDeck = deck;
    }


    //ACTIONS
    /*
    public void TryDoAction(Action a) {
        if (CanDoAction(a)) DoAction(a);
    }*/


    public bool CanDoAction() {
        //return true;
        return numPerformedActions < maxActions && isTurn && true;
    }

    public void DoAction() {
        numPerformedActions++;
    }

    public void StartTurn() {
        NetworkManager.instance.LogSeparator();

        numPerformedActions = 0;
        startTurnTime = DateTime.Now;
        //TurnTimer = 0;
        isTurn = true;
    }

    public void EndTurn() { // this is called as action
        if (!isTurn) return;
        isTurn = false;
        NetworkManager.instance.LogSeparator();

        FlowManager.instance.OnTurnEnd();
    }

    public void Draw(int numCards) {
        for (int i = 0; i < numCards; i++) {
            Card drawnCard = PlayerManager.instance.DrawCard();
            personalDeck.AddTop(drawnCard);
            if (drawnCard.Nid == "epidemic") {
                DiseaseManager.instance.EpidemicByCard();
                //set in discard pile
                Discard("epidemic");
            } else {
                //personalDeck.AddTop(drawnCard);
            }
        }
        personalDeck.Sort();

        //hand limit
        while (personalDeck.NumCards > handLimit) {
            //TODO let user choose which to discard
            string cardNid = personalDeck.PeekTop().Nid;
            //Discard();
            CommandManager.instance.ProcessCommand(string.Format("discard {0} {1}", Nid, cardNid));
        }
    }



    // queries
    public City CurrentCity {
        get { return currentCity; }
    }



    public bool HasCard(string cardNid) {
        return personalDeck.HasCard(cardNid);
    }

    public void Discard(string cardNid) {
        Card removed = personalDeck.Remove(cardNid);
        PlayerManager.instance.Discard(removed);
    }

    public static Player Get(string playerNid) {
        return PlayerManager.instance.GetPlayer(playerNid);
    }
    public static Player Get(int id) {
        return PlayerManager.instance.GetPlayer(id);
    }

    public int NumRemainingActions {
        get {
            return isTurn ? maxActions - numPerformedActions : 0;
        }
    }

    public Color Color {
        get { return ColorManager.instance.Char2Color(colorChar); }
    }

    public TimeSpan ElapsedTurnTime { get { return DateTime.Now - startTurnTime; } }


    public string GetStatus() {
        //TimeSpan elapsedTime = DateTime.Now - startTurnTime;
        return isTurn ? string.Format("turn: ({0} left)\n{1}", NumRemainingActions, Utility.MinuteStamp(ElapsedTurnTime)) : "-";
    }


    // other

}