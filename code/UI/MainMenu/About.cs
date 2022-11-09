using Sandbox.UI;
using Sandbox.UI.Construct;

namespace SweetTrickery.UI;

public sealed class About : Panel
{
	public static About Instance;
	
	public About()
	{
		Instance = this;
		StyleSheet.Load( "/UI/MainMenu/About.scss" );
		Add.Label( "Your goal is to collect candy from houses with green smoke.", "text" );
		Add.Label( "Faster you get to them, more candy you get.", "text" );
		Add.Label( "Navigate through map to collect as many candy as you can.", "text" );
		Add.Label( "From time to time a special Time Candy appears at your starting location.", "text" );
		Add.Label( "Eat it to get additional time.", "text" );
		Add.Button( "Back", SwitchToMainMenu ).AddClass( "back" );
	}
	
	public void SwitchToMainMenu()
	{
		MainMenu.Instance.Style.Display = DisplayMode.Flex;
		Style.Display = DisplayMode.None;
	}
}
