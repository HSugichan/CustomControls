using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace CustomControls
{
    /// <summary>
    /// Wrapped Microsoft.VisualBasic.Interaction
    /// </summary>
    public static class InputBox
    {
        /// <summary>
        /// Microsoft.VisualBasic.Interaction.InputBox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="defaultMessage"></param>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <returns></returns>
        public static string Show(string text, string caption, string defaultMessage = "", int xPos = -1, int yPos = -1)
            => Interaction.InputBox(text, caption, defaultMessage, xPos, yPos);
    }
}
