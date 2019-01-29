// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


////////// DESCRIPTION //////////

public delegate void StringEvent(string v);

public class ConsoleIO : MonoBehaviour {
    // --------------------- VARIABLES ---------------------


    // public
    public int maxLines = 15;
    public bool startFocus = false;
    public bool keepInputFocus = true;
    public bool toLower = false;
    public bool autoAddToOutput = false;

    // private
    string[] consoleLines;
    string lastTypedLine = "";

    bool wasFocused;

    // references
    public GameObject consoleUI;
    public Text outputText;
    public InputField inputField;
    public Toggle showConsoleToggle;

    [HideInInspector]
    public event StringEvent OnSubmitString;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        //OnSubmitString.RemoveAllListeners();
    }

    void Start () {
        Setup();
    }
	
	void Update () {
        if (wasFocused) {
            if (Input.GetKeyDown(KeyCode.Return)) {

                if (!string.IsNullOrEmpty(inputField.text)) {
                    //get text
                    lastTypedLine = inputField.text;
                    string text = inputField.text;
                    if(toLower) text = text.ToLower();

                    //invoke
                    if(autoAddToOutput) AddToConsoleOutput(text);
                    if (OnSubmitString != null) OnSubmitString(text);
                    //OnSubmitString.Invoke(text);

                    //reset
                    inputField.text = "";
                    if (keepInputFocus) inputField.ActivateInputField();
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                inputField.text = lastTypedLine;
                inputField.caretPosition = inputField.text.Length; // put cursor at end
            }
        }

        wasFocused = inputField.isFocused;
    }

    // --------------------- CUSTOM METHODS ----------------


    // commands
    void Setup() {
        outputText.text = "";
        consoleLines = new string[maxLines];
        if (startFocus) inputField.ActivateInputField();

        showConsoleToggle.onValueChanged.AddListener(ToggleConsoleVisibility);
    }

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
