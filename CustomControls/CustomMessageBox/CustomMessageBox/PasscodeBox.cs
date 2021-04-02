using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using CustomControls.Properties;

namespace CustomControls
{
    /// <summary>
    /// Input passcode window
    /// </summary>
    public static class PasscodeBox
    {
        /// <summary>
        /// Unlocked debug mode.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="passcode"></param>
        /// <returns></returns>
        public static bool IsUnlockedPasscode(int x, int y, string passcode = "424369")
        {
            if (string.IsNullOrWhiteSpace(passcode))
                return true;

            return Interaction.InputBox(Resources.TxtInputPasscode, Resources.TxtHUnlock, XPos: x, YPos: y) == passcode;
        }
    }
}