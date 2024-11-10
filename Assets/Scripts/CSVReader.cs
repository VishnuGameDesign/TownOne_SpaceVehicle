using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class CSVReader : MonoBehaviour
{
    //Singleton
    public static CSVReader Instance { get; private set; }

    public List<Dictionary<string, string>> ConvertedData => convertedData;

    [SerializeField] private string googleSheetDocId = "";
    private string url;

    //list of missions in Google Sheet, Key = sheet header, Value = corresponding row value
    public List<Dictionary<string, string>> convertedData = null;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } 
    }

    [ContextMenu ("InitiateDownloadCSV")]
    public void InitiateDownloadSheet()
    {
        url = "https://docs.google.com/spreadsheets/d/" + "182AaDGhtpIidUEC5hetgHETvpKIBz94EoDn4ybnEOuc" + "/export?format=csv";

        StartCoroutine(DownloadSheet(DownloadFollowup));
    }

    private IEnumerator DownloadSheet(System.Action<string> OnDownloadFinished)
    {
        yield return new WaitForEndOfFrame();

        string downloadedData = null;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if(webRequest.result == UnityWebRequest.Result.Success)
            {
                downloadedData = webRequest.downloadHandler.text;
            }
            else
            {
                Debug.LogError("Download link error!");
            }
        }

        OnDownloadFinished(downloadedData);
    }

    private void DownloadFollowup(string data)
    {
        if(data == null)
        {
            Debug.LogError("Unable to download or retrieve data!");
            return;
        }

        Debug.Log("DOWNLOAD SUCCESS!!!");
        convertedData = CSVConverter.Read(data); 
        Debug.Log(convertedData[1]["Line"]);
    }
}

public class CSVConverter
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, string>> Read(string csvText)
    {
        var list = new List<Dictionary<string, string>>();

        var lines = Regex.Split(csvText, LINE_SPLIT_RE);

        if (lines.Length <= 1) 
            return list;

        var header = Regex.Split(lines[0], SPLIT_RE);

        //Parse value as string, int, or float
        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);

            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, string>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");

                entry[header[j]] = value;
            }
            list.Add(entry);
        }
        return list;
    }
}