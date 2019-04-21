using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace TankGame.Views
{
	public class TemplatePool : MonoBehaviour
	{
		[SerializeField] private GameObject template;

		private Stack<GameObject> pool = new Stack<GameObject>();

		private void Awake()
		{
			template.gameObject.SetActive(false);
		}

		public GameObject GetInstance()
		{
			GameObject instance = null;
			if (pool.Count > 0)
			{
				instance = pool.Pop();
			} else
			{
				instance = Instantiate(template, template.transform.parent);
			}
			instance.SetActive(true);
			return instance;
		}

		public void ReturnInstance(GameObject instance)
		{
			instance.SetActive(false);
			pool.Push(instance);
		}

		internal T GetInstance<T>() where T : Component
		{
			var instance = GetInstance();
			return (T)instance.GetComponent(typeof(T));
		}
	}
}