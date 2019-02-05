// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

////////// DESCRIPTION //////////

public class SyncedFile : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    const string websiteUrl = "https://simoneguggiari.altervista.org/pandemic_online/"; // HTTPS

    //const string privateKey = ""; // nobody should know about this

    public float refreshRate = 2f;
    public bool logConnectionTime = false;
    public bool deleteOnClose = true;

    // private
    //fName in the form "/folder/file.txt"
    //base folder: pandemic_online
    string fName;

    List<string> localFileLines; // what's on the file sofar
    List<string> toWrite; // what's to add

    // references
    public StringEvent OnNewLine;


    // --------------------- BASE METHODS ----------------


    private void OnApplicationQuit() {
        if (deleteOnClose) {
            DeleteFile();
        }
    }

    private void Update() {
        //if (Input.GetKeyDown(KeyCode.F8)) DeleteFile();
    }

    // --------------------- CUSTOM METHODS ----------------

    public void Setup(string fName) {
        this.fName = fName;
        localFileLines = new List<string>();
        toWrite = new List<string>();

        StartCoroutine("UploadRoutine");
        StartCoroutine("DownloadRoutine");
    }

    void DeleteFile() {
        if (NetworkManager.instance.IsHost) {
            StartCoroutine("DeleteRoutine");
        }
    }

    // commands
    public void Write(string data) {
        Debug.Assert(!string.IsNullOrEmpty(data), "trying to write null string to synced file");
        Debug.Assert(toWrite != null, "write string is null");
        if (string.IsNullOrEmpty(data)) return;

        //assume a single line is written
        toWrite.Add(data);
        AddLine(data);
        //localFileLines.Add(data);

        //StartCoroutine(UploadRoutine(data + '\n'));
    }

    void AddLine(string data) {
        localFileLines.Add(data);
        if (OnNewLine != null) OnNewLine.Invoke(data);
    }



    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    void ProcessFileContent(string content) {
        //if (string.IsNullOrEmpty(content)) return;
        if (content == null) return;

        List<string> contentLines = content.Split('\n').Where(s => !string.IsNullOrEmpty(s)).ToList();

        //if (contentLines.Length == lines.Count) return; // nothing changed

        //assert that first lines are the same
        int minLength = Mathf.Min(contentLines.Count, localFileLines.Count);
        bool mismatch = false;

        for (int i = 0; i < minLength; i++) {
            if (!localFileLines[i].Equals(contentLines[i])) {
                Debug.LogWarning("line online mismatch: " + localFileLines[i]);
                mismatch = true;
            }
        }
        if (mismatch) {
            //try to resolve it
            for (int i = 0; i < minLength; i++) {
                localFileLines[i] = contentLines[i];
            }
        }


        //----
        if (localFileLines.Count > contentLines.Count) {
            //something was written but not finished uploading yet -> wait
        }
        else if (localFileLines.Count < contentLines.Count) {
            //must process these lines
            for (int j = localFileLines.Count; j < contentLines.Count; j++) {
                /*if (!string.IsNullOrEmpty(contentLines[j])) {
                    OnNewLine(contentLines[j]);
                    //add it to self
                    localFileLines.Add(contentLines[j]);
                }*/
                AddLine(contentLines[j]);
            }
        }
    }

    // queries


    // other
    IEnumerator UploadRoutine() {
        while (true) {
            yield return new WaitForSeconds(refreshRate);

            if (toWrite.Count>0) {

                DateTime uploadStartTime = DateTime.Now;

                string data = Utility.MergeLines(toWrite, true);
                toWrite.Clear();

                WWWForm form = new WWWForm();
                form.AddField("name", fName);
                form.AddField("data", data);

                WWW www = new WWW(websiteUrl + "write.php", form);
                yield return www;

                if (!string.IsNullOrEmpty(www.error)) Debug.LogError("www error: " + www.error);
                if (logConnectionTime) {
                    Debug.Log("uploaded in " + (DateTime.Now - uploadStartTime).TotalMilliseconds + "ms");
                }
            }
        }
    }

    public IEnumerator DownloadRoutine() {
        while (true) {
            yield return new WaitForSeconds(refreshRate);

            DateTime downloadStartTime = DateTime.Now;

            WWWForm form = new WWWForm();
            form.AddField("name", fName);

            WWW www = new WWW(websiteUrl + "read.php", form);
            yield return www;

            if (!string.IsNullOrEmpty(www.error)) Debug.LogError("www error: " + www.error);
            else {
                if (logConnectionTime) {
                    Debug.Log("downloaded in " + (DateTime.Now - downloadStartTime).TotalMilliseconds + "ms");
                }
                ProcessFileContent(www.text);
            }
        }
    }

    public IEnumerator DeleteRoutine() {
        WWWForm form = new WWWForm();
        form.AddField("name", fName);

        WWW www = new WWW(websiteUrl + "delete.php", form);
        yield return www;

        if (!string.IsNullOrEmpty(www.error)) Debug.LogError("www error: " + www.error);
    }


}