// (c) Simone Guggiari 2018

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

////////// DESCRIPTION //////////

public class InterfaceManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public


    // private
    List<Link> links = new List<Link>();


    // references
    [Header("Links")]
    public RectTransform playerArea;
    public RectTransform diseaseArea;
    public DeckVisualizer playerDeck, playerDeckDiscard, infectionDeck, infectionDeckDiscard;
    public TextMeshProUGUI outbreakText, infectionText, rateText, playtimeText;

    [Header("Prefabs")]
    public GameObject playerUI;
    public GameObject diseaseUI;

    public static InterfaceManager instance;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        if (instance != null) Destroy(this);
        instance = this;
    }


    void Start() {
        //LINK STUFF    
    }

    void Update() {
        UpdateUI();
    }

    // --------------------- CUSTOM METHODS ----------------


    // commands
    void UpdateUI() {
        foreach (Link l in links) {
            l.textComp.text = l.value();
        }
    }

    public void LinkEverything() {
        SetupPlayers();
        SetupDecks();
        SetupDiseases();
        SetupInfection();
        SetupGameInfo();
    }

    void SetupPlayers() {
        for (int i = 0; i < PlayerManager.instance.numPlayers; i++) {
            GameObject newPlayerUI = Instantiate(playerUI, playerArea);
            Player p = Player.Get(i);
            newPlayerUI.transform.Find("playerColor").GetComponent<Image>().color = p.Color;
            newPlayerUI.transform.Find("playerName").GetComponent<TextMeshProUGUI>().text = p.Nid;
            AddLink(newPlayerUI.transform.Find("playerInfo").GetComponent<TextMeshProUGUI>(), p.GetStatus);
            newPlayerUI.transform.Find("personalDeck").GetComponent<DeckVisualizer>().SetDeck(p.personalDeck, true);
        }
    }

    void SetupDecks() {
        playerDeck.SetDeck(PlayerManager.instance.playerDeck, false);
        playerDeckDiscard.SetDeck(PlayerManager.instance.playerDiscardDeck, true);

        infectionDeck.SetDeck(DiseaseManager.instance.InfectionDeck, false );
        infectionDeckDiscard.SetDeck(DiseaseManager.instance.InfectionDiscardDeck, true);
    }

    void SetupDiseases() {
        foreach (Disease d in DiseaseManager.instance.AllDiseases) {
            GameObject newDiseaseUI = Instantiate(diseaseUI, diseaseArea);

            newDiseaseUI.transform.Find("color").GetComponent<Image>().color = ColorManager.instance.Char2Color(d.Color);
            newDiseaseUI.transform.Find("name").GetComponent<TextMeshProUGUI>().text = d.Nid;
            AddLink(newDiseaseUI.transform.Find("number").GetComponent<TextMeshProUGUI>(), () => d.NumAvailableCubes.ToString());
            AddLink(newDiseaseUI.transform.Find("status").GetComponent<TextMeshProUGUI>(), d.StatusString);
        }
    }

    void SetupInfection() {
        AddLink(outbreakText, () => DiseaseManager.instance.OutbreakNum.ToString());
        AddLink(infectionText, () => DiseaseManager.instance.InfectionNum.ToString());
        AddLink(rateText, () => DiseaseManager.instance.InfectionRate.ToString());
    }

    void SetupGameInfo() {
        AddLink(playtimeText, () => Utility.HourStamp(GameManager.instance.GameTimer));
    }


    void AddLink(TextMeshProUGUI t, Func<string> value) {
        links.Add(new Link(t, value));
    }


    // queries



    // other
    public class Link {
        public TextMeshProUGUI textComp;
        public Func<string> value;

        public Link(TextMeshProUGUI textComp, Func<string> value) {
            this.textComp = textComp;
            this.value = value;
        }
    }

}