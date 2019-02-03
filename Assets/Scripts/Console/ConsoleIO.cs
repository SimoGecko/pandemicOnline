// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


////////// deals with reading output and printing output to console //////////

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
    GameObject consoleUI;
    Text outputText;
    InputField inputField;
    Toggle showConsoleToggle;

    public event StringEvent OnInput;


    // --------------------- BASE METHODS ------------------
    private void Awake() {
        Setup();

    }

    void Start() {

    }

    void Update() {
        if (wasFocused) {
            if (Input.GetKeyDown(KeyCode.Return)) InputConsole();
            if (Input.GetKeyDown(KeyCode.UpArrow)) FillPreviousLine();
        }
        wasFocused = inputField.isFocused;
    }

    // --------------------- CUSTOM METHODS ----------------


    // commands
    void Setup() {
        //find refs
        consoleUI = transform.Find("background").gameObject;
        outputText = consoleUI.GetComponentInChildren<Text>();
        inputField = consoleUI.GetComponentInChildren<InputField>();
        showConsoleToggle = GetComponentInChildren<Toggle>();

        //init them
        outputText.text = "";
        consoleLines = new string[maxLines];
        showConsoleToggle.onValueChanged.AddListener(b => consoleUI.SetActive(b));
        if (startFocus) inputField.ActivateInputField();
    }


    void InputConsole() {
        if (!string.IsNullOrEmpty(inputField.text)) {
            //get text
            lastTypedLine = inputField.text;
            string text = inputField.text;
            if (toLower) text = text.ToLower();

            //invoke
            if (autoAddToOutput) OutputConsole(text);
            if (OnInput != null) OnInput(text);

            //reset
            inputField.text = "";
            if (keepInputFocus) inputField.ActivateInputField();
        }
    }

    void FillPreviousLine() {
        inputField.text = lastTypedLine;
        inputField.caretPosition = inputField.text.Length; // put cursor at end
    }



    public void OutputConsole(string text) {
        string result = "";
        for (int i = 0; i < maxLines - 1; i++) {
            consoleLines[i] = consoleLines[i + 1];
            if (!string.IsNullOrEmpty(consoleLines[i])) {
                result += consoleLines[i] + "\n";
            }
        }
        consoleLines[maxLines - 1] = text;
        result += text;

        outputText.text = result;
    }



    // queries



    // other

}
