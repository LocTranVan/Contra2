using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets._Scripts
{
	public  class ManagerGun
	{
		public List<GameObject> Bullets;

		private const string NormalGun = "NormalGun", MachineGun = "MachineGun", FireballGun = "FireballGun", LaserGun = "LaserGun"
			, SpreadGun = "SpreadGun", Special = "Special", RapidFire = "RapidFire", Barrier = "Barrier", MachineGunPink = "MachineGunPink"
			, MachineGunRed = "MachineGunRed";

		private static ManagerGun manage;
		public static ManagerGun getIntance()
		{
			if(manage != null)
			{
				return manage;
			}
			manage = new ManagerGun();
			return manage;
		}
		public GameObject getBullet(String Gun)
		{
			switch (Gun)
			{
				case NormalGun:
					return Bullets[0];
				case MachineGunRed:
					return Bullets[1];
				case FireballGun:
					return Bullets[2];
				case LaserGun:
					return Bullets[3];
				default:
					return null;
			}
		}
		public float getRangeShoot(string Gun)
		{
			switch (Gun)
			{
				
				case NormalGun:
					return 5;
				case FireballGun:
					return 5.7f;
				case MachineGunPink:
					return 5.7f;
				case MachineGunRed:
					return 10f;
				default:
					return 30;
				
			}
		}
		public int getNumberBulletOnceShoot(String Gun)
		{
			switch (Gun)
			{
				case NormalGun:
					return 1;
				case MachineGun:
					return 2;
				default:
					return 2;
			}
		}
		public float getSpeedBullet(String Gun)
		{
			//Debug.Log(Gun);
			switch (Gun)
			{
				case NormalGun:
					return 10;
				case MachineGunPink:
					return 6;
				case FireballGun:
					return 4;
				case MachineGunRed:
					return 2;
				default:
					return 30;
			}
		}
		public int getDame(string Gun)
		{
			switch (Gun)
			{
				case NormalGun:
					return 1;
				case MachineGun:
					return 2;
				default:
					return 3;
			}
		}
	}
}
