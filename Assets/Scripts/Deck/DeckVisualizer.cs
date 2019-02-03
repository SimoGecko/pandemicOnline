// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

////////// takes a deck and visualizes it in the canvas //////////

public class DeckVisualizer : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    public bool openCards = true; // shows front or back

    // private


    // references
    Deck deckToVisualize;

    RectTransform cardPanel;
    TextMeshProUGUI cardInfoText;

    // --------------------- BASE METHODS ------------------
    void Start() {
    }

    void Update() {

    }

    // --------------------- CUSTOM METHODS ----------------


    // commands

    public void SetDeck(Deck d) {
        //find refs
        cardPanel = GetComponent<RectTransform>();
        cardInfoText = GetComponentInChildren<TextMeshProUGUI>();
        Debug.Assert(cardPanel != null, "deckVisualizer is on component without rectTransform");
        Debug.Assert(cardInfoText != null, "deckVisualizer has no TMPro child");

        deckToVisualize = d;
        d.OnDeckChange += VisualizeDeck;
        VisualizeDeck();
    }

    void VisualizeDeck() {
        for (int i = 1; i < cardPanel.childCount; i++) {
            Destroy(cardPanel.GetChild(i).gameObject);
        }
        for (int i = 0; i < deckToVisualize.NumCards; i++) {
            RectTransform newCard = Instantiate(ElementManager.instance.cardUIPrefab, cardPanel) as RectTransform;

            //set values
            string cardName = openCards ? deckToVisualize.Cards[i].Nid : "";
            Color cardColor = openCards ? ColorManager.instance.Char2Color(deckToVisualize.Cards[i].color) : Color.white;

            newCard.Find("cardName").GetComponent<TextMeshProUGUI>().text = cardName;
            newCard.Find("cardColor").GetComponent<Image>().color = cardColor;
        }
        cardInfoText.text = deckToVisualize.DeckName + ": " + deckToVisualize.NumCards;
    }


    // queries



    // other

}