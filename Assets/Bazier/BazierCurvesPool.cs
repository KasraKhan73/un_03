using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lunha
{
	public class BazierCurvesPool: MonoBehaviour
	{
		[SerializeField] private BazierCurveQuadraticController _bazierCurveQuadratic;
		[SerializeField] private BazierCurveCubicController _bazierCurveCubic;

		private readonly Queue<BazierCurveCubicController> _bazierCubicQueue = new Queue<BazierCurveCubicController>();

		private readonly Queue<BazierCurveQuadraticController> _bazierQuadraticQueue =
			new Queue<BazierCurveQuadraticController>();

		#region Close

		private void OnDisable()
		{
			foreach (var controller in _bazierQuadraticQueue) Destroy(controller);
			_bazierQuadraticQueue.Clear();

			foreach (var controller in _bazierCubicQueue) Destroy(controller);
			_bazierCubicQueue.Clear();
		}

		#endregion

		#region Open

		public BazierCurveQuadraticController GetBazierCurveQuadraticController()
		{
			if (!_bazierQuadraticQueue.Any())
				_bazierQuadraticQueue.Enqueue(gameObject.AddComponent<BazierCurveQuadraticController>());

			var result = _bazierQuadraticQueue.Dequeue();
			return result;
		}

		public void ReturnToPool(BazierCurveQuadraticController curveQuadraticController)
		{
			_bazierQuadraticQueue.Enqueue(curveQuadraticController);
		}

		public BazierCurveCubicController GetBazierCurveCubicController()
		{
			if (!_bazierCubicQueue.Any())
				_bazierCubicQueue.Enqueue(gameObject.AddComponent<BazierCurveCubicController>());

			var result = _bazierCubicQueue.Dequeue();

			if (_bazierCurveCubic)
			{
				_bazierCurveCubic.Copy(result);
			}

			return result;
		}

		public void ReturnToPool(BazierCurveCubicController curveQuadraticController)
		{
			_bazierCubicQueue.Enqueue(curveQuadraticController);
		}

		#endregion
	}
}