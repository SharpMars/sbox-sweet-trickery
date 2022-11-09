using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandbox.UI;
using SweetTrickery.UI;
using SweetTrickery.UI.Playing;

namespace SweetTrickery;

public sealed partial class SweetTrickery : Game
{
	public HudEntity<RootPanel> Hud;
	public static SweetTrickery Instance { get; private set; }
	[Net, HideInEditor] public float Countdown { get; set; }
	public static TimeSpan Timer => TimeSpan.FromSeconds( Instance.Countdown );
	[Net, HideInEditor] private List<HouseDoor> HouseDoors { get; set; }
	[Net, HideInEditor] private TimeCandySpawner CandySpawner { get; set; }
	[Net, HideInEditor] public TimeSince TimeSinceTimeCandyPickedUp { get; set; }
	[Net, HideInEditor] public bool TimeCandyExist { get; set; }
	[Net, HideInEditor] private int TimeToSpawnCandy { get; set; }
	[Net, HideInEditor] public HouseDoor CurrentDoor { get; set; }
	[Net] public static TimeSince LastDoorOpen { get; set; }
	[Net] private bool IsGameStarted { get; set; }
	[Net] public ulong Score { get; set; }
	[Net, Predicted] public bool JumpscareAlreadyHappened { get; set; }

	public SweetTrickery()
	{
		Instance = this;
		if ( IsClient )
		{
			Hud = new MainMenuHud();
		}
	}

	[Event.Entity.PostSpawn]
	private void PostSpawn()
	{
		HouseDoors = All.OfType<HouseDoor>().ToList();
		TimeToSpawnCandy = Random.Shared.Next( 20, 60 );
		CandySpawner = All.OfType<TimeCandySpawner>().First();
	}

	[Event.Hotload]
	private void OnHotload()
	{
		if ( !IsClient )
			return;

		switch ( Hud )
		{
			case MainMenuHud:
				Hud.Delete();
				Hud = new MainMenuHud();
				break;
			case PlayingHud:
				Hud.Delete();
				Hud = new PlayingHud();
				break;
		}
	}

	[Event.Tick]
	private void Tick()
	{
		if ( IsGameStarted && IsServer )
		{
			if ( Countdown <= 0 )
			{
				EndGame();
			}
			else
			{
				Countdown -= Time.Delta;
				
				if ( TimeSinceTimeCandyPickedUp > TimeToSpawnCandy && !TimeCandyExist )
				{
					CandySpawner.SpawnCandy();
					TimeCandyExist = true;
				}
			}
		}
	}

	public override void ClientJoined( Client client )
	{
		base.ClientJoined( client );
	}

	[ConCmd.Server]
	public static void PlayGame()
	{
		Instance.TimeSinceTimeCandyPickedUp = 0;
		PlayGameClient();
		var pawn = new Player( ConsoleSystem.Caller );
		ConsoleSystem.Caller.Pawn = pawn;

		pawn.Respawn();
		Instance.Countdown = (float)TimeSpan.FromMinutes( 2 ).TotalSeconds;
		ChooseNewDoor();
		LastDoorOpen = 0;

		Instance.IsGameStarted = true;
	}

	[ClientRpc]
	public static void PlayGameClient()
	{
		Instance.Hud.Delete();
		Instance.Hud = new PlayingHud();
	}

	public static void EndGame()
	{
		var client = Player.Instance.Client;
		EndGameClient();
		Player.Instance.Delete();
		client.Pawn = null;
		Instance.IsGameStarted = false;
		Instance.CandySpawner.DeleteCandy();
	}

	[ClientRpc]
	public static void EndGameClient()
	{
		Instance.Hud.Delete();
		Instance.Hud = new MainMenuHud();
		ScoreShow.Instance.Style.Display = DisplayMode.Flex;
		MainMenu.Instance.Style.Display = DisplayMode.None;
	}

	public static void ChooseNewDoor()
	{
		var newDoor = Instance.CurrentDoor;

		while ( Instance.CurrentDoor == newDoor )
		{
			var rand = Random.Shared.Next( 0, Instance.HouseDoors.Count );
			newDoor = Instance.HouseDoors[rand];
		}

		Instance.CurrentDoor = newDoor;
	}

	public static void TimeCandyPickedUp()
	{
		Instance.Countdown += 30;
		Instance.TimeSinceTimeCandyPickedUp = 0;
		Instance.TimeToSpawnCandy = Random.Shared.Next( 20, 60 );
		Instance.TimeCandyExist = false;
	}
}
