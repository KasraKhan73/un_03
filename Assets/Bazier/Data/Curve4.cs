using UnityEngine;

namespace Lunha
{
    public class Curve4 : ICurve
    {
        public Vector3 p0, p1, p2, p3;

        public Curve4(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            this.p0 = p0;
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }

        public Curve4 GetCurve()
        {
            return this;
        }

        public Curve4 GetCurve ( bool globalPosition = true )
        {
            return this;
        }
    }
}