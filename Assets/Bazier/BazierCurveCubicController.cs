using System;
using System.Collections;
using System.ComponentModel.Design;
using UnityEngine;
using DG.Tweening;

namespace Lunha
{
	public class BazierCurveCubicController : BazierCurveCubic
	{
		[SerializeField] [Tooltip ( "= not set ? transform" )]
		protected Transform movableTransform;

		[SerializeField] protected float speed;

		[SerializeField, Range ( 0.001f, 0.1f )]
		protected float _rotationSensitivity = 0.06f;

		[SerializeField] private MoveAxis _moveAxis;
		[SerializeField] private bool _inverse;
		[SerializeField] private bool _lookAtDirection;
		[SerializeField] private bool _test;

		private Tween _moveTween;
		
		private bool _isMoving;

		public bool Inverse
		{
			get => _inverse;
			set => _inverse = value;
		}

		private void Awake ( )
		{
			if ( !movableTransform )
				movableTransform = transform;
		}

		private void Start ( )
		{
			if ( _test )
				Test ( );
		}

		private void OnDisable ( )
		{
			TweenHelper.TweenKill ( _moveTween );
		}

		private void Test ( )
		{
			MoveByCurve ( ( ) =>
			{
				_inverse = !_inverse;
				MoveByCurve ( Test );
			} );
		}

		public void MoveByCurve ( Action callback = null )
		{
			CheckData ( true );
			StartCoroutine ( Move ( callback, _inverse, _lookAtDirection, _moveAxis, _globalSpacePosition ) );
		}

		public void MoveByCurveTween ( TweenCallback callback = null, float moveTime = 1, bool isLocal = false, int pointsCount = 50 )
		{
			CheckData ( true );

			_isMoving = true;

			Vector3 [ ] targetPoints = new Vector3 [ 100 ];

			for ( int i = 0; i < 100; i++ )
			{
				if (isLocal)
					targetPoints[i] = BazierCurves.GetPositionByAxis(
								movableTransform.localPosition,
								BazierCurves.GetCubicPosition(ref curves, ((float)i) / 100, !isLocal),
								MoveAxis.XYZ
							);
				else
					targetPoints[i] = BazierCurves.GetCubicPosition(ref curves, ((float)i) / 100, !isLocal);
			}

			if (isLocal == false)
				_moveTween = movableTransform.DOPath(targetPoints, moveTime).SetEase(Ease.Linear)
					.SetSpeedBased(false).OnComplete(callback).SetLookAt(0.01f, Vector3.back);
			else
			{
				movableTransform.localPosition = targetPoints[0];
				_moveTween = movableTransform.DOLocalPath(targetPoints, moveTime).SetEase(Ease.Linear)
					.OnComplete(callback);
			}
		}

		private IEnumerator Move ( Action callback, bool inverse = false, bool lookAtDirection = false,
			MoveAxis moveAxis = MoveAxis.XYZ, bool globalSpace = true )
		{
			_isMoving = true;

			if ( inverse )
			{
				for ( var t = 1f; t >= 0f; t -= Time.fixedDeltaTime * speed )
				{
					if ( !_isMoving )
					{
						var et = 0f;

						if ( lookAtDirection )
						{
							var direction = BazierCurves.GetCubicPosition ( ref curves, et + _rotationSensitivity ) -
											BazierCurves.GetCubicPosition ( ref curves, et );
							movableTransform.rotation =
								( Quaternion.LookRotation ( direction.normalized, Vector3.up ) );
						}

						if ( moveAxis != MoveAxis.XYZ )
							movableTransform.position = BazierCurves.GetPositionByAxis ( movableTransform.position,
								BazierCurves.GetCubicPosition ( ref curves, et ), moveAxis );
						else
							movableTransform.position = BazierCurves.GetCubicPosition ( ref curves, et );

						break;
					}

					if ( lookAtDirection )
					{
						var tF = t - _rotationSensitivity;
						if ( tF > 0f )
						{
							var direction = movableTransform.position -
											BazierCurves.GetCubicPosition ( ref curves, tF );
							movableTransform.rotation =
								( Quaternion.LookRotation ( direction.normalized, Vector3.up ) );
						}
					}

					if ( moveAxis != MoveAxis.XYZ )
						movableTransform.position = BazierCurves.GetPositionByAxis ( movableTransform.position,
							BazierCurves.GetCubicPosition ( ref curves, t ), moveAxis );
					else
						movableTransform.position = BazierCurves.GetCubicPosition ( ref curves, t );

					yield return new WaitForFixedUpdate ( );
				}
			}
			else
			{
				for ( var t = 0f; t <= 1f; t += Time.deltaTime * speed )
				{
					if ( !_isMoving )
					{
						var et = 1f;

						if ( lookAtDirection )
						{
							var direction = BazierCurves.GetCubicPosition ( ref curves, et - _rotationSensitivity ) -
											BazierCurves.GetCubicPosition ( ref curves, et );
							movableTransform.rotation =
								( Quaternion.LookRotation ( direction.normalized, Vector3.up ) );
						}

						if ( moveAxis != MoveAxis.XYZ )
							movableTransform.position = BazierCurves.GetPositionByAxis ( movableTransform.position,
								BazierCurves.GetCubicPosition ( ref curves, et ), moveAxis );
						else
							movableTransform.position = BazierCurves.GetCubicPosition ( ref curves, et );

						break;
					}

					if ( lookAtDirection )
					{
						var tF = t + _rotationSensitivity;
						if ( tF < 1f )
						{
							var direction = movableTransform.position -
											BazierCurves.GetCubicPosition ( ref curves, tF );
							movableTransform.rotation =
								( Quaternion.LookRotation ( direction.normalized, Vector3.up ) );
						}
					}

					if ( !globalSpace )
					{
						movableTransform.localPosition = BazierCurves.GetPositionByAxis (
							movableTransform.localPosition,
							BazierCurves.GetCubicPosition ( ref curves, t, false ),
							moveAxis
						);
					}
					else
					{
						movableTransform.position = BazierCurves.GetPositionByAxis (
							movableTransform.position,
							BazierCurves.GetCubicPosition ( ref curves, t, true ),
							moveAxis
						);
					}

					yield return new WaitForEndOfFrame ( );
				}
			}

			_isMoving = false;
			callback?.Invoke ( );

			callback = null;
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

		public override void Copy ( BazierCurveCubic copy )
		{
			base.Copy ( copy );

			var copy2 = copy as BazierCurveCubicController;

			copy2._inverse = _inverse;
			copy2._moveAxis = _moveAxis;
			copy2.speed = speed;
			copy2._lookAtDirection = _lookAtDirection;
		}

		public void Stop ( )
		{
			_isMoving = false;

			TweenHelper.TweenKill ( _moveTween );
		}
	}
}