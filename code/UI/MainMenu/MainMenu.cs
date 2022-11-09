using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace SweetTrickery.UI;

public sealed class MainMenu : Panel
{
	public static MainMenu Instance;

	public MainMenu()
	{
		Instance = this;
		StyleSheet.Load( "UI/MainMenu/MainMenu.scss" );

		Add.Image( "ui/logo.png" );
		Add.Button( "Play", SweetTrickery.PlayGame );
		Add.Button( "How to play", SwitchToAbout );
		Add.Button( "Exit", () => Local.Client.Kick() );
	}
	
	public void SwitchToAbout()
	{
		About.Instance.Style.Display = DisplayMode.Flex;
		Style.Display = DisplayMode.None;
	}
}
