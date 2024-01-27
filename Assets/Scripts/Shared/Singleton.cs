using UnityEngine;
using System.Collections;

/*
 * Generic Singleton class, adapted from mstevenson "MonoBehaviourSingleton" class
 * Original code: https://gist.github.com/mstevenson/4325117
 * Date of Retreival: 26/01/2024
 */
namespace AhmadAllahham.Shared
{
    public class Singleton<T> : MonoBehaviour
    where T : Component
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var objs = FindObjectsOfType(typeof(T)) as T[];
                    if (objs.Length > 0)
                        _instance = objs[0];
                    if (objs.Length > 1)
                    {
                        Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
                    }
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.hideFlags = HideFlags.HideAndDontSave;
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }
    }
}