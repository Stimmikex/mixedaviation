using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mountains
{
    [System.Serializable]
    public class Properties
    {
        public string OBJEKTART;
        public string OBJEKTKLAS;
        public double HOEHE;
        public string NAME;
        public string STATUS;
        public string SPRACHCODE;
        public string NAMEN_TYP;
        public double xcoord;
        public double ycoord;
        public double zcoord;
    }

    [System.Serializable]
    public class Geometry
    {
        public string type;
        public List<double> coordinates;
    }

    [System.Serializable]
    public class Feature
    {
        public string type;
        public Properties properties;
        public Geometry geometry;
    }

    [System.Serializable]
    public class Features
    {
        public List<Feature> features;
    }

    public class MountainUtility
    {
        public static Features GetFromJSON(string jsonData)
        {
            return JsonUtility.FromJson<Features>(jsonData);
        }
    }
}
