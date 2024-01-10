using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeoObject
{
    [System.Serializable]
    public class Geometry
    {
        public string type;
        public List<object> coordinates;
    }

    [System.Serializable]
    public class Feature
    {
        public string type;
        public Dictionary<string, object> properties;
        public Geometry geometry;
    }

    [System.Serializable]
    public class Features
    {
        public List<Feature> features;
    }

    public class GeoJSONUtility
    {
        public static Features GetFromJSON(string jsonData)
        {
            return JsonUtility.FromJson<Features>(jsonData);
        }
    }
}
