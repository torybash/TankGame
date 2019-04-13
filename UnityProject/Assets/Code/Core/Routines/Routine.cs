using System;
using System.Collections;
using UnityEngine;

//Pattern from https://github.com/wyyayy/Unity-coroutine-wrapper

public class Routine
{
	/// Delegate for termination subscribers.  manual is true if and only if
	/// the coroutine was stopped with an explicit call to Stop().
	public delegate void FinishedHandler(bool manual);

	/// Termination event.  Triggered when the coroutine completes execution.
	public event FinishedHandler Finished;

	/// Exception handler
	public event Action<Exception> ExceptionHandler;

	/// Internal coroutine wrapper
	private RoutineWrapper coroutineWrapper;

	/// Returns true if and only if the coroutine is running.  Paused tasks
	/// are considered to be running.
	public bool isRunning { get { return coroutineWrapper.Running; } }
	/// Returns true if and only if the coroutine is currently paused.
	public bool isPaused { get { return coroutineWrapper.Paused; } }

	public Coroutine coroutine { get { return coroutineWrapper.InternalCoroutine; } }


	/// Creates a new Task object for the given coroutine.
	///
	/// If autoStart is true (default) the task is automatically started
	/// upon construction.
	public Routine()
	{
		RoutineWrapper.Init();
	}

	/// Begins execution of the coroutine
	public void Start(IEnumerator enumerator)
	{
		coroutineWrapper = new RoutineWrapper(enumerator, this);
		coroutineWrapper.Finished += TaskFinished;
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

	internal void OnException(Exception e)
	{
		if (ExceptionHandler != null) ExceptionHandler(e);
		else Debug.LogError(e);
	}

	void TaskFinished(bool manual)
	{
		FinishedHandler handler = Finished;
		if (handler != null)
			handler(manual);
	}
}
