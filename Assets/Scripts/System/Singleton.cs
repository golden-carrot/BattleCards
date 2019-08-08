using UnityEngine;

namespace BattleCards.System {
    public class Singleton<T> : MonoBehaviour where T : Object {
        public static T Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<T>();
                }

                return _instance;
            }
        }
        private static T _instance;
    }
}

