using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace SBabchuk
{
    [CreateAssetMenu(menuName = "Settings/Create PlaneSettings", fileName = "PlaneSettings")]
    public class PlaneSettings : ScriptableObjectBase
    {
        public float airGlid = 10; //дальность полета
        
        public float handling = 10; //управляемость (радиус поворота)
        
        public int cost = 10; //стоимость
        
        public int researchPoint = 10; //очки изучения (необходимых для открытия доступа к покупке)
        
        public int detailsCount = 10; //количество деталей
        
        public PlaneColors color; //цвет 
    }
}
