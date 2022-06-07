using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class VideoCtrl : MonoBehaviour
{

    public VideoPlayer video;
    public ObjectEvent obevent;

    // ���� ��  ��� ���� �Ǵ°��� ���� �ϱ� ����
    public int numberValue;
    // ���� ��
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

        //�Է��� ���ڰ� �ְ�, �� ���ڰ� ������ �޶����ٸ�
        if (numberValue != currentNumber)
        {
            switch (numberValue)
            {
                case 1:  //���
                    video.Play();
                    break;
                case 2:
                    //�Ͻ�����
                    video.Pause();
                    break;
                case 3: // ���� ��
                    // ���� ���ݾ� Ű��� (1���� ���� ����)
                    if (video.GetDirectAudioVolume(0) < 1)
                        video.SetDirectAudioVolume(0, video.GetDirectAudioVolume(0) + 0.1f);
                    break;
                case 4:
                    //���� ���ݾ� ���̱�(0���� Ŭ ����)
                    if (video.GetDirectAudioVolume(0) > 0)
                        video.SetDirectAudioVolume(0, video.GetDirectAudioVolume(0) - 0.1f);
                    break;
            }
        }
        currentNumber = numberValue;
    }
}
