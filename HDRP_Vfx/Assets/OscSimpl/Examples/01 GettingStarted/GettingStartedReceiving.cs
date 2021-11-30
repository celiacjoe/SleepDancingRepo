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
        public GameObject CameraPersTop2;
        public GameObject CameraPersBot3;
        public GameObject CameraPersTop;

        public Transform target;
        //public GameObject CameraOrtho;
        public Animator AC;

        public float Velocity = 0;
        private Vector3 velocity = Vector3.zero;
        private Vector3 velocityPC = Vector3.zero;
        private float velocityFOV = 0.0f;
        private float velocityCamRot = 0.0f;
        public float SmoothTime = 0.3f;
        public float SmoothRot ;
        public float SmoothRotFX ;

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
        public GameObject PC;

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
            CameraPers.SetActive(true);
            CameraPersTop2.SetActive(false);
            CameraPersBot3.SetActive(false);
            CameraPersTop.SetActive(false);


            if ( !_oscIn ) _oscIn = gameObject.AddComponent<OscIn>();
			_oscIn.Open( Nbr_portIn);

        }

        void Update()
        {
            Vector3 targetPositionCam = new Vector3(PosXY.x, PosZ, PosXY.y);
            CameraPers.transform.position = Vector3.SmoothDamp(CameraPers.transform.position, targetPositionCam, ref velocity, SmoothTime);
            CameraPersTop2.transform.position = Vector3.SmoothDamp(CameraPers.transform.position, targetPositionCam, ref velocity, SmoothTime);
            CameraPersBot3.transform.position = Vector3.SmoothDamp(CameraPers.transform.position, targetPositionCam, ref velocity, SmoothTime);
            CameraPersTop.transform.position = Vector3.SmoothDamp(CameraPers.transform.position, targetPositionCam, ref velocity, SmoothTime);
            // CameraPers.transform.localRotation = Quaternion.Euler(0, CamRotationValue, 0);
            Vector3 targetPositionPC = new Vector3(PointCloudPosXY.x, 0, PointCloudPosXY.y);
            PC.transform.position = Vector3.SmoothDamp(PC.transform.position, targetPositionPC, ref velocityPC, SmoothTime);

            Camera CamPers = CameraPers.GetComponent<Camera>();
            float newFOV = Mathf.SmoothDamp(CamPers.fieldOfView, FOV_Pers, ref velocityFOV, SmoothTime);
            CamPers.fieldOfView = newFOV;
            Camera CamPersTop2 = CameraPersTop2.GetComponent<Camera>();
            CamPersTop2.fieldOfView = newFOV;
            Camera CamPersbot3 = CameraPersBot3.GetComponent<Camera>();
            CamPersTop2.fieldOfView = newFOV;
            Camera CamPersTop = CameraPersTop.GetComponent<Camera>();
            CamPersTop.fieldOfView = newFOV;

            //  float newCamRotationValue = Mathf.SmoothDamp(CameraPers.transform.rotation.y, CamRotationValue, ref velocityCamRot, SmoothTime);
            Quaternion Rotationtarget = Quaternion.Euler(0, CamRotationValue, 0);
            CameraPers.transform.rotation = Quaternion.Slerp(CameraPers.transform.rotation, Rotationtarget, Time.deltaTime * SmoothRot);
            Quaternion Rotationtarget2 = Quaternion.Euler(35, CamRotationValue, 0);
            CameraPersTop2.transform.rotation = Quaternion.Slerp(CameraPersTop2.transform.rotation, Rotationtarget2, Time.deltaTime * SmoothRot);
            Quaternion Rotationtarget3 = Quaternion.Euler(-20, CamRotationValue, 0);
            CameraPersBot3.transform.rotation = Quaternion.Slerp(CameraPersBot3.transform.rotation, Rotationtarget3, Time.deltaTime * SmoothRot);
            Quaternion Rotationtarget4 = Quaternion.Euler(90, 0, CamRotationValue);
            CameraPersTop.transform.rotation = Quaternion.Slerp(CameraPersTop.transform.rotation, Rotationtarget4, Time.deltaTime * SmoothRot);

            Quaternion RotationFXtarget = Quaternion.Euler(0, FXRotationValue, 0);
            CenterRotation.transform.rotation = Quaternion.Slerp(CenterRotation.transform.rotation, RotationFXtarget, Time.deltaTime * SmoothRotFX);

        }

        void OnEnable()
		{          
            _oscIn.Map(Map_CamPositionXY, EventPosXY);              // Cam position
            _oscIn.MapFloat(Map_HauteurZ, EventHauteurZ);           // Cam hauteur
            _oscIn.MapFloat(Map_SlideFOV, EventFOV);                // Cam FOV
            _oscIn.MapFloat(Map_EncodRotateCam, EventCamRotation);  // Cam Rotation
            _oscIn.MapFloat(Map_B1, EventAnim1);                    // ChangeAnim1
            _oscIn.MapFloat(Map_B2, EventAnim2);                    // ChangeAnim2
            _oscIn.MapFloat(Map_B3, EventAnim3);                    // ChangeAnim3
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

            _oscIn.Map(Map_PointcloudPosXY,EventPointCloudPosXY);          // debug PC Position

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
            PosZ = map(value, 0, 1, -3.5f, 8);
		}

        void EventFOV(float value)
        {           
           // FOV_Ortho = map(value,0,1,0.2f,4);
            FOV_Pers = map(value, 0, 1, 30, 90);
        }

        void EventCamRotation(float value)
        {
            CamRotationValue = map(value, 0, 1, 0, 360 );
        }

        void EventFXRotation(float value)
        {
            FXRotationValue = map(value, 0, 1, 0, 360);
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

        void EventAnim3(float value)
        {
            if (value == 1)
            {
                Animator AC_CamMovement = CenterRotation.GetComponent<Animator>();
                AC.SetTrigger("travers");
            }
        }

        void EventChangeCam(int value)
        {
             if (value == 0) {
                CameraPersTop.SetActive(true);
                CameraPersBot3.SetActive(false);
                CameraPersTop2.SetActive(false);
                CameraPers.SetActive(false);
            } else if (value == 1) {
                CameraPersTop.SetActive(false);
                CameraPersBot3.SetActive(true);
                CameraPersTop2.SetActive(false);
                CameraPers.SetActive(false);
                } else if (value == 2) {
                CameraPersTop.SetActive(false);
                CameraPersBot3.SetActive(false);
                CameraPersTop2.SetActive(true);
                CameraPers.SetActive(false);
            } else if (value == 3) {
                CameraPersTop.SetActive(false);
                CameraPersBot3.SetActive(false);
                CameraPersTop2.SetActive(false);
                CameraPers.SetActive(true);
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

        void EventPointCloudPosXY(OscMessage message)
        {
            float f1;
            float f2;
            if (message.TryGet(0, out f1) && message.TryGet(1, out f2))
            {
                //  VisualEffect VFX = FX.GetComponent<VisualEffect>();
                PointCloudPosXY.x = map(f1,0,1,-7,4);
                PointCloudPosXY.y = map(f2, 0, 1, -7, 4);
                VisualEffect VFX = FX.GetComponent<VisualEffect>();
              //  VFX.SetFloat("PointCloudPosition", map(f2, 0, 1, -2, 2));
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