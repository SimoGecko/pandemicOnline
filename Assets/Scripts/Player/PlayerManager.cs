// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

////////// generates and manages players as well as player decks //////////

public class PlayerManager : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    //public Color[] playerColors;

    public int numPlayers = 2;

    readonly static int[] numCardsPerPlayerNumber = new int[] { 0, 6, 4, 3, 2, 2 };
    readonly static int[] epidemiCardsPerDifficulty = new int[] { 4, 5, 6 };

    // private
    Dictionary<string, Player> playerDic = new Dictionary<string, Player>();
    List<Player> players = new List<Player>();

    public Deck playerDeck {get;private set;}
    public Deck playerDiscardDeck { get; private set;}
    

    int currentPlayerTurn;

    public bool RunOutOfCards { get; private set; }

    // references
    //public Player playerPrefab;
    public static PlayerManager instance;

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
        CreatePlayers();
        CreateDecks();
        GiveInitialCards();
        SetEpidemicAndShuffle();
        PlacePlayers();
        SetStartingPlayer();
    }

    public void StartTurn() {
        CurrentPlayer.StartTurn();
    }

    #region setup
    void CreatePlayers() {
        for (int i = 0; i < numPlayers; i++) {
            //Color playerColor = i < playerColors.Length ? playerColors[i] : Color.white;
            Player newPlayer = new Player(i, Color.white, "p"+i); //Instantiate(playerPrefab);
            newPlayer.Setup();

            playerDic.Add(newPlayer.Nid, newPlayer);
            players.Add(newPlayer);
        }
    }
    void CreateDecks() {
        playerDeck = new Deck("Player Deck");
        foreach (City c in Board.instance.AllCities) {
            playerDeck.Add(c.Nid, c.color);
        }
        playerDeck.Shuffle();
        playerDiscardDeck = new Deck("Player Discard Deck");
    }

    void GiveInitialCards() {
        int numCardsPerPlayer = numCardsPerPlayerNumber[numPlayers];
        for (int i = 0; i < numPlayers; i++) {
            players[i].GiveInitialDeck(playerDeck.Split(numCardsPerPlayer, "Personal Deck"));
        }
    }

    void SetEpidemicAndShuffle() {
        int numEpidemics = epidemiCardsPerDifficulty[(int)GameManager.instance.difficulty];
        Deck[] piles = playerDeck.SplitInto(numEpidemics);
        for (int i = 0; i < numEpidemics; i++) {
            piles[i].Add("epidemic", 'g'); // attention, deal with this!
            piles[i].Shuffle();
            playerDeck.JoinTop(piles[i]);
        }
    }

    void PlacePlayers() {
        for (int i = 0; i < numPlayers; i++) {
            players[i].Move(Board.instance.startingCity);
        }
    }

    void SetStartingPlayer() {
        currentPlayerTurn = Random.Range(0, numPlayers);
    }

    #endregion

    public Card DrawCard() {
        if (playerDeck.NumCards == 0) {
            RunOutOfCards = true;
            return null;
        } else {
            return playerDeck.RemoveTop();
        }
    }
    public void Discard(Card card) {
        playerDiscardDeck.AddBottom(card);
    }

    

    public void IncreaseTurn() {
        currentPlayerTurn = (currentPlayerTurn + 1) % numPlayers;
    }
    public Player CurrentPlayer { get { return players[currentPlayerTurn]; } }

    // queries
    public Player GetPlayer(string Nid) {
        Debug.Assert(playerDic.ContainsKey(Nid), "No player exists in dictionary with Nid " + Nid);
        if (!playerDic.ContainsKey(Nid)) return null;
        return playerDic[Nid];
    }
    public Player GetPlayer(int id) {
        Debug.Assert(0 <= id && id < players.Count, "Player index out of range " + id);
        return players[id];
    }

    public List<Player> AllPlayers { get { return playerDic.Values.ToList(); } }



    // other

}