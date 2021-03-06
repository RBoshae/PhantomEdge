﻿namespace VRTK.Examples
{
    using UnityEngine;

    public class Gun : VRTK_InteractableObject
    {
        private GameObject bullet;
        [SerializeField]
        private float bulletSpeed = 10000f;
        private float bulletLife = 5f;
        
        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);
            FireBullet();
        }

        protected void Start()
        {
            GlobalRefs.Gun = this.gameObject;
            bullet = transform.Find("Bullet").gameObject;
            bullet.SetActive(false);
        }

        private void FireBullet()
        {
            GameObject bulletClone = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation) as GameObject;
            bulletClone.SetActive(true);
            Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
            rb.AddForce(-bullet.transform.forward * bulletSpeed);
            Destroy(bulletClone, bulletLife);
            AudioSource sound = GetComponent<AudioSource>();
            sound.Play();
        }
    }
}