using UnityEngine;
using System.Collections;


public class Singleton <T> : MonoBehaviour
	where T : Component
{
	private static T _instance;
	public static T Instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType (typeof(T)) as T;
				if (_instance == null) {
					GameObject obj = new GameObject ();
					obj.hideFlags = HideFlags.HideAndDontSave;
					_instance = obj.AddComponent (typeof(T)) as T;
				}
			}
			return _instance;
		}
	}
	
	public virtual void Awake ()
	{
		DontDestroyOnLoad (this.gameObject);
		if (_instance == null) {
			_instance = this as T;
		} else {
			Destroy (gameObject);
		}
	}
}