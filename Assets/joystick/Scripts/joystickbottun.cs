/*======================================================================
Project Name    : joyStickHandMaid
File Name       : joystickbottun.cs
Creation Date   : 2018/07/27

Copyright © 2018- Soranoana. All rights reserved.

Rights owner
Nickname: ソラノアナ(Soranoana)
Twitter : @Inanis_foramen
Github  : https://github.com/BzLOVEman

This source code or any portion thereof must not be
reproduced or used in any manner whatsoever.
======================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystickbottun : MonoBehaviour {

    private bool isPushDown = false;
    private bool isPushUp = false;
    private bool wasPushDown = false;

    /* オブジェクトをタップしたら */
    public void PushDown() {
        //処理
        if (!wasPushDown) {
            PushDownEvent();
        }
    }

    /* オブジェクトをタップし終えたら */
    public void PushUp() {
        //処理
        PushUpEvent();
    }

    /* 押している間 */
    public void PushNow() {
        PushNowEvent();
    }

    private void PushNowEvent() {
        isPushDown=false;
        isPushUp=false;
    }

    /* タップ時のイベント一覧 */
    private void PushDownEvent() {
        isPushDown=true;
        isPushUp=false;
        wasPushDown=true;
    }

    /* タップ終了時のイベント一覧 */
    private void PushUpEvent() {
        isPushDown=false;
        isPushUp=true;
        wasPushDown=false;
    }

    public bool IsPushDown() {
        if (isPushDown) {
            isPushDown=false;
            return true;
        }
        return false;
    }

    public bool IsPushUp() {
        if (isPushUp) {
            isPushUp=false;
            return true;
        }
        return false;
    }
}
