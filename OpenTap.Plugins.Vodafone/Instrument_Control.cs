using OpenTap;   // Use OpenTAP infrastructure/core components (log,TestStep definition, etc)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTap.Plugins.Vodafone
{
    [Display("5G NR Test System", Group: "Instrument Control", Description: "Controls 5G NR Test System")]
    public class Instrument_Control : TestStep
    {
        #region Settings

        [Display("Signal Analyser VISA Resource", Group: "Signal Analyser Control", Description: "Sets the SA VISA String")]
        public Sig_Ana Sig_Ana_SCPI { get; set; }

        [Display("Power Supply VISA Resource", Group: "Power Supply Control", Description: "Sets the PS VISA String")]
        public Power_Supply PX_Supply_SCPI { get; set; }

        #endregion

        public Instrument_Control()
        {

        }

        public override void Run()
        {
            // ToDo: Add test case code.
            //Sig_Ana_SCPI.Set_Up();
            string CH_Power_Table_CSV = Sig_Ana_SCPI.Power();
            Log.Info("Channel Power = " + CH_Power_Table_CSV);


            string Px_Volt = PX_Supply_SCPI.Px_Volt();
            Log.Info("Power Supply Voltage = " + Px_Volt);

            string Px_Current = PX_Supply_SCPI.Px_Current();
            Log.Info("Power Supply Current = " + Px_Current);

            double[] CH_Power_Table_Array= CH_Power_Table_CSV.Split(',').Select(Double.Parse).ToArray();
            
            double pre_dpd_input_power = CH_Power_Table_Array[1];

            double pre_dpd_output_power = CH_Power_Table_Array[2];

            double pre_dpd_gain = CH_Power_Table_Array[3];

            double post_dpd_input_power = CH_Power_Table_Array[9];

            double post_dpd_output_power = CH_Power_Table_Array[10];

            double post_dpd_gain = CH_Power_Table_Array[11];

            Log.Info("Pre-DPD Input Power = " + pre_dpd_input_power);

            Log.Info("Pre-DPD Output Power = " + pre_dpd_output_power);

            Log.Info("Pre-DPD Gain = " + pre_dpd_gain);

            Log.Info("Post-DPD Input Power = " + post_dpd_input_power);

            Log.Info("Post-DPD Output Power = " + post_dpd_output_power);

            Log.Info("Post-DPD Gain = " + post_dpd_gain);

            //double CH_Power_D = double.Parse(CH_Power);

            //double RF_Power_D = double.Parse(RF_Power);

            //double Px_Volt_D = double.Parse(Px_Volt);

            //double Px_Current_D = double.Parse(Px_Current);

            //double RF_Gain = CH_Power_D / RF_Power_D;

            //double Pdc = Px_Volt_D * Px_Current_D;

            //Log.Info("RF Gain = " + RF_Gain);

            //double PAE = (CH_Power_D - RF_Power_D) / Pdc;

            //Log.Info("Power Amplifier Efficiency = " + PAE);

            RunChildSteps(); //If the step supports child steps.

           
        }
    }
}