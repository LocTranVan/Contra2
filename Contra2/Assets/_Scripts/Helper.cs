using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets._Scripts
{

	class Helper
	{
		private static Helper helper;
		private  Vector2 Right = new Vector2(1, 0), Left = new Vector2(-1, 0), Top = new Vector2(0, 1), Bot= new Vector2(0, -1)
			, SlantTop = new Vector2(1, 1), SlantBot = new Vector2(1, -1), UpSideSlantTop = new Vector2(-1, 1), UpSideSlantBot = new Vector2(-1, -1);
		public static Helper getIntance()
		{
			if (helper != null)
				return helper;
			helper = new Helper();
			return helper;

		}
		public Vector2 ChangeDirection(Vector2 currentDirection)
		{
			if (currentDirection == SlantBot || currentDirection == UpSideSlantBot)
				return Bot;
			if (currentDirection == SlantTop|| currentDirection == UpSideSlantTop)
				return Top;
			return currentDirection;
		}
		public Vector2 FindDirectionWhenHit(Vector2 currentDirection)
		{
			if (currentDirection == Right)
				return Left;
			if (currentDirection == Left)
				return Right;
			if (currentDirection == SlantBot)
				return Right;
			if (currentDirection == UpSideSlantBot)
				return Left;

			return Bot;
		}

		// disTarget = position - target;
		public Vector2 GetDirection(Vector2 disTarget, float maxDistance)
		{
			Vector2 direction;
			if (Mathf.Abs(disTarget.y) <= maxDistance)
			{
				 direction = (disTarget.x > 0) ? new Vector2(-1, 0) : new Vector2(1, 0);
				return direction;
			}
			if (Mathf.Abs(disTarget.x) <= maxDistance)
			{
				 direction = (disTarget.y > 0) ? new Vector2(0, -1) : new Vector2(0, 1);
				return direction;
			}
			if(disTarget.x > 0)
			{
				 direction = (disTarget.y > 0) ? new Vector2(-1, -1) : new Vector2(-1, 1);
				return direction;
			}

				 direction = (disTarget.y > 0) ? new Vector2(1, -1) : new Vector2(1, 1);
				return direction;
			
		}

	}
}
