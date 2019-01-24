// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

////////// DESCRIPTION //////////

public class DeckVisualizer : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    public bool openCards = true; // shows front or back

    // private


    // references
    RectTransform cardPanel;
    Deck deckToVisualize;
	
	
	// --------------------- BASE METHODS ------------------
	void Start () {
        //VisualizeDeck();
        cardPanel = GetComponent<RectTransform>();
        Debug.Assert(cardPanel != null, "deckVisualizer is on component without rectTransform");
    }
	
	void Update () {
        
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    /*
    void CreateSampleDeck() {
        deckToVisualize = new Deck();
        deckToVisualize.Add("sfo", 'k');
        deckToVisualize.Add("simo", 'b');
        deckToVisualize.Add("aladspofg", 'y');
        deckToVisualize.Shuffle();
        deckToVisualize.Add("newbottom", 'r');
        deckToVisualize.RemoveBottom();
    }*/

    public void SetDeck(Deck d) {
        deckToVisualize = d;
        d.OnDeckChange += VisualizeDeck;
    }

    void VisualizeDeck() {
        for(int i=0; i<cardPanel.childCount; i++) {
            Destroy(cardPanel.GetChild(i).gameObject);
        }
        for (int i = 0; i < deckToVisualize.NumCards; i++) {
            RectTransform newCard = Instantiate(ElementManager.instance.cardUIPrefab, cardPanel) as RectTransform;
            //set values
            newCard.Find("cardName").GetComponent< TextMeshProUGUI>().text = deckToVisualize.Cards[i].Nid;
            newCard.Find("cardColor").GetComponent<Image>().color = ColorManager.instance.Char2Color(deckToVisualize.Cards[i].color);
        }
    }


    // queries



    // other

}