using Sandbox.UI;
using Sandbox.UI.Construct;

namespace SweetTrickery.UI;

public sealed class ScoreShow : Panel
{
	public static ScoreShow Instance;

	public ScoreShow()
	{
		Instance = this;
		StyleSheet.Load( "/UI/MainMenu/ScoreShow.scss" );
		
		var panel = Add.Panel( "score_panel" );
		panel.Add.Image( "ui/candy.png", "candy" );
		panel.Add.Label( "Score: ", "score_title" );

		Add.Label( SweetTrickery.Instance.Score.ToString(), "score" );
		Add.Button( "Back", SwitchToMainMenu ).AddClass( "back" );
	}
	
	public void SwitchToMainMenu()
	{
		MainMenu.Instance.Style.Display = DisplayMode.Flex;
		Style.Display = DisplayMode.None;
	}
}
