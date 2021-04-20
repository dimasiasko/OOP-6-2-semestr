using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MusicPlayer
{ // клас для розширення стандартних можливестей MediaPlayer
    public static class PlayerExtension
    { // 4 методи для отримання значення повної довжини та теперішньої довжини відтворення
        public static string GetSongPositionString(this MediaPlayer player)
        {
            if (player.NaturalDuration.HasTimeSpan)
                return player.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
            else
                return String.Empty;

        }
        public static string GetNowPositionString(this MediaPlayer player)
        {
            return player.Position.ToString(@"mm\:ss");
        }

        public static double GetSongPosition(this MediaPlayer player)
        {
            if (player.NaturalDuration.HasTimeSpan)
                return Math.Round(player.NaturalDuration.TimeSpan.TotalSeconds, 2);
            else
                return 0;
        }
        public static double GetNowPosition(this MediaPlayer player)
        {
            return Math.Round(player.Position.TotalSeconds, 2);
        }
        // метод почати заново програвання
        public static void Repeat(this MediaPlayer player)
        {
            player.Stop();
            player.Play();
        }
    }
}
