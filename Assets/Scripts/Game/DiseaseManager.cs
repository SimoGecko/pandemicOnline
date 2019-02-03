// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

////////// controls infection, outbreak, epidemics //////////

public class DiseaseManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    //data
    readonly static int[] infectionsPerRate = new int[] { 2, 2, 2, 3, 3, 4, 4 };
    readonly static int[] numBaseCubes = new int[] { 3, 3, 3, 2, 2, 2, 1, 1, 1 };

    readonly static char[] diseaseColors = new char[] { 'y', 'r', 'b', 'k' };
    readonly static string[] diseaseNids = new string[] { "y", "r", "b", "k" };

    // private
    public int InfectionNum { get; private set; }
    public int OutbreakNum { get; private set; }

    public Deck InfectionDeck { get; private set; }
    public Deck InfectionDiscardDeck { get; private set; }

    Dictionary<string, Disease> diseaseDic = new Dictionary<string, Disease>();

    //for outbreaks
    bool firstOutbreak;
    List<string> outbreakCities = new List<string>();

    // references
    public static DiseaseManager instance;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        if (instance != null) Destroy(this);
        instance = this;
    }


    // --------------------- CUSTOM METHODS ----------------


    // commands
    /*
    public void Setup() {
        SetupMarkers();
        CreateDiseases();
        CreateDecks();
        //BaseInfect();
    }*/

    public void SetupMarkers() {
        InfectionNum = 0;
        OutbreakNum = 0;
        //place them on the board
    }

    public void CreateDiseases() {
        //setup disease
        int numDiseases = diseaseNids.Length;
        for (int i = 0; i < numDiseases; i++) {
            Disease newDisease = new Disease(diseaseNids[i], diseaseColors[i]); // auto setup
            //newDisease.InstantiateCubesAndMarkers();
            diseaseDic.Add(newDisease.Nid, newDisease);
        }
    }

    public void CreateDecks() {
        InfectionDeck = new Deck("Infection Deck");
        foreach (City c in Board.instance.AllCities) {
            InfectionDeck.AddNew(c.Nid, c.color);
        }
        InfectionDeck.Shuffle();
        InfectionDiscardDeck = new Deck("Infection Discard Deck");
    }


    public void BaseInfect() {
        for (int i = 0; i < numBaseCubes.Length; i++) {

            Card infectCard = InfectionDeck.RemoveTop();
            InfectionDiscardDeck.AddBottom(infectCard);

            InfectCommand(infectCard.Nid, amount: numBaseCubes[i]);
            //for (int j = 0; j < numBaseCubes[j]; j++) { }
            //Infect(infectCard.Nid, numBaseCubes[i]);
        }
    }



    //--------------------- commands
    #region commands
    void InfectCommand(string cityNid, string diseaseNid = "", int amount = 1) {
        if (!NetworkManager.instance.IsHost) return;

        if (string.IsNullOrEmpty(diseaseNid)) diseaseNid = DiseaseNidFromColor(City.Get(cityNid).color);

        string infectCommand = string.Format("infect {0} {1} {2}", cityNid, diseaseNid, amount);
        NetworkManager.instance.LocalNewInput(infectCommand); // Outbreak(cityNid, diseaseNid);
    }

    void OutbreakCommand(string cityNid, string diseaseNid = "") {
        if (!NetworkManager.instance.IsHost) return;

        if (string.IsNullOrEmpty(diseaseNid)) diseaseNid = DiseaseNidFromColor(City.Get(cityNid).color);

        string outbreakCommand = string.Format("outbreak {0} {1}", cityNid, diseaseNid);
        NetworkManager.instance.LocalNewInput(outbreakCommand);
    }

    void EpidemicCommand(string cityNid) {
        if (!NetworkManager.instance.IsHost) return;

        string epidemicCommand = string.Format("epidemic {0}", cityNid);
        NetworkManager.instance.LocalNewInput(epidemicCommand);
    }
    #endregion



    public void Infect(string cityNid, string diseaseNid = "", string amount = "1") { // HACK
        int a;
        if(! int.TryParse(amount, out a)) {
            a = 1;
        }
        Infect(cityNid, diseaseNid, a);
    }


    void Infect(string cityNid, string diseaseNid = "", int amount = 1) {
        //here it checks if an outbreak should happen -> only spot that does it
        City city = City.Get(cityNid);
        if (string.IsNullOrEmpty(diseaseNid)) diseaseNid = DiseaseNidFromColor(city.color);
        Disease disease = Disease.Get(diseaseNid);

        bool mustOutbreak = (city.NumTotalDiseaseCubes + amount > 3) && !outbreakCities.Contains(cityNid);

        amount = Mathf.Min(amount, 3 - city.NumTotalDiseaseCubes);

        /*
        if (amount > 0) {
            CommandManager.instance.OutputText(string.Format("infect {0} {1} {2}", cityNid, diseaseNid, amount));
        }*/

        for (int i = 0; i < amount; i++) {
            disease.Infect(city);
        }

        if (mustOutbreak) {
            OutbreakCommand(cityNid, diseaseNid);
        }
    }


    public void EndTurnInfect() {
        int infectionsToPerform = InfectionRate;

        for (int i = 0; i < infectionsToPerform; i++) {
            Card infectCard = InfectionDeck.RemoveTop();
            InfectionDiscardDeck.AddBottom(infectCard);

            InfectCommand(infectCard.Nid);
            //Infect(infectCard.Nid, 1); // TODO command manager
        }
    }

    public void Outbreak(string cityNid, string diseaseNid) {
        //CommandManager.instance.OutputText(string.Format("outbreak {0} {1}", cityNid, diseaseNid));

        OutbreakNum++;
        //move cursor

        //mark it as outbreak not to repeat
        bool thisWasFirstOutbreak = false;
        if (!firstOutbreak) {
            firstOutbreak = true;
            thisWasFirstOutbreak = true;
        }

        outbreakCities.Add(cityNid);

        foreach (City c in City.Get(cityNid).AdjacentCities()) {
            //infect it if not outbreak yet
            if (!outbreakCities.Contains(c.Nid)) {
                InfectCommand(c.Nid, diseaseNid); // should be executed right away
                //Infect(c.Nid, 1, diseaseNid);
            }
        }

        if (thisWasFirstOutbreak) {
            //clean up
            firstOutbreak = false;
            outbreakCities.Clear();
        }
    }



    public void Epidemic(string cityNid) { // done at will
        //CommandManager.instance.OutputText(string.Format("epidemic {0} {1}", cityNid));

        //1.increase
        InfectionNum++;
        //move cursor

        //2. infect
        InfectCommand(cityNid, amount: 3);
        //for (int i = 0; i < 3; i++) { }

        //3.intensify
        InfectionDiscardDeck.Shuffle();
        InfectionDeck.JoinTop(InfectionDiscardDeck);
    }



    public void EpidemicByCard() { // done at the right moment by looking at card
        Card infectCard = InfectionDeck.RemoveBottom();
        InfectionDiscardDeck.AddBottom(infectCard);
        EpidemicCommand(infectCard.Nid);
        //Epidemic(infectCard.Nid);
    }


    // queries
    public Disease GetDisease(string Nid) {
        Debug.Assert(diseaseDic.ContainsKey(Nid), "No disease exists in dictionary with Nid " + Nid);
        if (!diseaseDic.ContainsKey(Nid)) return null;
        return diseaseDic[Nid];
    }

    public List<Disease> AllDiseases { get { return diseaseDic.Values.ToList(); } }

    public static string DiseaseNidFromColor(char color) {
        int idx = System.Array.FindIndex(diseaseColors, c => c == color);// diseaseColors.ToList().Find();
        if(idx>=0)return diseaseNids[idx];
        Debug.Log("couldn't find disease with color " + color);
        return null;
    }

    public int InfectionRate { get { return infectionsPerRate[InfectionNum]; } }


    // other

}