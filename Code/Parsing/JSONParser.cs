using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;

public static class JSONParser {

    private static string rawJSON;

    // entry point
    public static void UpdateLevelData() {
        GetOnlineData();
    }

    // global Property. Certainly not the safest way, but this will do for this example.
    public static List<LevelData> LevelsAvailable
    {
        get; set;
    }

    private static void GetOnlineData() {
        //dirty
        ServicePointManager.CertificatePolicy = new HTTPSHandshake();
        WebClient fromWeb = new WebClient();
        Stream fileContent = fromWeb.OpenRead(@"https://raw.githubusercontent.com/dranner-bgt/lights-out-assignment/master/levels/lights-out-levels.json");
        StreamReader reader = new StreamReader(fileContent);

        try {
            rawJSON = reader.ReadToEnd();
            FillList();
        }
        catch {
            rawJSON = "No Data Was Parsed...";
            Debug.Log(rawJSON);
        }

        fileContent.Close();
        reader.Close();
        fromWeb.Dispose();
    }

    private static void FillList() {
        List<LevelData> newList = new List<LevelData>();
        LevelData[] levels = GlobalHelpers.getJsonArray<LevelData>(rawJSON);
        for (int i = 0; i < levels.Length; i++) {
            newList.Add(levels[i]);
        }
        LevelsAvailable = newList;
    }
}
