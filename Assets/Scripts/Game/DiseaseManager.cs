// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

////////// DESCRIPTION //////////

public class DiseaseManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    //data
    readonly int[] infectionsPerRate = new int[] { 2, 2, 2, 3, 3, 4, 4 };
    readonly int[] numBaseCubes = new int[] { 3, 3, 3, 2, 2, 2, 1, 1, 1 };

    readonly char[] diseaseColors = new char[] { 'y', 'r', 'b', 'k' };
    readonly string[] diseaseNids = new string[] { "diseaseYellow", "diseaseRed", "diseaseBlue", "diseaseBlack" };

    // private
    public int InfectionRate { get; private set; }
    public int OutbreakNum { get; private set; }

    List<string> alreadyOutbreakedCities;

    Deck infectionDeck, infectionDiscardDeck;
    Dictionary<string, Disease> diseaseDic = new Dictionary<string, Disease>();


    // references
    public static DiseaseManager instance;

    // --------------------- BASE METHODS ------------------
    private void Awake() {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    void Start () {
        
	}
	
	void Update () {
        
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    void Setup() {
        InfectionRate = 0;
        OutbreakNum = 0;

        CreateDiseases();

    }

    void CreateDiseases() {
        //setup disease
    }

    void CreateDecks() {
        infectionDeck = new Deck();
        foreach(City c in Board.instance.AllCities) {
            infectionDeck.Add(c.Nid, c.color);
        }
        infectionDeck.Shuffle();
        infectionDiscardDeck = new Deck();
    }

    void BaseInfect() {
        for (int i = 0; i < 9; i++) {
            Card infectCard = infectionDeck.RemoveTop();
            infectionDiscardDeck.AddBottom(infectCard);

            Infect(infectCard.Nid, numBaseCubes[i]);
        }
    }

    void Infect(string cityNid, int amount=1, string diseaseNid = "") {
        //here it checks if an outbreak should happen
    }

    void Outbreak(string cityNid, string diseaseNid = "") {

    }

    void RecursiveOutbreak() {

    }

    void Epidemic() {
        //1.increase
        InfectionRate++;
        //2. infect
        Card infectCard = infectionDeck.RemoveBottom();
        infectionDiscardDeck.AddBottom(infectCard);
        Infect(infectCard.Nid, 3);

        //3.intensify
        infectionDiscardDeck.Shuffle();
        infectionDeck.JoinTop(infectionDiscardDeck);
    }


    // queries
    public Disease GetDisease(string Nid) {
        Debug.Assert(diseaseDic.ContainsKey(Nid), "No disease exists in dictionary with Nid " + Nid);
        return diseaseDic[Nid];
    }

    public List<Disease> AllDiseases {
        get {
            return diseaseDic.Values.ToList();
        }
    }


    // other

}