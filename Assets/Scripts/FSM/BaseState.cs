using UnityEngine;
public class BaseState<Ttarget>
{
	protected int anim;
	protected Ttarget target;
	public BaseState(string anim, Ttarget target)
	{
		this.anim = Animator.StringToHash(anim);
		this.target = target;
	}
	virtual public void Enter() { }
	virtual public void Exit() { }
	virtual public void Update() { }
	virtual public void FixedUpdate() { }
}
