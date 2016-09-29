using UnityEngine;
using System.Collections;

public static class GlobalHelpers {

    // To work with Unity's rather incomplete JsonUtility, I'd've suggested to wrap the levels within the JSON file provided
    // Into another, top level object called "Levels" for the same result. To save time, I borrowed this from the Unity Forums.

    public static T[] getJsonArray<T>(string json) {
        string newJson = "{ \"array\": " + json + "}";
        Debug.Log(newJson);
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        Debug.Log(wrapper.array);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T> {
        public T[] array;
    }
}