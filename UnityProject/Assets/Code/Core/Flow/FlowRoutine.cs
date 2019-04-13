using System.Collections;


public class FlowRoutine
{
	public IFlow flow;
	public Routine routine;

	public FlowRoutine(IFlow flow)
	{
		this.flow = flow;
		routine = new Routine();
		routine.Start(flow.Flow());
	}
}


