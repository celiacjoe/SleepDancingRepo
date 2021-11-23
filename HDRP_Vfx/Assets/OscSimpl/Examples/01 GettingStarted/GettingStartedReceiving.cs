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
        public string Map_PositionXY ;
        public GameObject CameraOrtho;
        public GameObject CameraPers;
        public GameObject CameraPersTop;
        public Vector2 PosXY;
        
		public string Map_HauteurZ;
        public float PosZ;
        public string Map_EncodRotateCam;
        public float CamRotationValue;

        public string Map_MultiplierCam;
        public float CamMultiplier;

        public string Map_SlideFOV;
        private float FOV_Ortho;
        public float FOV_Pers;

        public string Map_B1;
        public string Map_B2;
        public string Map_B3;

        public string Map_Radio;
        /// ----- FX PART
        /// 
        public GameObject CenterRotation;
        public string Map_EncodRotateFX;
        public float FXRotationValue;
        public GameObject FX;
        public string Map_Particles;
        public int Particles;

        public string Map_Slider1;
        public string Map_Slider2;
        public string Map_Slider3;
        public string Map_Slider4;
        public string Map_Slider5;

        public string Map_B4;
        public string Map_B5;
        public string Map_B6;

        public string Map_Radio2;



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
            CameraOrtho.transform.position = new Vector3(PosXY.x, PosZ, PosXY.y);
            CameraPers.transform.position = new Vector3(PosXY.x, PosZ, PosXY.y);
            CameraPersTop.transform.position = new Vector3(PosXY.x, PosZ, PosXY.y);

            // CameraPers.transform.localRotation = Quaternion.Euler(0, CamRotationValue, 0);
        }

        void OnEnable()
		{          
            _oscIn.Map(Map_PositionXY, EventPosXY);                 // Cam position
            _oscIn.MapFloat(Map_HauteurZ, EventHauteurZ);           // Cam hauteur
            _oscIn.MapFloat(Map_SlideFOV, EventFOV);                // Cam FOV
            _oscIn.MapFloat(Map_EncodRotateCam, EventCamRotation);    // Cam Rotation
            _oscIn.MapFloat(Map_B1, EventB1);                       // Cam Switch
            _oscIn.MapInt(Map_Radio, EventAnim);                    // Anim 

            _oscIn.MapFloat(Map_MultiplierCam, EventMultiplierCam);    // Cam multiplier

            _oscIn.MapFloat(Map_EncodRotateFX, EventFXRotation);     // FX Rotation
            _oscIn.MapFloat(Map_Particles, EventFXParticles);       // FX In Particles
            _oscIn.MapFloat(Map_Slider1, EventFX1);       // FX Param01
            _oscIn.MapFloat(Map_Slider2, EventFX2);       // FX Param02

            //  _oscIn.MapFloat(Map_Radio2, EventAnimVertical);

            //  _oscIn.MapFloat(Map_B1, EventB2);                         // Button 2
            //  _oscIn.MapFloat(Map_B1, EventB3);                         // Button 3
            //  _oscIn.MapFloat(Map_B1, EventB4);                         // Button 4
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
            FOV_Ortho = map(value,0,1,0.2f,4);
            FOV_Pers = map(value, 0, 1, 20, 100);

            Camera CamOrtho = CameraOrtho.GetComponent<Camera>();
            CamOrtho.fieldOfView = FOV_Ortho;
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

        void EventMultiplierCam(float value)
        {
            CamMultiplier = map(value, 0, 1, 0, 1);
            
        }

        void EventFXRotation(float value)
        {
            FXRotationValue = map(value, 0, 1, 0, 360);
            FX.transform.localRotation = Quaternion.Euler(0, FXRotationValue, 0);
        }

        void EventB1(float value)
        {
            if(value == 1)
            {
                if (BCam){                    
                    // CameraOrtho.SetActive(false);
                    CameraPersTop.SetActive(false);
                    CameraPers.SetActive(true);
                    BCam = false;
                }else{                   
                    // CameraOrtho.SetActive(true);
                    CameraPersTop.SetActive(true);
                    CameraPers.SetActive(false);
                    BCam = true;
                }               
            }
        }

        void EventAnim(int value)
        {
                if (value == 0) {

                } else if (value == 1) {
                Animator AC_CamMovement = CenterRotation.GetComponent<Animator>();
                AC_CamMovement.SetTrigger("RotationHorizontal");
                } else if (value == 2) {
                Animator AC_CamMovement = CenterRotation.GetComponent<Animator>();
                AC_CamMovement.SetTrigger("RotationVertical");
                } else if (value == 3) {
                    Debug.Log("Radio 3 OK");
                } else if (value == 4) {
                    Debug.Log("Radio 4 OK");
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
            VFX.SetFloat("FallingSursaut", value);
        }

        void EventFX2(float value)
        {
            VisualEffect VFX = FX.GetComponent<VisualEffect>();
            VFX.SetFloat("BlendSpeed", value);
        }



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