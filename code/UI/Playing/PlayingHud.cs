using Sandbox;
using Sandbox.UI;

namespace SweetTrickery.UI.Playing;

public sealed partial class PlayingHud : HudEntity<RootPanel>
{
	public static PlayingHud Instance;
	Jumpscare _jumpscare;
	
	public PlayingHud()
	{
		Instance = this;
		RootPanel.SetTemplate( "/UI/Playing/PlayingHud.html" );
		if ( !SweetTrickery.Instance.JumpscareAlreadyHappened )
		{
			_jumpscare = new Jumpscare();
			RootPanel.AddChild( _jumpscare );
		}
	}
	
	public void ShowJumpscare()
	{
		if(_jumpscare.IsValid())
			_jumpscare.Style.Display = DisplayMode.Flex;
	}
}
