using System;
using System.Collections;
using UnityEngine;

//Pattern from https://github.com/wyyayy/Unity-coroutine-wrapper

public class Routine
{
	/// Termination event.  Triggered when the coroutine completes execution.
	public event Action OnComplete = delegate { };

	/// Exception handler
	public event Action<Exception> ExceptionHandler = delegate { };

	/// Internal coroutine wrapper
	private RoutineWrapper coroutineWrapper;

	/// Returns true if and only if the coroutine is running.  Paused tasks
	/// are considered to be running.
	public bool IsRunning {
		get { return coroutineWrapper.Running; }
	}
	/// Returns true if and only if the coroutine is currently paused.
	public bool IsPaused {
		get { return coroutineWrapper.Paused; }
	}

	public Coroutine Coroutine {
		get { return coroutineWrapper.InternalCoroutine; }
	}


	/// Creates a new Task object for the given coroutine.
	///
	/// If autoStart is true (default) the task is automatically started
	/// upon construction.
	public Routine()
	{
		RoutineWrapper.Init();
		coroutineWrapper = new RoutineWrapper(this);
		coroutineWrapper.OnComplete += Completed;
	}

	/// Begins execution of the coroutine
	public void Start(IEnumerator enumerator)
	{
		coroutineWrapper.enumerator = enumerator;
		coroutineWrapper.Start();
	}

	/// Discontinues execution of the coroutine at its next yield.
	public void Stop()
	{
		coroutineWrapper.Stop();
	}

	public void Pause()
	{
		coroutineWrapper.Pause();
	}

	public void Unpause()
	{
		coroutineWrapper.Unpause();
	}

	private void Completed()
	{
		OnComplete();
	}

	internal void OnException(Exception e)
	{
		if (ExceptionHandler != null) ExceptionHandler(e);
		else Debug.LogError(e);
	}

}
