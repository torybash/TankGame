using System;
using System.Collections;
using UnityEngine;

public class RoutineWrapper
{
	private static RoutineWorker routineWorker;

	public static void Init()
	{
		if (routineWorker == null)
		{
			var go = new GameObject("_CoroutineHelper");
			routineWorker = go.AddComponent<RoutineWorker>();
			GameObject.DontDestroyOnLoad(go);
		}
	}

	public bool Running { get; private set; } 
	public bool Paused { get; private set; }
	public Coroutine InternalCoroutine { get; private set; }
	public IEnumerator enumerator { get; set; }

	public event Action OnComplete = delegate { };

	private bool stopped;

	private Routine routine;

	public RoutineWrapper(Routine routine)
	{
		this.routine = routine;
	}

	public void Pause()
	{
		Paused = true;
	}

	public void Unpause()
	{
		Paused = false;
	}

	public void Start()
	{
		Running = true;
		InternalCoroutine = routineWorker.StartCoroutine(CallWrapper());
	}

	public void Stop()
	{
		stopped = true;
		Running = false;

		if (InternalCoroutine != null)
		{
			routineWorker.StopCoroutine(InternalCoroutine);
			InternalCoroutine = null;
		}
	}

	private IEnumerator CallWrapper()
	{
		yield return null;
		var current = enumerator;
		while (Running)
		{
			if (Paused) yield return null;
			else
			{
				if (enumerator != null)
				{
					bool yieldNextSucess = false;

					try
					{
						yieldNextSucess = current.MoveNext();
					} catch (Exception err)
					{
						routine.OnException(err);
					}

					if (yieldNextSucess)
					{
						yield return current.Current;
						continue;
					} else Running = false;
				} else
				{
					Running = false;
				}
			}
		}

		OnComplete();

		InternalCoroutine = null;
	}
}