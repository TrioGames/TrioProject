﻿using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {

	public const string TAG_BALL = "Player";

	public const string TAG_BONUS = "Bonus";

	public const string TAG_PLANE = "Respawn";

	public const string TAG_TRIO_OBJECT = "TrioObject";

	public const string TAG_BOTTOM_PLANE = "RockBottomPlane";

	public const int GRAVITY_MODE = 2;

	public const int FIREBALL_MODE = 1;

	public const int NORMAL_MODE = 0;

	public const string FIRE_BALL = "fireball";
	
	public const string MAIN_BALL = "mainball";

	public const float FIREBALL_TIMER_INIT = 10;

	public const float SUPERBALL_TIMER_INIT = 5;

	public const string FIREBALL_BONUS = "fireballbonus";

	public const string SUPERBALL_BONUS = "superballbonus";

	public const string GRAVITY_BONUS = "gravitybonus";

	public const int SUPERBALL_SPEED_CONST = 50;

	public const int BALL_SPEED = 8;

	public const float POWERUP_LIFE_TIMER = 20;

	public const float HEIGHT_NO_PLATFORM = 4.0f;

	public const int GAME_STATUS_END = 2;

	public const int GAME_STATUS_RUN = 1;

	public const int GAME_STATUS_PAUSE = 0;

    public const int GAME_STATUS_SLOWMO = 3;

    public const float GRAVITY_DEFAULT = -5f;

	public const float GRAVITY_LOW = -2.5f;

	public const float GRAVITY_TIMER = 10;

	public const string TAG_PLATFORM = "Platform";

    public const float ROTATE_SPEED = 10;

    public const bool IsMobile = true;

    public const bool IsWeb = false;

    public const bool IsEditor = false;

    public const int POSSIBLITY_TO_GET_BONUS = 10;
}
