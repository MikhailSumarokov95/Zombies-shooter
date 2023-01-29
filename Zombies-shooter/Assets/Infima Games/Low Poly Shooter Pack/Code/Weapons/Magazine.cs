//Copyright 2022, Infima Games. All Rights Reserved.

using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    /// <summary>
    /// Magazine.
    /// </summary>
    public class Magazine : MagazineBehaviour
    {

        #region FIELDS SERIALIZED

        [Title(label: "Settings")]
        
        [Tooltip("Total Ammunition.")]
        [SerializeField]
        private int ammunitionTotal = 10;

        [Title(label: "Interface")]

        [Tooltip("Interface Sprite.")]
        [SerializeField]
        private Sprite sprite;

        #endregion

        public override int AmmunitionSum { get; set; }

        #region GETTERS

        /// <summary>
        /// Ammunition Total.
        /// </summary>
        public override int GetAmmunitionTotal() => ammunitionTotal;
        /// <summary>
        /// Sprite.
        /// </summary>
        public override Sprite GetSprite() => sprite;

        #endregion

        public override int Reload()
        {
            var currentAmmunition = AmmunitionSum - ammunitionTotal;
            if (currentAmmunition <= 0)
            {
                currentAmmunition = AmmunitionSum;
                AmmunitionSum = 0;
                return currentAmmunition;
            }

            else
            {
                AmmunitionSum -= ammunitionTotal;
                return ammunitionTotal;
            }
        }
    }
}