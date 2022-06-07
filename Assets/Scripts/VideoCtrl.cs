using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class VideoCtrl : MonoBehaviour
{

    public VideoPlayer video;
    public ObjectEvent obevent;

    // 기준 값  계속 실행 되는것을 방지 하기 위함
    public int numberValue;
    // 이전 값
    int currentNumber;

    private void Update()
    {
        if (obevent.touched&& numberValue!=1)
        {
            numberValue = 1;
            obevent.touched = false;
        }
        else if(obevent.touched && numberValue == 1)
        {
            numberValue = 2;
            obevent.touched = false;
        }

        //입력한 숫자가 있고, 그 숫자가 이전과 달라졌다면
        if (numberValue != currentNumber)
        {
            switch (numberValue)
            {
                case 1:  //재생
                    video.Play();
                    break;
                case 2:
                    //일시정지
                    video.Pause();
                    break;
                case 3: // 볼륨 업
                    // 볼륨 조금씩 키우기 (1보다 작을 때만)
                    if (video.GetDirectAudioVolume(0) < 1)
                        video.SetDirectAudioVolume(0, video.GetDirectAudioVolume(0) + 0.1f);
                    break;
                case 4:
                    //볼륨 조금씩 줄이기(0보다 클 때만)
                    if (video.GetDirectAudioVolume(0) > 0)
                        video.SetDirectAudioVolume(0, video.GetDirectAudioVolume(0) - 0.1f);
                    break;
            }
        }
        currentNumber = numberValue;
    }
}
