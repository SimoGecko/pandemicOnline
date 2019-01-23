// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

////////// DESCRIPTION //////////

public class Deck {
    // --------------------- VARIABLES ---------------------

    // public
    //info: index 0 is top, assuming covered deck

    // private
    //string deckName;
    public List<Card> Cards { get; private set; }

    // references
	
	
	

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public Deck() {
        Cards = new List<Card>();
    }

    public Deck(Deck d) {
        //copies deck
    }



    //ORDERING
    public void Shuffle() {
        Cards.Shuffle();
        //Permutation = Utility.Permutation(NumCards);
    }

    public void Shuffle(int[] perm) {
        //to make shuffled decks consistent across clients
    }

    public void Sort() {
        Cards.Sort();
    }


    //ADD
    public void Add(string nid, char color) {
        Card newCard = new Card(NumCards, color, nid);
        AddTop(newCard);
    }

    public void AddTop(Card c) {
        Cards.Insert(0, c);
    }
    public void AddBottom(Card c) {
        Cards.Add(c);
    }
    public void AddAt(Card c, int idx) {
        //idx=NumCards is allowed as it's added at the end
        Debug.Assert(0 <= idx && idx <= NumCards, "Add at index out of range");
        Cards.Insert(idx, c);
    }

    //REMOVE
    public Card RemoveTop() {
        return RemoveAt(0);
    }

    public Card RemoveBottom() {
        return RemoveAt(NumCards-1);
    }

    Card RemoveAt(int idx) {
        Debug.Assert(!EmptyDeck, "Deck is empty!");
        Debug.Assert(0 <= idx && idx < NumCards, "Card index out of range");
        Card result = Cards[idx];
        Cards.RemoveAt(idx);
        return result;
    }

    public void Discard(Card c) {
        Debug.Assert(Cards.Contains(c), "Card is not present in deck");
        Cards.Remove(c);
    }


    //SPLIT JOIN
    public Deck Split(int numCards) { // takes n cards from top
        Debug.Assert(0 <= numCards && numCards < NumCards, "Not enough cards to split");

        Deck result = new Deck();
        for(int i=0; i<numCards; i++) {
            result.AddBottom(RemoveTop());
        }
        return result;
    }

    public Deck[] SplitInto(int numDecks) {
        //split into n decks
        Deck[] result = new Deck[numDecks];
        int remainingCards = NumCards;
        for (int i = 0; i < numDecks; i++) {
            int cardsToTake = Mathf.RoundToInt(((float)remainingCards) / (numDecks - i));
            remainingCards -= cardsToTake;
            result[i] = Split(cardsToTake);
        }
        return result;
    }

    public void JoinTop(Deck deck) { // adds the other deck on top
        for (int i = 0; i < deck.NumCards; i++) {
            this.AddTop(deck.RemoveBottom());
        }
    }

    






    // queries
    public int NumCards { get { return Cards.Count; } }
    public bool EmptyDeck { get { return Cards.Count == 0; } }

    public bool HasCard(Card c) {
        return Cards.Contains(c);
    }

    public Card PeekTop() {
        return Cards[0];
    }
    public Card PeekBottom() {
        return Cards[NumCards - 1];
    }

    public int[] Permutation {
        get { return Cards.Select(c => c.id).ToArray(); }
    }

    public string[] CardStrings {
        get { return Cards.Select(c => c.Nid).ToArray(); }
    }


    // other

}