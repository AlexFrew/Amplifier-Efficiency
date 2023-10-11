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

        [Display("Signal Generator VISA Resource", Group: "Signal Generator Control", Description: "Sets the SG VISA String")]
        public Sig_Gen Sig_Gen_SCPI { get; set; }

        [Display("Power Supply VISA Resource", Group: "Power Supply Control", Description: "Sets the PS VISA String")]
        public Power_Supply PX_Supply_SCPI { get; set; }

        #endregion

        public Instrument_Control()
        {

        }

        public override void Run()
        {
            // ToDo: Add test case code.
            Sig_Ana_SCPI.Set_Up();
            string CH_Power = Sig_Ana_SCPI.Power();
            Log.Info("Channel Power = " + CH_Power);

            string RF_Power = Sig_Gen_SCPI.RF_Power();
            Log.Info("Signal Generator RF Power = " + RF_Power);

            string Px_Volt = PX_Supply_SCPI.Px_Volt();
            Log.Info("Power Supply Voltage = " + Px_Volt);

            string Px_Current = PX_Supply_SCPI.Px_Current();
            Log.Info("Power Supply Current = " + Px_Current);

            double CH_Power_D = double.Parse(CH_Power);

            double RF_Power_D = double.Parse(RF_Power);

            double Px_Volt_D = double.Parse(Px_Volt);

            double Px_Current_D = double.Parse(Px_Current);

            double RF_Gain = CH_Power_D / RF_Power_D;

            double Pdc = Px_Volt_D * Px_Current_D;

            Log.Info("RF Gain = " + RF_Gain);

            double PAE = (CH_Power_D - RF_Power_D) / Pdc;

            Log.Info("Power Amplifier Efficiency = " + PAE);

            RunChildSteps(); //If the step supports child steps.

            // If no verdict is used, the verdict will default to NotSet.
            // You can change the verdict using UpgradeVerdict() as shown below.
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}
