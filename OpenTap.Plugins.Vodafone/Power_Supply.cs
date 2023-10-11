using OpenTap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

//Note this template assumes that you have a SCPI based instrument, and accordingly
//extends the ScpiInstrument base class.

//If you do NOT have a SCPI based instrument, you should modify this instance to extend
//the (less powerful) Instrument base class.

namespace OpenTap.Plugins.Vodafone
{
    [Display("Power_Supply", Group: "Power Supply Control", Description: "Controls Power Supply")]
    public class Power_Supply : ScpiInstrument
    {
        #region Settings
        // ToDo: Add property here for each parameter the end user should be able to change
        #endregion

        public Power_Supply()
        {
            Name = "Power Supply";
            // ToDo: Set default values for properties / settings.
        }

        /// <summary>
        /// Open procedure for the instrument.
        /// </summary>
        public override void Open()
        {

            base.Open();

            
            // TODO:  Open the connection to the instrument here

            //if (!IdnString.Contains("Instrument ID"))
            //{
            //    Log.Error("This instrument driver does not support the connected instrument.");
            //    throw new ArgumentException("Wrong instrument type.");
            // }

        }

        internal string Px_Volt()
        {
            ScpiCommand("INIT");
            string Px_Supply_Volt;
            Px_Supply_Volt = ScpiQuery(":FETCH?");
            return Px_Supply_Volt;
        }

        internal string Px_Current()
        {
            ScpiCommand("INIT");
            string Px_Supply_Current;
            Px_Supply_Current = ScpiQuery(":FETCH?");
            return Px_Supply_Current;
        }

        /// <summary>
        /// Close procedure for the instrument.
        /// </summary>
        public override void Close()
        {
            // TODO:  Shut down the connection to the instrument here.
            base.Close();
        }
    }
}
