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

        [Display("Digital Pre Distortion", Group: "Vector Signal Analyser", Description: "Turns DPD on and Off")]

        public bool DPD { get; set; }


        #endregion

        public Instrument_Control()
        {
            DPD = false;
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            //Sig_Ana_SCPI.Set_Up();

            if (DPD == true)
            {
                Sig_Ana_SCPI.DPD_State_On();
            }

            else

            {
                Sig_Ana_SCPI.DPD_State_Off();
            }

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

            double Px_Volt_D = double.Parse(Px_Volt);

            double Px_Current_D = double.Parse(Px_Current);

            double Pdc = Px_Volt_D * Px_Current_D;

            Log.Info("Power Dc  Pdc = " + Pdc +"Watts");

            double pre_dpd_input_power_watt = Math.Pow(10, pre_dpd_input_power / 10 - 3);

            double pre_dpd_output_power_watt = Math.Pow(10, pre_dpd_output_power / 10 - 3);

            double PAE_Pre_DPD = ((pre_dpd_output_power_watt- pre_dpd_input_power_watt) / Pdc)*100;

            Log.Info("Power Amplifier Efficiency Pre DPD = " + PAE_Pre_DPD + " %");

            if (DPD == true)
            {
                Log.Info("Post-DPD Input Power = " + post_dpd_input_power);

                Log.Info("Post-DPD Output Power = " + post_dpd_output_power);

                Log.Info("Post-DPD Gain = " + post_dpd_gain);

                double post_dpd_input_power_watt = Math.Pow(10, post_dpd_input_power / 10 - 3);

                double post_dpd_output_power_watt = Math.Pow(10, post_dpd_output_power / 10 -3);

                double PAE_Post_DPD = ((post_dpd_output_power_watt - post_dpd_input_power_watt) / Pdc) * 100;

                Log.Info("Power Amplifier Efficiency Post DPD = " + PAE_Post_DPD + " %");

            }

            RunChildSteps(); //If the step supports child steps.

           
        }
    }
}