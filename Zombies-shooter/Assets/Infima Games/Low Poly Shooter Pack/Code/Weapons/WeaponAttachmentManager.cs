﻿//Copyright 2022, Infima Games. All Rights Reserved.

using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    /// <summary>
    /// Weapon Attachment Manager. Handles equipping and storing a Weapon's Attachments.
    /// </summary>
    public class WeaponAttachmentManager : WeaponAttachmentManagerBehaviour
    {
        #region FIELDS SERIALIZED

        [Title(label: "Scope")]

        [Tooltip("Determines if the ironsights should be shown on the weapon model.")]
        [SerializeField]
        private bool scopeDefaultShow = true;

        [Tooltip("Default Scope!")]
        [SerializeField]
        private ScopeBehaviour scopeDefaultBehaviour;

        [Tooltip("Selected Scope Index. If you set this to a negative number, ironsights will be selected as the enabled scope.")]
        [SerializeField]
        private int scopeIndex = -1;

        [Tooltip("First scope index when using random scopes.")]
        [SerializeField]
        private int scopeIndexFirst = -1;

        [Tooltip("Should we pick a random index when starting the game?")]
        [SerializeField]
        private bool scopeIndexRandom;

        [Tooltip("All possible Scope Attachments that this Weapon can use!")]
        [SerializeField]
        private ScopeBehaviour[] scopeArray;

        [Title(label: "Muzzle")]

        [Tooltip("Selected Muzzle Index.")]
        [SerializeField]
        private int muzzleIndex;

        [Tooltip("Should we pick a random index when starting the game?")]
        [SerializeField]
        private bool muzzleIndexRandom = true;

        [Tooltip("All possible Muzzle Attachments that this Weapon can use!")]
        [SerializeField]
        private MuzzleBehaviour[] muzzleArray;

        [Title(label: "Laser")]

        [Tooltip("Selected Laser Index.")]
        [SerializeField]
        private int laserIndex = -1;

        [Tooltip("Should we pick a random index when starting the game?")]
        [SerializeField]
        private bool laserIndexRandom = true;

        [Tooltip("All possible Laser Attachments that this Weapon can use!")]
        [SerializeField]
        private LaserBehaviour[] laserArray;

        [Title(label: "Grip")]

        [Tooltip("Selected Grip Index.")]
        [SerializeField]
        private int gripIndex = -1;

        [Tooltip("Should we pick a random index when starting the game?")]
        [SerializeField]
        private bool gripIndexRandom = true;

        [Tooltip("All possible Grip Attachments that this Weapon can use!")]
        [SerializeField]
        private GripBehaviour[] gripArray;

        [Title(label: "Magazine")]

        [Tooltip("Selected Magazine Index.")]
        [SerializeField]
        private int magazineIndex;

        [Tooltip("Should we pick a random index when starting the game?")]
        [SerializeField]
        private bool magazineIndexRandom = true;

        [Tooltip("All possible Magazine Attachments that this Weapon can use!")]
        [SerializeField]
        private Magazine[] magazineArray;

        #endregion

        #region FIELDS

        /// <summary>
        /// Equipped Scope.
        /// </summary>
        private ScopeBehaviour scopeBehaviour;
        /// <summary>
        /// Equipped Muzzle.
        /// </summary>
        private MuzzleBehaviour muzzleBehaviour;
        /// <summary>
        /// Equipped Laser.
        /// </summary>
        private LaserBehaviour laserBehaviour;
        /// <summary>
        /// Equipped Grip.
        /// </summary>
        private GripBehaviour gripBehaviour;
        /// <summary>
        /// Equipped Magazine.
        /// </summary>
        private MagazineBehaviour magazineBehaviour;

        #endregion

        #region Properties

        public int ScopeBehaviourCount { get { return scopeArray.Length; } }

        public int MuzzleBehaviourCount { get { return muzzleArray.Length; } }

        public int LaserBehaviourCount { get { return laserArray.Length; } }

        public int GripBehaviourCount { get { return gripArray.Length; } } 

        
        public int ScopeIndex { get { return scopeIndex; } }

        public int MuzzleIndex { get { return muzzleIndex; } }

        public int LaserIndex { get { return laserIndex; } }

        public int GripIndex { get { return gripIndex; } }

        #endregion

        #region UNITY FUNCTIONS

        /// <summary>
        /// Awake.
        /// </summary>
        protected override void Awake()
        {
            //Randomize. This allows us to spice things up a little!
            if (scopeIndexRandom)
                scopeIndex = Random.Range(scopeIndexFirst, scopeArray.Length);
            //Select Scope!
            scopeBehaviour = scopeArray.SelectAndSetActive(scopeIndex);
            //Check if we have no scope. This could happen if we have an incorrect index.
            if (scopeBehaviour == null)
            {
                //Select Default Scope.
                scopeBehaviour = scopeDefaultBehaviour;
                //Set Active.
                scopeBehaviour.gameObject.SetActive(scopeDefaultShow);
            }

            //Randomize. This allows us to spice things up a little!
            if (muzzleIndexRandom)
                muzzleIndex = Random.Range(0, muzzleArray.Length);
            //Select Muzzle!
            muzzleBehaviour = muzzleArray.SelectAndSetActive(muzzleIndex);

            //Randomize. This allows us to spice things up a little!
            if (laserIndexRandom)
                laserIndex = Random.Range(0, laserArray.Length);
            //Select Laser!
            laserBehaviour = laserArray.SelectAndSetActive(laserIndex);

            //Randomize. This allows us to spice things up a little!
            if (gripIndexRandom)
                gripIndex = Random.Range(0, gripArray.Length);
            //Select Grip!
            gripBehaviour = gripArray.SelectAndSetActive(gripIndex);

            //Randomize. This allows us to spice things up a little!
            if (magazineIndexRandom)
                magazineIndex = Random.Range(0, magazineArray.Length);
            //Select Magazine!
            magazineBehaviour = magazineArray.SelectAndSetActive(magazineIndex);
        }

        #endregion

        #region GETTERS

        public override ScopeBehaviour GetEquippedScope() => scopeBehaviour;
        public override ScopeBehaviour GetEquippedScopeDefault() => scopeDefaultBehaviour;

        public override MagazineBehaviour GetEquippedMagazine() => magazineBehaviour;
        public override MuzzleBehaviour GetEquippedMuzzle() => muzzleBehaviour;

        public override LaserBehaviour GetEquippedLaser() => laserBehaviour;
        public override GripBehaviour GetEquippedGrip() => gripBehaviour;

        #endregion

        #region SETTERS

        public void SetAttachment(Progress.WeaponAttachmentSelected attachment)
        {
            SetEquippedScope(attachment.ScopeIndex);
            SetEquippedMuzzle(attachment.MuzzleIndex);
            SetEquippedLaser(attachment.LaserIndex);
            SetEquippedGrip(attachment.GripIndex);
        }

        public void SetEquippedScope(int value)
        {
            scopeIndex = value;
            scopeArray.SelectAndSetActive(scopeIndex);

            if (scopeBehaviour == null)
            {
                scopeBehaviour = scopeDefaultBehaviour;
                scopeBehaviour.gameObject.SetActive(scopeDefaultShow);
            }
        }

        public void SetEquippedMuzzle(int value)
        {
            muzzleIndex = value;
            muzzleArray.SelectAndSetActive(muzzleIndex);
        }

        public void SetEquippedLaser(int value)
        {
            laserIndex = value;
            laserArray.SelectAndSetActive(laserIndex);
        }

        public void SetEquippedGrip(int value)
        {
            gripIndex = value;
            gripArray.SelectAndSetActive(gripIndex);
        }

        #endregion
    }
}