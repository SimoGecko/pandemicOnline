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
    readonly static int[] infectionsPerRate = new int[] { 2, 2, 2, 3, 3, 4, 4 };
    readonly static int[] numBaseCubes = new int[] { 3, 3, 3, 2, 2, 2, 1, 1, 1 };

    readonly static char[] diseaseColors = new char[] { 'y', 'r', 'b', 'k' };
    readonly static string[] diseaseNids = new string[] { "diseaseYellow", "diseaseRed", "diseaseBlue", "diseaseBlack" };

    // private
    public int InfectionNum { get; private set; }
    public int OutbreakNum { get; private set; }

    List<string> alreadyOutbreakedCities;

    public Deck infectionDeck { get; private set; }
    public Deck infectionDiscardDeck { get; private set; }

    Dictionary<string, Disease> diseaseDic = new Dictionary<string, Disease>();


    bool firstOutbreak;
    List<string> outbreakCities = new List<string>();

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
    public void Setup() {
        SetupMarkers();
        CreateDiseases();
        CreateDecks();
        BaseInfect();
    }

    void SetupMarkers() {
        InfectionNum = 0;
        OutbreakNum = 0;
        //place them on the board
    }

    void CreateDiseases() {
        //setup disease
        int numDiseases = diseaseNids.Length;
        for (int i = 0; i < numDiseases; i++) {
            Disease newDisease = new Disease(diseaseNids[i], diseaseColors[i]);
            newDisease.Setup();
            diseaseDic.Add(newDisease.Nid, newDisease);
        }
    }

    void CreateDecks() {
        infectionDeck = new Deck("Infection Deck");
        foreach(City c in Board.instance.AllCities) {
            infectionDeck.Add(c.Nid, c.color);
        }
        infectionDeck.Shuffle();
        infectionDiscardDeck = new Deck("Infection Discard Deck");
    }

    


    //--------------------- commands
    void Infect(string cityNid, int amount=1, string diseaseNid = "") {
        //here it checks if an outbreak should happen -> only spot that does it
        City city = City.Get(cityNid);
        if (string.IsNullOrEmpty(diseaseNid)) diseaseNid = DiseaseNidFromColor(city.color);
        Disease disease = Disease.Get(diseaseNid);


        bool mustOutbreak = false;
        if (city.NumDiseaseCubes + amount > 3) mustOutbreak = true;

        amount = Mathf.Min(amount, 3 - city.NumDiseaseCubes);

        if(amount>0)
            ConsoleIO.instance.Log(string.Format("infect {0} {1} {2}", cityNid, diseaseNid, amount));

        for (int i = 0; i < amount; i++) {
            disease.Infect(city);
        }

        if(mustOutbreak) Outbreak(cityNid, diseaseNid);
    }

    void BaseInfect() {
        for (int i = 0; i < 9; i++) {
            Card infectCard = infectionDeck.RemoveTop();
            infectionDiscardDeck.AddBottom(infectCard);

            Infect(infectCard.Nid, numBaseCubes[i]);
        }
    }

    void EndTurnInfect() {
        int infectionsToPerform = InfectionRate;
        for (int i = 0; i < infectionsToPerform; i++) {
            Card infectCard = infectionDeck.RemoveTop();
            infectionDiscardDeck.AddBottom(infectCard);

            Infect(infectCard.Nid, 1);
        }
    }

    void Outbreak(string cityNid, string diseaseNid) {
        ConsoleIO.instance.Log(string.Format("outbreak {0} {1}", cityNid, diseaseNid));

        OutbreakNum++;
        //move cursor

        //mark it as outbreak not to repeat
        bool thisWasFirstOutbreak = false;
        if (!firstOutbreak) {
            firstOutbreak = true;
            thisWasFirstOutbreak = true;
        }

        outbreakCities.Add(cityNid);

        foreach(City c in City.Get(cityNid).AdjacentCities()) {
            //infect it if not outbreak yet
            if (!outbreakCities.Contains(c.Nid)) {
                Infect(c.Nid, 1, diseaseNid);
            }
        }

        if (thisWasFirstOutbreak) {
            //clean up
            firstOutbreak = false;
            outbreakCities.Clear();
        }
    }


    public void Epidemic() {
        //1.increase
        InfectionNum++;
        //move cursor
   
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

    public List<Disease> AllDiseases { get { return diseaseDic.Values.ToList(); } }

    public static string DiseaseNidFromColor(char color) {
        int idx = System.Array.FindIndex(diseaseColors, c => c == color);// diseaseColors.ToList().Find();
        return diseaseNids[idx];
    }

    public int InfectionRate { get { return infectionsPerRate[InfectionNum]; } }


    // other

}