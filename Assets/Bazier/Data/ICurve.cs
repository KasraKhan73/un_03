namespace Lunha
{
    public interface ICurve
    {
        Curve4 GetCurve();
        Curve4 GetCurve( bool globalPosition = true);
    }
}