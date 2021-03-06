using System.Collections.Generic;
using Figroll.PersonalTrainer.Domain.API;

namespace Figroll.PersonalTrainer.Domain.Beats
{
    public class Sequencer : Metronome, ISequencer
    {
        private readonly Dictionary<int, bool> _pattern = new Dictionary<int, bool>();
        private int _beatPosition;

        public void SetPattern(string pattern)
        {
            // "1+3+"
            // "1234"
            // "+3+"
            // "1+2+3+4+"
            _pattern.Clear();
            _pattern.Add(1, false);
            _pattern.Add(2, false);
            _pattern.Add(3, false);
            _pattern.Add(4, false);
            _pattern.Add(5, false);
            _pattern.Add(6, false);
            _pattern.Add(7, false);
            _pattern.Add(8, false);

            if (pattern.Contains("1"))
            {
                _pattern[1] = true;
            }
            if (pattern.Contains("2"))
            {
                _pattern[3] = true;
            }
            if (pattern.Contains("3"))
            {
                _pattern[5] = true;
            }
            if (pattern.Contains("4"))
            {
                _pattern[7] = true;
            }

            if (pattern.Contains("1+") || pattern.Contains("+2"))
            {
                _pattern[2] = true;
            }
            if (pattern.Contains("2+") || pattern.Contains("+3"))
            {
                _pattern[4] = true;
            }
            if (pattern.Contains("3+") || pattern.Contains("+4"))
            {
                _pattern[6] = true;
            }
            if (pattern.Contains("4+"))
            {
                _pattern[8] = true;
            }
        }

        protected override void DoPlay()
        {
            _beatPosition = 0;
            var millisecondsPerBeat = (int) (1000.0 / (BPM * 2 / 60.0));
            BeatTimer.Change(millisecondsPerBeat, millisecondsPerBeat);
        }

        protected override void OnTick(object state)
        {
            _beatPosition++;

            var lastBeatInBar = _beatPosition == 8;
            var beatShouldPlay = _pattern[_beatPosition];

            if (lastBeatInBar && !beatShouldPlay)
            {
                EndBar();
            }
            else if (lastBeatInBar)
            {
                PlayBeat(true);
            }
            else if (beatShouldPlay)
            {
                PlayBeat(false);
            }

            if (lastBeatInBar)
            {
                _beatPosition = 0;
            }
        }
    }
}