// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class Player : IElement {
    // --------------------- VARIABLES ---------------------

    // public
    const int maxActions = 4;
    const int handLimit = 7;


    // private
    public string Nid { get; private set; }
    public Deck personalDeck { get; private set; }

    int numPerformedActions = 0;
    bool isMyTurn = false;
    int id;
    public Color color { get; private set; }

    // references
    Pawn pawn;



    // --------------------- CUSTOM METHODS ----------------

    public Player(int id, Color color) {
        this.id = id;
        this.color = color;
        Nid = "player_" + id;
    }


    // commands
    public void Setup() {
        //create pawn
        pawn = ElementManager.instance.Copy(ElementManager.instance.pawnPrefab);
    }

    public void Move(string cityNid) {
        //move pawn
        pawn.MoveTo(cityNid);
    }
    public void GiveInitialDeck(Deck deck) {
        personalDeck = deck;
    }


    //ACTIONS
    public bool CanDoAction() {
        return numPerformedActions<maxActions && isMyTurn && true;
    }

    public void DoAction() {
        //TODO
        numPerformedActions++;
    }

    public void StartTurn() {
        numPerformedActions = 0;
        isMyTurn = true;
    }

    public void EndTurn() {
        isMyTurn = false;
    }

    public void Draw(int numCards) {
        for (int i = 0; i < numCards; i++) {
            Card drawnCard = PlayerManager.instance.DrawCard();
            if(drawnCard.Nid == "epidemic") {
                DiseaseManager.instance.Epidemic();
            } else {
                personalDeck.AddTop(drawnCard);
                personalDeck.Sort();
            }
        }
        //hand limit
        while (personalDeck.NumCards > handLimit) {
            //TODO let user choose which to discard
            Discard(personalDeck.PeekTop().Nid);
        }
    }



    // queries
    public City CurrentCity {
        get { return pawn.CurrentCity; }
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
           return isMyTurn ? maxActions - numPerformedActions : 0;
        }
    }

    public string GetStatus() {
        return "status";
    }


    // other

}