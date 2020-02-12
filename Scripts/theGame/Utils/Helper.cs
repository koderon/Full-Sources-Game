using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public static class Helper
{
    public static T Create<T>(GameObject go, Transform parent = null) where T : Component
    {
        var g = Object.Instantiate(go);
        if (g != null)
        {
            if (parent != null)
                g.transform.SetParent(parent, false);

            g.transform.localScale = Vector3.one;
            g.transform.localPosition = Vector3.zero;
        }

        return g != null ? g.GetComponent<T>() : null;
    }

    public static T Clone<T>(GameObject go, Transform parent = null) where T : Component
    {
        if (parent == null)
            parent = go.transform.parent;
        var clone = Object.Instantiate(go);
        if (clone != null)
        {
            clone.transform.localScale = go.transform.localScale;
            clone.transform.SetParent(parent, false);
            clone.transform.localPosition = go.transform.localPosition;
        }
        return (clone != null) ? clone.GetComponent<T>() : null;
    }

    public static GameObject Clone(GameObject go, Transform parent)
    {
        var clone = Object.Instantiate(go);
        if (clone != null)
        {
            clone.transform.SetParent(parent, false);
        }
        return clone;
    }

    private static long _serverDelta;
    public static long ServerDelta
    {
        get { return _serverDelta; }
        set { _serverDelta = value - (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds; }
    }

    public static long TimeStamp
    {
        get
        {
            var unixTimestamp = _serverDelta + (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return unixTimestamp;
        }
    }

    public static long ConvertDateTimeToSec(DateTime time)
    {
        var t = (long)time.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        return t;
    }

    public static DateTime ConvertSecontToDateTime(long sec)
    {
        var ts = TimeSpan.FromSeconds(sec);
        var dt = new DateTime(1970, 1, 1) + ts;
        return dt;
    }

    public static long TimeStampMs
    {
        get
        {
            var unixTimestamp = _serverDelta + (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            return unixTimestamp;
        }
    }

    public static string GetDottedTimeSpan(long uts, bool needHours = true, bool needSeconds = true)
    {
        if (uts < TimeStamp)
            uts = (int)TimeStamp;
        var value = new TimeSpan(0, 0, (int)(uts - TimeStamp));
        string duration = "";
        if (value.Hours > 0 || needHours)
            duration += (value.Hours + value.Days * 24).ToString("00") + ":";
        duration += value.Minutes.ToString("00");
        if (needSeconds)
            duration += ":" + value.Seconds.ToString("00");
        //string duration = (value.Hours + value.Days * 24).ToString("00") + ":" + value.Minutes.ToString("00") + ":" + value.Seconds.ToString("00");
        return duration;
    }

    public static string GetDottedTime(int uts, bool needHours = true, bool needSeconds = true)
    {
        var value = new TimeSpan(0, 0, uts);
        string duration = "";
        if (value.Hours > 0 || needHours)
            duration += (value.Hours + value.Days * 24).ToString("00") + ":";
        duration += value.Minutes.ToString("00");
        if (needSeconds)
            duration += ":" + value.Seconds.ToString("00");

        return duration;
    }

    public static string ConvertSecToDottedTime(int time, bool drawDotted = true)
    {
        var m = (int)(time / 60);
        var s = time - (m * 60);
        var t = "";
        t += m > 0 ? "" + m : "";
        t += m > 0 ? (drawDotted ? ":" : " ") : "";
        t += (m > 0 && s < 10) ? "0" + s : "" + s;
        return t;
    }

    public static int ToMod(int p, int q)
    {
        q = Math.Abs(q);
        var result = p % q;
        if (result > 0)
            result = p + (q - result);
        return result;
    }

    private static int _syncRnd = UnityEngine.Random.Range(0, int.MaxValue);

    private static int GetRandom()
    {
        _syncRnd = (115245 * _syncRnd + 123451);
        return Math.Abs(_syncRnd);
    }

    public static void SetRandomize(int r) { _syncRnd = r; }

    public static int SyncRand(int min, int max = 0)
    {
        if (max == 0)
        {
            return GetRandom() % min;
        }

        int val = GetRandom() % (max - min) + min;
        return val;
    }

    public static float SyncRand(float min, float max)
    {
        if (IsEqual(min, max))
            return min;
        int dist = (int)(10000.0f * (max - min));
        return (GetRandom() % dist) / 10000.0f + min;
    }

    public static List<int> GetSyncRandIndexInList(int count, int countRand)
    {
        var indexs = new List<int>();
        for (int i = 0; i < count; i++)
        {
            indexs.Add(i);
        }

        var rIndex = new List<int>();
        while (rIndex.Count < countRand)
        {
            var r = Helper.SyncRand(0, indexs.Count);
            var index = indexs[r];
            indexs.RemoveAt(r);

            if (indexs.Count <= 0)
                break;

            rIndex.Add(index);
        }

        return rIndex;
    }

    public static string ConvertRegion(int region)
    {
        var regions = new string[6] { "europe", "asia", "america", "africa", "oceania", "world" };
        return regions[region - 1];
    }

    public static bool IsEqual(float v0, float v1, float epsilon = 0.001f)
    {
        return Mathf.Abs(v0 - v1) < epsilon;
    }

    public static string ConvertNumberToGoldString(int number)
    {
        return number > 0 ? string.Format("{0:#,#}", number) : "0";
    }

    public static string ColoraizeText(string text, string color)
    {
        return "<color=" + color + ">" + text + "</color>";
    }

    public static Sprite ConvertTexture2DToSprite(Texture2D tex)
    {
        return tex == null ? null : Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    public static Vector3 ConvertStringToVector3(string text)
    {
        var v = new Vector3();
        var words = text.Split(',');
        if (words.Length >= 1)
            v.x = float.Parse(words[0]);
        if (words.Length >= 2)
            v.y = float.Parse(words[1]);
        if (words.Length >= 3)
            v.z = float.Parse(words[2]);

        return v;
    }

    public static string CheckJson(string lngResText)
    {
        var textMass = lngResText.Split('\n');

        for (var i = 0; i < textMass.Length; i++)
        {
            var t = textMass[i];
            var index = t.IndexOf("//", StringComparison.Ordinal);
            if(index == -1)
                continue;

            t = t.Substring(0, index);
            textMass[i] = t;
        }

        var newText = "";
        foreach (var s in textMass)
        {
            newText += s + "\n";
        }

        return newText;
    }

    public static Color GetColorShadow(bool isDay)
    {
        return isDay ? new Color(0, 0, 0, 0.3f) : new Color(1, 1, 1, 0.3f);
    }

    public static void GotoURL(string url)
    {
        Application.OpenURL(url);
    }
}