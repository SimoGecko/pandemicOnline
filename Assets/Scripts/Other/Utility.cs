// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

////////// DESCRIPTION //////////

public static class Utility {
    // --------------------- MATH METHODS ---------------------


    public static Vector3 CamRaycast() {
        Plane plane = new Plane(Vector3.up, 0);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter;
        if (plane.Raycast(ray, out enter)) {
            return ray.GetPoint(enter);
        }
        return Vector3.zero;
    }

    public static T FindMin<T>(List<T> list, System.Func<T, float> f) {
        float best = float.MaxValue;
        T result = default(T);
        for (int i = 0; i < list.Count; i++) {
            float d = f(list[i]);
            if (d < best) {
                best = d;
                result = list[i];
            }
        }
        return result;
    }

    public static Vector2 RoundTo2(Vector3 pos, int digits = 2) {
        return new Vector2((float)Math.Round(pos.x, 2), (float)Math.Round(pos.z, 2));
    }

    public static Vector2 OnUnitCircle() {
        float phi = RandomValue * 2 * Mathf.PI;
        return new Vector2(Mathf.Cos(phi), Mathf.Sin(phi));
    }

    public static Vector3 To3(this Vector2 v) { return new Vector3(v.x, 0, v.y); }
    public static Vector2 To2(this Vector3 v) { return new Vector2(v.x, v.z); }

    // --------------------- CUSTOM METHODS ----------------

    //converts an object to a string in the form field1,field2,...
    public static string Serialize<T>(T obj) {
        string result = "";
        foreach (var fi in typeof(T).GetFields()) {
            result += fi.GetValue(obj).ToString() + ",";
        }
        return result;
    }

    //takes a string in the above format and creates an object of the correct type from it
    public static T Deserialize<T>(string s) where T : new() {
        s = RemoveSpaceAndTabs(s);
        string[] values = s.Split(',');
        T result = new T();
        int v = 0;
        foreach (var fi in typeof(T).GetFields()) {
            var val = System.Convert.ChangeType(values[v++], fi.FieldType);
            fi.SetValue(result, val);
        }
        return result;
    }

    public static int[] LinearArray(int n) {
        int[] result = new int[n];
        for (int i = 0; i < n; i++) result[i] = i;
        return result;
    }

    public static void Shuffle<T>(this IList<T> list) {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = Random(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static string RemoveSpaceAndTabs(this string s) {
        return s.Replace(" ", "").Replace("\t", "");
    }

    public static string[] GetLines(this string content) {
        return content.Split(new[] { System.Environment.NewLine, "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
    }


    public static int[] Permutation(int n) {
        int[] result = LinearArray(n);
        result.Shuffle();
        return result;
    }

    public static string GetAlphanumericRandomString(int n) {
        const string glyphs = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; //length: 62
        //6 letters space: 56 bn
        string result = "";
        for (int i = 0; i < n; i++) {
            int randomIdx = Random(0, glyphs.Length);
            result += glyphs[randomIdx];
        }
        return result;
    }



    //--------------------------------- STAMPS ---------------------------------

    public static string DateTimeStamp(DateTime value) {
        return value.ToString("dd/MM/yy_HH:mm:ss");
    }


    public static string HourStamp(TimeSpan value) {
        return HourStamp(new DateTime(value.Ticks));
    }
    public static string HourStamp(DateTime value) {
        return value.ToString("HH:mm:ss");
    }
    public static string HourStamp(float secs) {
        TimeSpan t = TimeSpan.FromSeconds(secs);
        return string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
    }


    public static string MinuteStamp(TimeSpan value) {
        return MinuteStamp(new DateTime(value.Ticks));
    }
    public static string MinuteStamp(DateTime value) {
        return value.ToString("mm:ss");
    }
    public static string MinuteStamp(float secs) {
        TimeSpan t = TimeSpan.FromSeconds(secs);
        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }


    //--------------------------------- RANDOM ---------------------------------
    private static System.Random rng = new System.Random();

    public static void SetRandomSeed(int seed) {
        rng = new System.Random(seed);
    }

    public static int Random(int min, int max) { // returns random int in range [min, max[
        Debug.Assert(min < max, "min should be smaller than max");
        return min + rng.Next(max - min);
    }
    public static float RandomValue { get { return (float)rng.NextDouble(); } }
}