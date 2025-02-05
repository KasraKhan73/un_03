using UnityEditor;

namespace SBabchuk.Databases
{
    [CustomEditor(typeof(PlaneSettings))]
    public class SettingsEditor : BaseDatabaseEditor
    {
        public override void Draw()
        {
            SettingsDrawer.Draw((PlaneSettings)database, selectedMode);
        }
    }
}