
public class PlayerFSM
{
    public IState curstate { get; private set; }
    
    public void changeState(IState nextState)
	{
		if (nextState == null) return;
		if (curstate == nextState) return;

		curstate?.Exit();
		curstate = nextState;
		curstate.Enter();
	}

}
