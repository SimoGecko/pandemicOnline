﻿// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

////////// DESCRIPTION //////////

public class SyncedFile : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    const string websiteUrl = "https://simoneguggiari.altervista.org/pandemic_online/"; // HTTPS

    public float refreshRate = 2f;
    public bool log = false;
    public bool deleteOnClose = true;

    // private
    string fName;
    List<string> lines;
    public bool busyUpload { get; private set; }
    public bool busyDownload { get; private set; } // make a queue of jobs

    DateTime uploadStartTime, downloadStartTime;

    // references
    public StringEvent OnNewRemoteLine;

    private void OnApplicationQuit() {
        if(deleteOnClose)
            DeleteFile();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F8)) DeleteFile();
    }

    // --------------------- CUSTOM METHODS ----------------

    public void Setup(string fName) {
        this.fName = fName;
        lines = new List<string>();
        Write(""); // to create it
        StartCoroutine("DownloadRoutine");
    }

    void DeleteFile() {
        StartCoroutine("DeleteRoutine");

    }

    // commands
    public void Write(string data) {
        //assume a single line is written
        uploadStartTime = DateTime.Now;
        lines.Add(data);
        StartCoroutine(UploadRoutine(data + '\n'));
    }



    void ProcessFileContent(string content) {
        string[] contentLines = content.Split('\n');

        //if (contentLines.Length == lines.Count) return; // nothing changed

        //assert that first lines are the same
        int minLength = Mathf.Min(contentLines.Length, lines.Count);
        for (int i = 0; i < minLength; i++) {
            if (! lines[i].Equals(contentLines[i])) Debug.LogError("line online mismatch: " + lines[i]);
        }

        if (lines.Count > contentLines.Length) {
            //something was written but not finished uploading yet -> wait
        }else if (lines.Count < contentLines.Length) {
            //must process these lines
            for (int j = lines.Count; j < contentLines.Length; j++) {
                if (!string.IsNullOrEmpty(contentLines[j])) {
                    OnNewRemoteLine(contentLines[j]);
                    //add it to self
                    lines.Add(contentLines[j]);
                }
            }
        }
    }

    // queries
    


    // other
    IEnumerator UploadRoutine(string data) {
        WWWForm form = new WWWForm();
        form.AddField("name", fName);
        form.AddField("data", data);

        WWW www = new WWW(websiteUrl + "write.php", form);
        yield return www;

        if (!string.IsNullOrEmpty(www.error)) Debug.Log("www error: " + www.error);
        if (log) {
            Debug.Log("uploaded in " + (DateTime.Now - uploadStartTime).TotalMilliseconds + "ms");

        }
    }

    public IEnumerator DownloadRoutine() {
        while (true) {
            downloadStartTime = DateTime.Now;

            WWWForm form = new WWWForm();
            form.AddField("name", fName);

            WWW www = new WWW(websiteUrl + "read.php", form);
            yield return www;

            if (!string.IsNullOrEmpty(www.error)) Debug.Log("www error: " + www.error);
            else {
                if (log) {
                    Debug.Log("downloaded in " + (DateTime.Now - downloadStartTime).TotalMilliseconds + "ms");

                }
                ProcessFileContent(www.text);
            }

            yield return new WaitForSeconds(refreshRate);
        }
    }

    public IEnumerator DeleteRoutine() {
        WWWForm form = new WWWForm();
        form.AddField("name", fName);

        WWW www = new WWW(websiteUrl + "delete.php", form);
        yield return www;

        if (!string.IsNullOrEmpty(www.error)) Debug.Log("www error: " + www.error);
    }


}