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
    //public bool invertOrder = false;

    // private
    bool openCards = true; // shows front or back


    // references
    Deck deckToVisualize;

    RectTransform cardPanel;
    TextMeshProUGUI cardInfoText;
    Toggle openCardsToggle;//, invertOrderToggle;

    // --------------------- BASE METHODS ------------------
    void Start() {
    }

    void Update() {

    }

    // --------------------- CUSTOM METHODS ----------------


    // commands

    public void SetDeck(Deck d, bool openCards = true) {
        //find refs
        cardPanel = GetComponent<RectTransform>();
        cardInfoText = GetComponentInChildren<TextMeshProUGUI>();
        openCardsToggle = transform.Find("deckInfo").GetComponentInChildren<Toggle>();
        //invertOrderToggle = transform.Find("invertToggle").GetComponent<Toggle>();

        Debug.Assert(cardPanel != null, "deckVisualizer is on component without rectTransform");
        Debug.Assert(cardInfoText != null, "deckVisualizer has no TMPro child");
        Debug.Assert(openCardsToggle != null, "deckVisualizer has no open cards toggle");
        //Debug.Assert(invertOrderToggle != null, "deckVisualizer has no invert order toggle");

        this.openCards = openCards;
        openCardsToggle.isOn = openCards;

        deckToVisualize = d;

        d.OnDeckChange += VisualizeDeck;
        openCardsToggle.onValueChanged.AddListener(SetOpenCards);
        //invertOrderToggle.onValueChanged.AddListener(b => invertOrder = b);
        VisualizeDeck();
    }

    void VisualizeDeck() {
        for (int i = 1; i < cardPanel.childCount; i++) {
            Destroy(cardPanel.GetChild(i).gameObject);
        }
        for (int i = 0; i < deckToVisualize.NumCards; i++) {
            RectTransform newCard = Instantiate(ElementManager.instance.cardUIPrefab, cardPanel) as RectTransform;


            //deck: if covered, i=0 -> top
            // if open, i = count-1 -> top
            int idx = openCards?(deckToVisualize.NumCards-1)-i: i;
            //if (invertOrder) idx = (deckToVisualize.NumCards - 1) - idx;

            Card card = deckToVisualize.Cards[idx];


            //set values
            string cardName = openCards ? card.Nid : "";
            Color cardColor = openCards ? ColorManager.instance.Char2Color(card.color) : Color.white;

            newCard.Find("cardName").GetComponent<TextMeshProUGUI>().text = cardName;
            newCard.Find("cardColor").GetComponent<Image>().color = cardColor;
        }
        cardInfoText.text = deckToVisualize.DeckName + ": " + deckToVisualize.NumCards;
    }

    void SetOpenCards(bool b) {
        openCards = b;
        VisualizeDeck();
    }

    // queries



    // other

}