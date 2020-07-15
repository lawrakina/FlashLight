using System.Linq;
using FpsUnity.Enums;
using FpsUnity.Interface;
using FpsUnity.Model;
using FpsUnity.Services;
using Helper;
using UnityEngine;


namespace FpsUnity.Controller
{
    public sealed class Inventory : IInitualization
    {
        #region Fields

        private Weapon[] _weapons = new Weapon[5];
        private int _cashLastIndex = 0;

        #endregion


        #region Properties

        private Weapon[] Weapons => _weapons;
        public FlashLightModel FlashLight { get; private set; }

        #endregion


        #region Methods

        public void Initialization()
        {
            _weapons = ServiceLocatorMonoBehavior.GetService<CharacterController>().
                GetComponentsInChildren<Weapon>();

            foreach (var weapon in Weapons)
            {
                //Debug.Log($"weapon:{weapon}, weapon.name:{weapon.name}");
                weapon.IsVisible = false;
            }

            FlashLight = Object.FindObjectOfType<FlashLightModel>();
            FlashLight.Switch(FlashLightActiveType.Off);
        }

        public void RemoveWeapon(Weapon weapon)
        {
            for (int i = 0; i < _weapons.Length; i++)
            {
                if (_weapons[i].Compare(weapon))
                    _weapons[i] = null;
            }

            _weapons = _weapons.Where(x => x != null).ToArray();
        }

        public Weapon GetWeaponByIndex(int index)
        {
            if (index < 0)
                index = 0;
            if (index >= _weapons.Length)
                index = _weapons.Length - 1;
            _cashLastIndex = index;
            return _weapons[index];
        }

        public Weapon GetWeaponByType(WeaponType type)
        {
            return _weapons.FirstOrDefault(weapon => weapon.WeaponType == type);
        }

        #endregion

        public int GetLastIndexWeapon()
        {
            return _cashLastIndex;
        }
    }
}