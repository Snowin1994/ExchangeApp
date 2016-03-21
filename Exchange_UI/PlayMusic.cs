using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exchange_UI
{
    public class PlayMusic
    {
        private SoundPlayer sp;
        
   
        public PlayMusic()
        {
            sp = new SoundPlayer();
        }

        /// <summary>
        /// 循环播放音效
        /// </summary>
        /// <param name="soundLocation">音效路径</param>
        /// <param name="millisecondsEach">单次时间（单位：秒）</param>
        /// <param name="playNum">循环次数</param>
        public void PlayLoopingSound(string soundLocation, int millisecondsEach ,int playNum)
        {
            sp.SoundLocation = soundLocation;
            sp.PlayLooping();
            Thread.Sleep(millisecondsEach * playNum);
            sp.Stop();
        }

        /// <summary>
        /// 单次播放音效
        /// </summary>
        /// <param name="soundLocation">音效路径</param>
        /// <param name="millisecondsEach">单位时间（单位：秒）</param>
        public void PlayEachSound(string soundLocation,int millisecondsEach)
        {
            sp.SoundLocation = soundLocation;
            sp.Play();
            Thread.Sleep(millisecondsEach);
            sp.Stop();
        }
    }
}
