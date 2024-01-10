using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Airports 
{

    [System.Serializable]
    public class Airport
    {
        public int airport_id;
        public string airport_ident;
        public string type;
        public string name;
        public double latitude_deg;
        public double longitude_deg;
        public int elevation_ft;
        public string municipality;
        public string iata_code;
        public List<Runway> runways;
    }

    [System.Serializable]
    public class Runway
    {
        public int runway_id;
        public int length_ft;
        public int width_ft;
        public string surface;
        public bool lighted;
        public string le_ident;
        public double le_latitude_deg;
        public double le_longitude_deg;
        public double le_elevation_ft;
        public double le_heading_degT;
        public string he_ident;
        public double he_latitude_deg;
        public double he_longitude_deg;
        public double he_elevation_ft;
        public double he_heading_degT;
    }

    [System.Serializable]
    public class AirportList
    {
        public List<Airport> airportlist;
    }

    [System.Serializable]   
    public class AirportUtility
    {
        public static AirportList GetAirportsFromJSON(string jsonData)
        {
            return JsonUtility.FromJson<AirportList>(jsonData);
        }
    }
}
