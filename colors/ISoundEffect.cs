using System;
using System.Collections.Generic;
using System.Text;

namespace colors
{
    public interface ISoundEffect
    {
        void HitSound();
        void CorrectSound();
        void IncorrectSound();
        void ResultSound();
    }
}
