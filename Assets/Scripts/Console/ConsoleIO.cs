// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

////////// DESCRIPTION //////////

public class ConsoleIO : MonoBehaviour {
    // --------------------- VARIABLES ---------------------


    // public
    public int maxLines = 15;
    public bool keepInputFocus = true;
    public bool toLower = false;

    // private
    string[] consoleLines;
    string lastTypedLine = "";

    bool wasFocused;

    // references
    public GameObject consoleUI;
    public Text outputText;
    public InputField inputField;
    public Toggle showConsoleToggle;

    public StringEvent OnSubmitString;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        outputText.text = "";
        consoleLines = new string[maxLines];
    }

    void Start () {
        if(keepInputFocus) inputField.ActivateInputField();
        showConsoleToggle.onValueChanged.AddListener(ToggleConsoleVisibility);
    }
	
	void Update () {
        if (wasFocused) {
            if (Input.GetKeyDown(KeyCode.Return)) {

                if (!string.IsNullOrEmpty(inputField.text)) {
                    lastTypedLine = inputField.text;

                    string text = inputField.text;
                    if(toLower) text = text.ToLower();

                    AddToConsoleOutput(text);
                    inputField.text = "";
                    OnSubmitString.Invoke(text);
                    //CommandManager.in.ProcessCommand(lower);

                    if (keepInputFocus) inputField.ActivateInputField();
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                inputField.text = lastTypedLine;
            }
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

[System.Serializable]
public class StringEvent : UnityEvent<string> { }