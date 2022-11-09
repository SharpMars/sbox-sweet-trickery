using Sandbox;
using SandboxEditor;
using SweetTrickery.UI.Playing;

namespace SweetTrickery;

[HammerEntity, EditorModel("models/editor/cordon_helper.vmdl")]
public partial class JumpScareActivator : Entity
{
	[Net] public float TimeSinceActivation { get; set; }
	[Net] public bool Activated { get; set; }
	[Net, Predicted] public bool ShouldDelete { get; set; }
	
	[Input]
	public void ActivateJumpscare()
	{
		Activated = true;
	}
	
	[Event.Tick]
	public void Tick()
	{
		if(Activated)
			TimeSinceActivation += Time.Delta;

		if ( TimeSinceActivation > 5f && IsClient )
		{
			PlayingHud.Instance.ShowJumpscare();
			ShouldDelete = true;
			SweetTrickery.Instance.JumpscareAlreadyHappened = true;
		}
		
		if(ShouldDelete && IsServer)
			Delete();
	}
}
