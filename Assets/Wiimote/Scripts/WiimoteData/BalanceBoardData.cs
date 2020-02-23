using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WiimoteApi {
  public class BalanceBoardData : WiimoteData
  {

    public float TopRight { get { return _TopRight; } }
    private float _TopRight = 0; //TODO default value??

    private int _TopRightRaw = 0;



    public float TopLeft { get { return _TopLeft; } }
    private float _TopLeft = 0;

    private int _TopLeftRaw = 0;


    public float BottomRight { get { return _BottomRight; } }
    private float _BottomRight = 0;

    private int _BottomRightRaw = 0;


    public float BottomLeft { get { return _BottomLeft; } }
    private float _BottomLeft = 0;

    private int _BottomLeftRaw = 0;

    public float BatteryPercent { get { return _BatteryPercent; }}
    private float _BatteryPercent = 0;


    //Local Parameters
    private byte CONTINUOUS_REPORTING    = 0x04;
    // private byte COMMAND_LIGHT           = 0x11;
    // private byte COMMAND_REPORTING       = 0x12;
    // private byte COMMAND_REQUEST_STATUS  = 0x15;
    // private byte COMMAND_REGISTER        = 0x16;
    // private byte COMMAND_READ_REGISTER   = 0x17;
    private byte INPUT_STATUS            = 0x20;
    private byte INPUT_READ_DATA         = 0x21;
    private byte EXTENSION_8BYTES        = 0x32;
    private byte BUTTON_DOWN_MASK        = 0x08;
    private byte LED1_MASK               = 0x10;
    private float BATTERY_MAX             = 200.0F;
    private int TOP_RIGHT               = 0;
    private int BOTTOM_RIGHT            = 1;
    private int TOP_LEFT                = 2;
    private int BOTTOM_LEFT             = 3;

    //Calibration values hard coded for 1st board
    private float[,] calibration = {{4045, 19570, 19959, 7701},
                                    {5844, 21355, 21761, 9449},
                                    {7646, 23147, 23566, 11201}};

    // public bool calibrationRequested = false;

    //Lambda to convert byte to int:
    // Func<byte, int> b2i = b => (int)b; //might need to go from string to bytes?

    public BalanceBoardData(Wiimote Owner):base(Owner)
    {
      //Initialize what needs to be initialized
    }

    public override bool InterpretData(byte[] data)
    {
      if (data == null || data.Length < 2)
      {
        Debug.Log("No data?");
        return false;
      }

      byte inputType = data[1];

      //FUCK THIS I hardcoded the calibration values
      // if (inputType == INPUT_STATUS){
      //   //TODO blink light?
      //   _BatteryPercent = b2i(data[7:9]) / BATTERY_MAX;
      // }
      // else if (inputType == INPUT_READ_DATA){
      //   Debug.log("Got calibration data");
      //   if (calibrationRequested){
      //       int length = b2i(data[4]) / 16 + 1;
      //       data = data[7:7 + length];
      //       //TODO get int of bytes d[j:j+2] for j in [0, 2, 4, 6]
      //       Func<byte[], int[]> cal
      //       if (length == 16){
      //         calibration = {cal(data[0:8]), cal(data[8:16]), 1e4, 1e4, 1e4, 1e4};
      //       }
      //       else if (length < 16){
      //         calibration[2] = cal(data[0:8]);
      //         calibrationRequested = false;
      //
      //       }
      //   }
      // }
      if (inputType == EXTENSION_8BYTES){
        //Get mass:
        // data = data[4..11];
        GetMass(data); //inclusive? only want 4 to 11?
      }


      return true;
    }

    private void GetMass(byte[] data){

      // _TopRightRaw = b2i(data[0:2]); //TODO how to get 2 bytes from byte array?

      _TopRightRaw = (int)data[0] << 8; //get 1st byte, shift over
      _TopRightRaw |= data[1]; //get 2nd byte in empty space
      _BottomRightRaw = (int)data[2] << 8;
      _BottomRightRaw |= data[3];
      _TopLeftRaw = (int)data[4] << 8;
      _TopLeftRaw |= data[5];
      _BottomLeftRaw = (int)data[6] << 8;
      _BottomLeftRaw |= data[7];
      //
      // _BatteryRaw = ;

      //TODO: normalize data by total weight first?
      _TopRight = CalcMass(_TopRightRaw, TOP_RIGHT);
      _BottomRight = CalcMass(_BottomRightRaw, BOTTOM_RIGHT);
      _TopLeft = CalcMass(_TopLeftRaw, TOP_LEFT);
      _BottomLeft = CalcMass(_BottomLeftRaw, BOTTOM_LEFT);

    }

    private float CalcMass(int raw, int pos)
    {
      // # Calculates the Kilogram weight reading from raw data at position pos
      //     # calibration[0] is calibration values for 0kg
      //     # calibration[1] is calibration values for 17kg
      //     # calibration[2] is calibration values for 34kg

      //If raw less than 0 calib value, return 0
      //Otherwise, spline:
      if (raw < calibration[0,pos]){
        return 0.0F;
      }
      else if (raw < calibration[1,pos]){
        return 17 * ((raw - calibration[0,pos]) /
                      (float)(calibration[1,pos]
                      - calibration[0,pos]));
      }
      else {
        return 17 + 17 * ((raw - calibration[1,pos]) /
                          (float)(calibration[2,pos]
                          - calibration[1,pos]));
      }

    }


  }

}
