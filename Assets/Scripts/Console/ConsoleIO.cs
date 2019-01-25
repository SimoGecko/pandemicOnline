// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

////////// DESCRIPTION //////////

public class ConsoleIO : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    public int maxLines = 15;
    public bool keepConsoleFocus = true;

    // private
    string[] consoleLines;
    string lastInputText = "";

    bool wasFocused;

    // references
    CommandManager consoleCommand;
    public Text outputText;
    public InputField inputField;

    public static ConsoleIO instance;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        if (instance != null) Destroy(gameObject);
        instance = this;

        outputText.text = "";
        consoleLines = new string[maxLines];
    }

    void Start () {
        consoleCommand = GetComponent<CommandManager>();
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return)) {

            if (wasFocused && inputField.text != "") {
                Debug.Assert(!string.IsNullOrEmpty(inputField.text), "null or empty console input");

                //consoleCommand.ProcessCommand(inputField.text);
                AddToConsoleOutput(inputField.text);
                inputField.text = "";
                if (keepConsoleFocus) inputField.ActivateInputField();
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            inputField.text = lastInputText;
        }

        wasFocused = inputField.isFocused;
    }

    // --------------------- CUSTOM METHODS ----------------


    // commands


    void AddToConsoleOutput(string text) {

        string result = "";
        for (int i = 0; i < maxLines-1; i++) {
            consoleLines[i] = consoleLines[i + 1];
            if(!string.IsNullOrEmpty(consoleLines[i]))
                result += consoleLines[i]+"\n";
        }
        consoleLines[maxLines - 1] = text;
        result += text;

        lastInputText = text;

        outputText.text = result;
    }

    public void Log(string text) {
        AddToConsoleOutput(text);
    }


    // queries



    // other

}