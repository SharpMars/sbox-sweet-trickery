using Sandbox.UI;
using Sandbox.UI.Construct;

namespace SweetTrickery.UI.Playing;

public sealed class Score : Panel
{
	public static Score Instance { get; private set; }
	
	private Label _label;

	public Score()
	{
		StyleSheet.Load( "/ui/Playing/Score.scss" );

		Add.Image( "ui/candy.png", "image" );
		_label = Add.Label( "0", "score");
		Instance = this;
	}

	public override void Tick()
	{
		_label.Text = $"{SweetTrickery.Instance.Score}";
		
		base.Tick();
	}
}
