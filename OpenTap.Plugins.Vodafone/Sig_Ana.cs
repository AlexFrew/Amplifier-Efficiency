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
    [Display("Sig_Ana", Group: "Instrument Control", Description: "Insert a description here")]
    public class Sig_Ana : ScpiInstrument
    {
        #region Settings
        // ToDo: Add property here for each parameter the end user should be able to change
        #endregion

        public Sig_Ana()
        {
            Name = "Signal Analyser";
            // ToDo: Set default values for properties / settings.
        }

        internal string Power()
        {
            ScpiCommand(":INITiate:RESTart");
            TapThread.Sleep(7000);
            string CH_Power;
            CH_Power = ScpiQuery(":FETCh:PAMPlifier?");
            return CH_Power;
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

        internal void DPD_State_Off()
        {
            ScpiCommand(":SENSe:PAMPlifier:DPD:ENABle 0");
        }

        internal void DPD_State_On()
        {
            ScpiCommand(":SENSe:PAMPlifier:DPD:ENABle 1");
        }

        /// <summary>
        /// Close procedure for the instrument.
        /// </summary>
        public override void Close()
        {
            // TODO:  Shut down the connection to the instrument here.
            base.Close();
        }

        internal void Set_Up()
        {
            ScpiCommand(":INST:CONF:NR5G:MONitor");
            ScpiCommand(":INST:CONF:NR5G:CHPower");
        }
    }
}
