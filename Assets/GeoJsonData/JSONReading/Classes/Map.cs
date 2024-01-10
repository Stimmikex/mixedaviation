using UnityEngine;

namespace Map
{
    [System.Serializable]
    public class MapInfo : MonoBehaviour
    {
        [SerializeField]
        public Transform mapMesh;

        [SerializeField]
        public string meshName;

        [SerializeField]
        public Vector2[] fence;

        [System.Serializable]
        public class PinPoint
        {
            [SerializeField]
            public Vector2 LatLongFactors;
            
            [SerializeField]
            public Vector2 LatLongDelta;

            [SerializeField]
            public float verticalScaleFactor;

            [SerializeField]
            public float verticalDelta;
        }

        [SerializeField]
        public PinPoint points;
    }
}
