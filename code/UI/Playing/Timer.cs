using Sandbox.UI;
using Sandbox.UI.Construct;

namespace SweetTrickery.UI.Playing;

public sealed class Timer : Panel
{
	private Label _label;
 	
	public Timer()
	{
		_label = Add.Label( "10:00" );
		StyleSheet.Load( "/UI/Playing/Timer.scss" );
	}

	public override void Tick()
	{
		_label.Text = $"{SweetTrickery.Timer.Minutes:00}:{SweetTrickery.Timer.Seconds:00}";
		
		base.Tick();
	}
}
