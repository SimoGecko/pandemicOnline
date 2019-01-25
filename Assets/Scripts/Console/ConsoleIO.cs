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
    string lastTypedLine = "";

    bool wasFocused;

    // references
    public GameObject consoleUI;
    CommandManager consoleCommand;
    public Text outputText;
    public InputField inputField;
    public Toggle showConsoleToggle;

    public static ConsoleIO instance;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        if (instance != null) Destroy(gameObject);
        instance = this;

        outputText.text = "";
        consoleLines = new string[maxLines];
    }

    void Start () {
        if(keepConsoleFocus) inputField.ActivateInputField();

        consoleCommand = GetComponent<CommandManager>();
        showConsoleToggle.onValueChanged.AddListener(ToggleConsoleVisibility);
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return)) {

            if (wasFocused && inputField.text != "") {
                Debug.Assert(!string.IsNullOrEmpty(inputField.text), "null or empty console input");

                lastTypedLine = inputField.text;
                string lower = inputField.text.ToLower();

                AddToConsoleOutput(lower);
                inputField.text = "";
                consoleCommand.ProcessCommand(lower);

                if (keepConsoleFocus) inputField.ActivateInputField();
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            inputField.text = lastTypedLine;
        }

        wasFocused = inputField.isFocused;
    }

    // --------------------- CUSTOM METHODS ----------------


    // commands
    void ToggleConsoleVisibility(bool b) {
        consoleUI.SetActive(b);
    }


    void AddToConsoleOutput(string text) {

        string result = "";
        for (int i = 0; i < maxLines-1; i++) {
            consoleLines[i] = consoleLines[i + 1];
            if(!string.IsNullOrEmpty(consoleLines[i]))
                result += consoleLines[i]+"\n";
        }
        consoleLines[maxLines - 1] = text;
        result += text;

        outputText.text = result;
    }

    public void Log(string text) {
        AddToConsoleOutput(text);
    }


    // queries



    // other

}