// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


////////// DESCRIPTION //////////

public class ButtonCommand : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    public string memo;
    public bool needsCity = false;


    // private
    Command myCommand;


    // references
	
	
	// --------------------- BASE METHODS ------------------
	void Start () {
        Setup();   
	}
	
	void Update () {
        
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    void Setup() {
        GetComponentInChildren<Text>().text = memo;
        //ancora per una volta devi vedere come si va a fare questa nuova versione di un nuovo gioco
        //è molto divertente devo dire stare qua a prendere freddo per tutto il tempo che non ci stiamo facendo nulla di molto special e
        //alla fine scrivere in maniera semplice non mi da fastidio per quanto io debba andare da quella nuova parte a fare un nuovo giro di casa
        myCommand = CommandManager.instance.FindMatchingCommand(memo);
        GetComponent<Button>().onClick.AddListener(PerformAction);
    }

    void PerformAction() {
        //get al parameters
        //'P', 'C', 'D', 'O', 'N'

        List<string> parameters = new List<string>();
        Player player = PlayerManager.instance.CurrentPlayer;

        if (player != null) {
            parameters.Add(player.Nid);

            City city = needsCity ? null : player.CurrentCity;
            if (city != null) {
                parameters.Add(city.Nid);
                parameters.Add(city.DefaultDisease.Nid);
                Player other = null;//
                parameters.Add(other == null ? "" : other.Nid);
                parameters.Add("1");

                myCommand.InvokeAllParams(parameters.ToArray());
            }
        }
    }



    // queries
    City CityClicked() {
        return null;
    }



    // other

}