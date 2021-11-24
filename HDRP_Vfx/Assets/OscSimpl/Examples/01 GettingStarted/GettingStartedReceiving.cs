/*
	Created by Carl Emil Carlsen.
	Copyright 2016-2018 Sixth Sensor.
	All rights reserved.
	http://sixthsensor.dk
*/

using UnityEngine;
using UnityEngine.VFX;

namespace OscSimpl.Examples
{
	public class GettingStartedReceiving : MonoBehaviour
	{
		[SerializeField] OscIn _oscIn;

        /// ----- CAM Controll
        public GameObject CameraPers;
        public GameObject CameraPersTop;
        //public GameObject CameraOrtho;
        public Animator AC;

        public string Map_CamPositionXY ;
        public Vector2 PosXY;        
		public string Map_HauteurZ;
        public float PosZ;
        public string Map_EncodRotateCam;
        public float CamRotationValue;

        public string Map_SlideFOV;
       // private float FOV_Ortho;
        public float FOV_Pers;

        public string Map_B1;
        public string Map_B2;
        public string Map_B3;

        public string Map_Radio;

        /// ----- FX PART
        public GameObject CenterRotation;
        public GameObject FX;
        public string Map_EncodRotateFX;
        public float FXRotationValue;
        public string Map_FxAngle;
        public float FXAngleValue;

        public string Map_Particles;
        public int Particles;

        public string Map_Change1;
        public string Map_Change2;
        public string Map_Change3;
        public string Map_Change4;
        public string Map_Change5;
       // public string Map_Change6;

        /// ----- debug
        public string Map_KBPosX;
    //    public float KBposX;
        public string Map_KBPosY;
    //    public float KBposY;
        public string Map_KBradius;
        //     public float radius;
        public string Map_PointcloudPosXY;
        public Vector2 PointCloudPosXY;

        public string Map_MaxParticles;
        public string MAP_Color;

        public string Map_FXrotation2;

      //  public string Map_B4;
      //  public string Map_B5;
     //   public string Map_B6;

        public string Map_ChangeDebug1;
        public string Map_ChangeDebug2;

        float map(float Val, float minInit, float MaxInit, float MinFinal, float MaxFinal)
        {
            return MinFinal + (Val - minInit) * (MaxFinal - MinFinal) / (MaxInit - minInit);
        }

        private bool BCam;
        float _incomingFloat;
        private int Nbr_portIn;

        void Start()
		{
            //PosXY = new Vector2(0,0);
            //PosZ = 0;
            Nbr_portIn = _oscIn.port;

			if( !_oscIn ) _oscIn = gameObject.AddComponent<OscIn>();
			_oscIn.Open( Nbr_portIn);

        }

        void Update()
        {
          //  CameraOrtho.transform.position = new Vector3(PosXY.x, PosZ, PosXY.y);
            CameraPers.transform.position = new Vector3(PosXY.x, PosZ, PosXY.y);
            CameraPersTop.transform.position = new Vector3(PosXY.x, PosZ, PosXY.y);
            // CameraPers.transform.localRotation = Quaternion.Euler(0, CamRotationValue, 0);
        }

        void OnEnable()
		{          
            _oscIn.Map(Map_CamPositionXY, EventPosXY);              // Cam position
            _oscIn.MapFloat(Map_HauteurZ, EventHauteurZ);           // Cam hauteur
            _oscIn.MapFloat(Map_SlideFOV, EventFOV);                // Cam FOV
            _oscIn.MapFloat(Map_EncodRotateCam, EventCamRotation);  // Cam Rotation
            _oscIn.MapFloat(Map_B1, EventAnim1);                    // ChangeAnim1
            _oscIn.MapFloat(Map_B1, EventAnim2);                    // ChangeAnim2
           // _oscIn.MapFloat(Map_B1, EventAnim3);                    // ChangeAnim3
            _oscIn.MapInt(Map_Radio, EventChangeCam);               // ChangeCam

            _oscIn.MapFloat(Map_EncodRotateFX, EventFXRotation);    // FX Rotation
            _oscIn.MapFloat(Map_FXrotation2, EventFXRotation);      // FX Rotation 2
            _oscIn.MapFloat(Map_Particles, EventFXParticles);       // FX In Particles
            _oscIn.MapFloat(Map_Change1, EventFX1);                 // FX Param01
            _oscIn.MapFloat(Map_Change2, EventFX2);                 // FX Param02
            _oscIn.MapFloat(Map_Change3, EventFX3);                 // FX Param03
            _oscIn.MapFloat(Map_Change4, EventFX4);                 // FX Param04
            _oscIn.MapFloat(Map_Change5, EventFX5);                 // FX Param05
           // _oscIn.MapFloat(Map_Change6, EventFX6);                 // FX Param06

            _oscIn.MapFloat(Map_KBPosX, EventKBposX);               // debug KB posX
            _oscIn.MapFloat(Map_KBPosY, EventKBposY);               // debug KB posY
            _oscIn.MapFloat(Map_KBradius, EventKBradius);           // debug KB radius

            _oscIn.Map(Map_PointcloudPosXY,EventPosXY);          // debug PC Position

            _oscIn.MapFloat(Map_ChangeDebug1, EventDebug1);         // Debug Param01
            _oscIn.MapFloat(Map_ChangeDebug2, EventDebug2);
        }

		void OnDisable()
		{
			//_oscIn.Unmap( OnTest2 );
		}

        void EventPosXY(OscMessage message)
        {
            float f1;
            float f2;
            if (message.TryGet(0, out f1) && message.TryGet(1, out f2))
            {
                PosXY.x = map(f1, 0, 1, -6f, 6f); 
                PosXY.y = map(f2, 0, 1, -6f, 6f);
            }
            OscPool.Recycle(message);        
        }

        void EventHauteurZ( float value )
		{
            PosZ = map(value, 0, 1, -2.5f, 5);
		}

        void EventFOV(float value)
        {           
           // FOV_Ortho = map(value,0,1,0.2f,4);
            FOV_Pers = map(value, 0, 1, 20, 100);
           // Camera CamOrtho = CameraOrtho.GetComponent<Camera>();
         //   CamOrtho.fieldOfView = FOV_Ortho;
            Camera CamPers = CameraPers.GetComponent<Camera>();
            CamPers.fieldOfView = FOV_Pers;
            Camera CamPersTop = CameraPersTop.GetComponent<Camera>();
            CamPersTop.fieldOfView = FOV_Pers;
        }

        void EventCamRotation(float value)
        {
            CamRotationValue = map(value, 0, 1, 0, 360 );
            CameraPers.transform.localRotation = Quaternion.Euler(0, CamRotationValue , 0);
            CameraPersTop.transform.localRotation = Quaternion.Euler(0, CamRotationValue, 0);
        }

        void EventFXRotation(float value)
        {
            FXRotationValue = map(value, 0, 1, 0, 360);
            CenterRotation.transform.localRotation = Quaternion.Euler(0, FXRotationValue, 0);
        }

        void EventAnim1(float value)
        {
            if(value == 1){
                Animator AC_CamMovement = CenterRotation.GetComponent<Animator>();
                AC.SetTrigger("RotationHorizontal");                  
            }
        }

        void EventAnim2(float value)
        {
            if (value == 1){
                Animator AC_CamMovement = CenterRotation.GetComponent<Animator>();
                AC.SetTrigger("RotationVertical");
            }
        }

        void EventChangeCam(int value)
        {
                if (value == 0) {
                    CameraPersTop.SetActive(false);
                    CameraPers.SetActive(true);                 
                } else if (value == 1) {
                    CameraPersTop.SetActive(true);
                    CameraPers.SetActive(false);
                } else if (value == 2) {
                } else if (value == 3) {
                } else if (value == 4) {
                }
        }

        void EventFXParticles(float value)
        {
            VisualEffect VFX = FX.GetComponent<VisualEffect>();
            VFX.SetFloat("Launch", value);
        }

        void EventFX1(float value)
        {
            VisualEffect VFX = FX.GetComponent<VisualEffect>();
            VFX.SetFloat("NbrTrail", value);
        }

        void EventFX2(float value)
        {
            VisualEffect VFX = FX.GetComponent<VisualEffect>();
            VFX.SetFloat("LifeTimeMax", value);
        }

        void EventFX3(float value)
        {
            VisualEffect VFX = FX.GetComponent<VisualEffect>();
            VFX.SetFloat("BlurPosition", value);
        }

        void EventFX4(float value)
        {
            VisualEffect VFX = FX.GetComponent<VisualEffect>();
            VFX.SetFloat("ExtendTrail", value);
        }

        void EventFX5(float value)
        {
            VisualEffect VFX = FX.GetComponent<VisualEffect>();
            VFX.SetFloat("Turbulence", value);
        }

        /// ---- Debug

        void EventKBposX(float value)
        {
            VisualEffect VFX = FX.GetComponent<VisualEffect>();
            VFX.SetFloat("KB_PosX", value);
        }

        void EventKBposY(float value)
        {
            VisualEffect VFX = FX.GetComponent<VisualEffect>();
            VFX.SetFloat("KB_PosY", value);
        }

        void EventKBradius(float value)
        {
            VisualEffect VFX = FX.GetComponent<VisualEffect>();
            VFX.SetFloat("KB_Radius", value);
        }

        void EventPointcloudPosXY(OscMessage message)
        {
            float f1;
            float f2;
            if (message.TryGet(0, out f1) && message.TryGet(1, out f2))
            {
                VisualEffect VFX = FX.GetComponent<VisualEffect>();
                VFX.SetFloat("KB_PosX", f1);
                VFX.SetFloat("KB_PosY", f2);
              //  PosXY.y = map(f2, 0, 1, -6f, 6f);
            }
            OscPool.Recycle(message);
        }

        void EventDebug1(float value)
        {
            VisualEffect VFX = FX.GetComponent<VisualEffect>();
            VFX.SetFloat("MaxParticles", value);
        }

        void EventDebug2(float value)
        {
            VisualEffect VFX = FX.GetComponent<VisualEffect>();
            VFX.SetFloat("ColorTwick", value);
        }

        /*    void EventFX6(float value)
            {
                VisualEffect VFX = FX.GetComponent<VisualEffect>();
                VFX.SetFloat("BlendSpeed", value);
            }*/

        /* 
          void EventAnimHorizontal(float value)
          {
                  Animator AC_CamMovement = CenterRotation.GetComponent<Animator>();
                  AC_CamMovement.SetTrigger("RotationHorizontal");
                  Debug.Log("Radio 1 OK");
          }
          void EventAnimVertical(float value)
          {
                   Animator AC_CamMovement = CenterRotation.GetComponent<Animator>();
                   AC_CamMovement.SetTrigger("RotationVertical");
              Debug.Log("Radio 2 OK");
          }
          */

    }
}