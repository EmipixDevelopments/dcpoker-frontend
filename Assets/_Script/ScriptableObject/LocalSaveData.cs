﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class LocalSaveData 
{
	public static LocalSaveData current = new LocalSaveData();

	public string Username;
	public string password;
	public string phoneCode;
	public string phoneNumber;
	public bool isRememberMe;
	//public GameObjects SettedGameObjects;
}

