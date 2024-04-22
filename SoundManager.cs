using System.Media;
using WMPLib;

namespace CourseworkFifteen
{
    class SoundManager
    {
        WindowsMediaPlayer soundPlayer = new WindowsMediaPlayer();

        public void PlayOneShotAudio(int Sound)
        {
            if(Properties.Settings.Default.AudioOnOff)
            {
                switch (Sound)
                {
                    case 1:
                        soundPlayer.URL = "Audio/Click.WAV";
                        soundPlayer.controls.play();
                        break;
                    case 2:
                        soundPlayer.URL = "Audio/Error.WAV";
                        soundPlayer.controls.play();
                        break;
                    case 3:
                        soundPlayer.URL = "Audio/GameOver.WAV";
                        soundPlayer.controls.play();
                        break;
                    case 4:
                        soundPlayer.URL = "Audio/MusicBG.WAV";
                        soundPlayer.controls.play();
                        break;
                    case 5:
                        soundPlayer.URL = "Audio/N.WAV";
                        soundPlayer.controls.play();
                        break;
                    case 6:
                        soundPlayer.URL = "Audio/Select.WAV";
                        soundPlayer.controls.play();
                        break;
                    case 7:
                        soundPlayer.URL = "Audio/Win.WAV";
                        soundPlayer.controls.play();
                        break;
                }
            }
        }
    }
}
