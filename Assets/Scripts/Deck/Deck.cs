// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//using System.Collections.ObjectModel;

////////// represents a generic deck of cards that can be manipulate (add/remove/peek cards, shuffle/order) //////////

public class Deck {
    // --------------------- VARIABLES ---------------------

    // public
    //info: index 0 is top, assuming covered deck

    // private
    //string deckName;
    //bool covered;
    public List<Card> Cards { get; private set; }

    // references
    public event System.Action OnDeckChange; // plug it

    //public event System.Action OnDeckEnd
	
	
	

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public Deck() {
        Cards = new List<Card>();
        //Cards.CollectionChanged += DeckChanged;
    }

    public Deck(Deck d) {
        //copies deck
        Cards = new List<Card>();
        for (int i = 0; i < d.NumCards; i++) {
            Card copyCard = new Card(d.PeekAt(i));
            AddBottom(copyCard);
        }
    }



    //ORDERING
    public void Shuffle() {
        Cards.Shuffle();
        //Permutation = Utility.Permutation(NumCards);
    }

    public void Shuffle(int[] perm) {
        //to make shuffled decks consistent across clients
        Cards.Sort();
        List<Card> newOrder = new List<Card>();
        for (int i = 0; i < NumCards; i++) {
            newOrder.Add(Cards[perm[i]]);
        }
        Cards = newOrder;
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

    public Card RemoveAt(int idx) {
        Debug.Assert(!EmptyDeck, "Deck is empty!");
        Debug.Assert(0 <= idx && idx < NumCards, "Card index out of range");
        Card result = Cards[idx];
        Cards.RemoveAt(idx);
        return result;
    }

    public Card Remove(Card c) {
        Debug.Assert(Cards.Contains(c), "Deck doesn't contain card "+c.Nid);
        Cards.Remove(c);
        return c;
    }
    public Card Remove(string cardNid) {
        Debug.Assert(CardStrings.Contains(cardNid), "Deck doesn't contain card " + cardNid);
        return Remove(GetCard(cardNid));
    }
    /*
    public void Discard(Card c) {
        Debug.Assert(Cards.Contains(c), "Card is not present in deck");
        Cards.Remove(c);
    }*/


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
        Debug.Assert(NumCards >= numDecks, "Not enough cards to split into decks");
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

    
    void DeckChanged() {
        if (OnDeckChange != null) OnDeckChange();
    }






    // queries
    public int NumCards { get { return Cards.Count; } }
    public bool EmptyDeck { get { return Cards.Count == 0; } }

    public bool HasCard(Card c) {
        return Cards.Contains(c);
    }

    public bool HasCard(string cardNid) {
        return CardStrings.Contains(cardNid);
    }

    public Card[] AllCardsSatisfying(System.Predicate<Card> predicate) {
        return Cards.Where(c => predicate(c)).ToArray();
    }

    public Card GetCard(string cardNid) {
        return Cards.Find(c => c.Nid == cardNid);
    }

    public Card PeekTop() {
        return Cards[0];
    }
    public Card PeekBottom() {
        return Cards[NumCards - 1];
    }
    public Card PeekAt(int id) {
        Debug.Assert(0 <= id && id < NumCards, "invalid peak point");
        return Cards[id];
    }

    public int[] Permutation {
        get { return Cards.Select(c => c.id).ToArray(); }
    }

    public string[] CardStrings {
        get { return Cards.Select(c => c.Nid).ToArray(); }
    }


    // other

}