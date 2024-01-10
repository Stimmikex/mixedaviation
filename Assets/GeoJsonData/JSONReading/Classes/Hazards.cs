using System.Collections.Generic;
using UnityEngine;


namespace HazardsFunctions
{
    public class Hazards
    {
        [System.Serializable]
        public class Properties
        {
            public string NAME;
            public string AIRPORT;
            public string TYPE;
            public string STATUS;
            public string STYLE;
            public List<FeatureProperties> properties;
        }

        [System.Serializable]
        public class FeatureProperties
        {
            public int REC_NR;
            public double GEO_LON;
            public double GEO_LAT;
            public double TOP_AMSL;
            public double HEIGHT_AGL;
            public string RADIUS;
            public string WEF;
            public string MARKING;
            public string LIGHTING;
            public string GROUP;
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
    }

    public class HazardUtility
    {
        public static Hazards.Features GetFromJSON(string jsonData)
        {
            return JsonUtility.FromJson<Hazards.Features>(jsonData);
        }
    }
}
