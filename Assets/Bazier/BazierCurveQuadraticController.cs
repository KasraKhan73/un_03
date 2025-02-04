using System;
using System.Collections;
using UnityEngine;

namespace Lunha
{
	public class BazierCurveQuadraticController : BazierCurveQuadratic
	{
		[SerializeField]
		[Tooltip ( "= not set ? transform" )]
		protected Transform movableTransform;

		[SerializeField] protected float speed = 1f;

		private void Awake ( )
		{
			if ( !movableTransform )
				movableTransform = transform;
		}

		public void MoveByCurve ( Action callback = null, bool inverse = false )
		{
			CheckData ( true );
			StartCoroutine ( Move ( callback, inverse ) );
		}

		private IEnumerator Move ( Action callback, bool inverse = false, bool lookAtDirection = false )
		{
			if ( inverse )
			{
				for ( var curveIndex = iCurves.Length - 1; curveIndex >= 0; curveIndex-- )
				{
					var curve = iCurves [ curveIndex ];
					for ( var t = 1f; t >= 0f; t -= Time.deltaTime * speed )
					{
						if(lookAtDirection)
						{
							var tF = t - 0.1f;
							if (tF > 0f)
							{
								var direction = movableTransform.position -
												BazierCurves.GetQuadraticPosition(curve.GetCurve(), t);
								movableTransform.rotation = (Quaternion.LookRotation(direction.normalized, Vector3.up));
							}
						}
						
						movableTransform.position =
							BazierCurves.GetQuadraticPosition ( curve.GetCurve ( ), t );
						yield return new WaitForEndOfFrame ( );
					}
				}
			}
			else
			{
				foreach ( var curve in iCurves )
					for ( var t = 0f; t <= 1f; t += Time.deltaTime * speed )
					{
						if(lookAtDirection)
						{
							var tF = t + 0.1f;
							if (tF < 1f)
							{
								var direction = movableTransform.position -
												BazierCurves.GetQuadraticPosition(curve.GetCurve(), t);
								movableTransform.rotation = (Quaternion.LookRotation(direction.normalized, Vector3.up));
							}
						}
						movableTransform.position =
							BazierCurves.GetQuadraticPosition ( curve.GetCurve ( ), t );
						yield return new WaitForEndOfFrame ( );
					}
			}

			callback?.Invoke ( );
		}

		public void SetCurves ( ICurve [ ] newCurves )
		{
			iCurves = newCurves;
		}

		public void SetMovableTransform ( Transform newMovable )
		{
			movableTransform = newMovable;
		}

		public void SetSpeed ( float s )
		{
			speed = s;
		}

		public float GetSpeed ( )
		{
			return speed;
		}
	}
}