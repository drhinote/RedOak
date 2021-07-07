<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Full.aspx.cs" Inherits="reports_Full" %>

<html>
  <head>
    <title>Red Oak Instruments, LLC.</title>

    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/angular_material/1.1.8/angular-material.min.css">
    <link href="../css/StyleSheet.css" rel="stylesheet" />

    <!-- Angular Material requires Angular.js Libraries -->
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.9/angular.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.9/angular-animate.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.9/angular-aria.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.9/angular-messages.min.js"></script>

    <!-- Angular Material Library -->
    <script src="https://ajax.googleapis.com/ajax/libs/angular_material/1.1.8/angular-material.min.js"></script>  


    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript" src="../script/DoubleGaugeChart.js"></script>
    <script type="text/javascript" src="../script/DoubleGaugeChartRG.js"></script>
    <script type="text/javascript" src="../script/DoubleGaugeChartRGY.js"></script>
    <script type="text/javascript" src="../script/OneSeriesBarChart.js"></script>
    <script type="text/javascript" src="../script/OneSeriesColumnChart.js"></script>
    <script type="text/javascript" src="../script/TwoSeriesBarChart.js"></script>
    <script type="text/javascript" src="../script/TwoSeriesColumnChart.js"></script>
    <script type="text/javascript" src="../script/TwoSeriesColumnChart1.js"></script>
    <script type="text/javascript" src="../script/FullPulseChart.js"></script>
    <script type="text/javascript" src="../script/SingleGaugeChart.js"></script>
    <script type="text/javascript" src="../script/SinglePulseChart.js"></script>
    <script type="text/javascript" src="../script/PulseForceChart.js"></script>
    <script type="text/javascript" src="../script/Angular.js"></script>
    <script type="text/javascript">

        google.charts.load("current", { packages: ['corechart', 'bar', 'line', 'gauge'] });

        google.charts.setOnLoadCallback(drawCharts);

        function drawCharts() {

            // LEFT REACTION
            SingleGaugeChart(
                "left_reaction_guage",
                "L Reaction",
                <%=Data.LeftReactionTime%>
            );

            // INJURY EVIDENCE
            DoubleGaugeChart(
                "injury_evidence_guages",
                <%=Data.LeftInjuryEvidence%>,
                <%=Data.RightInjuryEvidence%>
            );

            // RIGHT REACTION
            SingleGaugeChart(
                "right_reaction_guage",
                "R Reaction",
                <%=Data.RightReactionTime%>
            );

            // INJURY EVIDENCE HISTORY
            TwoSeriesColumnChart1(
                "History (earliest to latest)",
                "test_history_chart",
                <%=Data.TestHistory%>
            );

            // MOTOR COORDINATION
            TwoSeriesColumnChart(
                "Motor Coordination",
                "motor_coordination_chart",
                <%=Data.LeftMotorControl2s%>, <%=Data.LeftMotorControl3s%>,
                <%=Data.RightMotorControl2s%>, <%=Data.RightMotorControl3s%>
            );

            // SENSORY COORDINATION
            TwoSeriesColumnChart(
                "Sensory Coordination",
                "sensory_coordination_chart",
                <%=Data.LeftSensoryControl2s%>, <%=Data.LeftSensoryControl3s%>,
                <%=Data.RightSensoryControl2s%>, <%=Data.RightSensoryControl3s%>
            );


            // RG-REACTION TIME
            DoubleGaugeChartRG(
                "rg_reaction_chart",
                <%=Data.LeftReactionTime%>,
                <%=Data.RightReactionTime%>,
            );

            // RGY-REACTION TIME
            DoubleGaugeChartRGY(
                "rgy_reaction_chart",
                <%=Data.LeftCognitiveReactionTime%>,
                <%=Data.RightCognitiveReactionTime%>,
            );

            // REACTION VARIANCE
            TwoSeriesColumnChart(
                "Reaction Variance",
                "reaction_variance_chart",
                <%=Data.LeftCorrelation2s%>, <%=Data.LeftCorrelation3s%>,
                <%=Data.RightCorrelation2s%>, <%=Data.RightCorrelation3s%>
            );

            // LEFT PULSE 2S
            FullPulseChart(
                "Left Hand - 2 Symbol",
                "left_pulse_chart",
                <%=Data.LeftPulse_2s%>
            );

            // LEFT PULSE 3 2S
            SinglePulseChart(
                "Left Pulse 3",
                "left_pulse_3_chart",
                <%=Data.LeftPulse3_2s%>
            );

            // LEFT PULSE FORCE 3 2S
            PulseForceChart(
                "Left Pulse 3",
                "left_pulse_force_3_chart",
                <%=Data.LeftPulseForce3_2s%>
            );

            // LEFT PULSE 7 2S
            SinglePulseChart(
                "Left Pulse 7",
                "left_pulse_7_chart",
                <%=Data.LeftPulse7_2s%>
            );

            // LEFT PULSE FORCE 7 2S
            PulseForceChart(
                "Left Pulse 7",
                "left_pulse_force_7_chart",
                <%=Data.LeftPulseForce7_2s%>
            );

            // RIGHT PULSE 2S
            FullPulseChart(
                "Right Hand - 2 Symbol",
                "right_pulse_chart",
                <%=Data.RightPulse_2s%>
            );

            // RIGHT PULSE 3 2S
            SinglePulseChart(
                "Right Pulse 3",
                "right_pulse_3_chart",
                <%=Data.RightPulse3_2s%>
            );

            // RIGHT PULSE FORCE 3 2S
            PulseForceChart(
                "Right Pulse 3",
                "right_pulse_force_3_chart",
                <%=Data.RightPulseForce3_2s%>
            );

            // RIGHT PULSE 7 2S
            SinglePulseChart(
                "Right Pulse 7",
                "right_pulse_7_chart",
                <%=Data.RightPulse7_2s%>
            );

            // RIGHT PULSE FORCE 7 2S
            PulseForceChart(
                "Right Pulse 7",
                "right_pulse_force_7_chart",
                <%=Data.RightPulseForce7_2s%>
            );

            // LEFT PULSE 3S
            FullPulseChart(
                "Left Hand - 3 Symbol",
                "left_pulse_chart_3s",
                <%=Data.LeftPulse_3s%>
            );

            // LEFT PULSE 3 3S
            SinglePulseChart(
                "Left Pulse 3",
                "left_pulse_3_chart_3s",
                <%=Data.LeftPulse3_3s%>
            );

            // LEFT PULSE FORCE 3 3S
            PulseForceChart(
                "Left Pulse 3",
                "left_pulse_force_3_chart_3s",
                <%=Data.LeftPulseForce3_3s%>
            );

            // LEFT PULSE 7 3S
            SinglePulseChart(
                "Left Pulse 7",
                "left_pulse_7_chart_3s",
                <%=Data.LeftPulse7_3s%>
            );

            // LEFT PULSE FORCE 7 3S
            PulseForceChart(
                "Left Pulse 7",
                "left_pulse_force_7_chart_3s",
                <%=Data.LeftPulseForce7_3s%>
            );

            // RIGHT PULSE 3S
            FullPulseChart(
                "Right Hand - 3 Symbol",
                "right_pulse_chart_3s",
                <%=Data.RightPulse_3s%>
            );

            // RIGHT PULSE 3 3S
            SinglePulseChart(
                "Right Pulse 3",
                "right_pulse_3_chart_3s",
                <%=Data.RightPulse3_3s%>
            );

            // RIGHT PULSE FORCE 3 3S
            PulseForceChart(
                "Right Pulse 3",
                "right_pulse_force_3_chart_3s",
                <%=Data.RightPulseForce3_3s%>
            );

            // RIGHT PULSE 7 3S
            SinglePulseChart(
                "Right Pulse 7",
                "right_pulse_7_chart_3s",
                <%=Data.RightPulse7_3s%>
            );

            // RIGHT PULSE FORCE 7 3S
            PulseForceChart(
                "Right Pulse 7",
                "right_pulse_force_7_chart_3s",
                <%=Data.RightPulseForce7_3s%>
            );
        }
    </script>
  </head>

  <body>

    <div id="header1" runat="server" class="header-style1"></div>
    <div class="header-style2"></div>
    <div class="header-style3"></div>

    <div style="padding-left:20px; padding-top:20px; font-family:'Times New Roman'; ">
        <asp:Table runat="server" style="border-style:groove; padding-bottom:20px; width:700px">
            <asp:TableRow>
                <asp:TableCell style="font-weight:bold">Test ID</asp:TableCell> <asp:TableCell><%=Data.TestId%></asp:TableCell>
                <asp:TableCell style="font-weight:bold">Test Range</asp:TableCell> <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell style="font-weight:bold">Optional ID</asp:TableCell> <asp:TableCell><%=Data.OptionalId%></asp:TableCell>
                <asp:TableCell style="font-weight:bold; font-size:small; padding-left:10px">Start Date / Time</asp:TableCell> <asp:TableCell><%=Data.StartDate%></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell style="font-weight:bold">Gender</asp:TableCell> <asp:TableCell><%=Data.Gender%></asp:TableCell>
                <asp:TableCell style="font-weight:bold; font-size:small; padding-left:10px">Last Date / Time</asp:TableCell> <asp:TableCell><%=Data.TestDate%></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell style="font-weight:bold">Age</asp:TableCell> <asp:TableCell><%=Data.Age%></asp:TableCell>
                <asp:TableCell style="font-weight:bold; font-size:small; padding-left:10px">Total Tests</asp:TableCell> <asp:TableCell><%=Data.TotalTests%></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell style="font-weight:bold">Dominant Hand</asp:TableCell> <asp:TableCell><%=Data.DominantHand%></asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <div style="padding-top:20px; font-family:'Times New Roman';">
            <table>
                <tr>
                    <td>
                        The results of this evaluation are consistent with: 
                    </td>
                    <td>
                        <div style="text-align:center"><asp:Label ID="InjuryBox" runat="server" Width="300px"></asp:Label></div>
                    </td>
                </tr>
                
            </table>
                
            <table style="margin-top:10px; margin-left:45px">
                <tr>
                    <td>
                        <div runat="server" id="left_reaction_guage" style="width:150px; margin-top:35px"></div>
                    </td>
                    <td>
                        <div runat="server" id="injury_evidence_guages" style="width:400px"></div>
                    </td>
                    <td>
                        <div runat="server" id="right_reaction_guage" style="width:150px; margin-top:35px" ></div>
                    </td>
                </tr>
            </table>
                
        </div>
    </div>

    <div style="padding-left:20px; padding-top:10px; font-family:Times New Roman; width:860px">
        <div style="font-size:14px">
            <div><b>L/R Injury Dials</b></div>
            <div>The ROI data collection system measures hand performance as a function of time by monitoring strength, 
                reaction times and coordination of the thumb, index, and small fingers simultaneously. It measures sensory reflexes and fine motor control, 
                which is the coordination of muscles, bones, and nerves to produce small, precise movements (ex:  picking up a pencil with the index finger and thumb).
            </div>
            <div style="padding-top:20px"><b>Why are we testing?:</b></div>
            <div>Problems of the brain, spinal cord, peripheral nerves, muscles, or joints may all decrease fine motor control. 
                Sports injuries, Carpal Tunnel Syndrome, Alzheimer’s, Parkinson’s, car crash rehabilitation, stroke rehabilitation, 
                arthritis, and many injuries/diseases can cause changes in fine motor control and sensory reflexes.  By monitoring 
                coordination and reaction times during your training/rehabilitation, we can document the effectiveness of your training/rehab program 
                and determine if modifications are needed.  Measuring at the start and at intervals during your traingin/rehab program will 
                help us gauge your level of improvement and progress.
            </div>
            <div style="padding-top:20px"><b>How can I improve my scores?</b></div>
            <div>
                By consistently following your training/rehab program, including all treatment recommendations given by your trainer/doctor, you 
                should be able to see improvements in your scores over time.
            </div>
            <div runat="server" id="test_history_chart" style="height: 200px; padding-top:20px"></div>
            <div style="padding-top:20px">
                The results of these biomedical screening tests provide a measure of the functionality capabilities of the test subject based on numerical patterns. The results 
                derived from this screen are intended as an indication of functionality performance only. No medical diagnosis is intended or implied. The lower the score, 
                the greater the probability that the person exibits patterns consistant with normal hand functionality.
            </div>
        </div>  
    </div>

    <div style="padding-top:90px; font-family:'Times New Roman'">
        <div class="footer-style1">RedOak Instruments, LLC</div>
        <div class="footer-style2">(281) 385-9951 - www.redoakinstruments.com - 21218 Kingsland Blvd, Katy, TX 77450</div>
        <div class="footer-style3">support@redoakinstruments.com</div>
    </div>

    <div class="pagebreakafter"></div>

    <div id="FullReport" runat="server" style="font-family:'Times New Roman';">

        <div class="header-style1" runat="server" id="header2"> </div>
        <div class="header-style2"></div>
        <div class="header-style3"></div>

        <div>
            <asp:Table runat="server" style="border-style:groove; padding-bottom:20px; padding-left:20px; padding-top:20px">
                <asp:TableRow>
                    <asp:TableCell style="font-weight:bold">Test ID</asp:TableCell> <asp:TableCell><%=Data.TestId%></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell style="font-weight:bold">Optional ID</asp:TableCell> <asp:TableCell><%=Data.OptionalId%></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell style="font-weight:bold">Gender</asp:TableCell> <asp:TableCell><%=Data.Gender%></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell style="font-weight:bold">Age</asp:TableCell> <asp:TableCell><%=Data.Age%></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell style="font-weight:bold">Dominant Hand</asp:TableCell> <asp:TableCell><%=Data.DominantHand%></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell style="font-weight:bold">Test Date</asp:TableCell> <asp:TableCell><%=Data.TestDate%></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>

        <div style="padding-left:20px">

        <div ng-controller="AppCtrl as appCtrl" ng-cloak="" class="gridListdemoBasicUsage" ng-app="ROI" style="width:880px; height:280px">
            <md-grid-list md-cols-xs="8" md-cols-sm="8" md-cols-md="8" md-cols-gt-md="8" md-row-height-gt-md="4:1" md-row-height="4:1" md-gutter="8px" md-gutter-gt-sm="8px">

                <md-grid-tile md-rowspan="2" md-colspan="2" md-colspan-sm="2" md-colspan-xs="2"></md-grid-tile>
                <md-grid-tile md-colspan=2><h3><%=Data.TestDate%></h3></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.TestDateHist1%>'" md-colspan="2"><h3><%=Data.TestDateHist1%></h3></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.TestDateHist2%>'" md-colspan="2"><h3><%=Data.TestDateHist2%></h3></md-grid-tile>

                <md-grid-tile class="roiLightBlue" md-rowspan="1"><h4>Left</h4></md-grid-tile>
                <md-grid-tile class="roiLightBlue" md-rowspan="1"><h4>Right</h4></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.TestDateHist1%>'" class="roiLightBlue" md-rowspan="1"><h4>Left</h4></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.TestDateHist1%>'" class="roiLightBlue" md-rowspan="1"><h4>Right</h4></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.TestDateHist2%>'" class="roiLightBlue" md-rowspan="1"><h4>Left</h4></md-grid-tile>
                <md-grid-tile ng-show="'<%=Data.TestDateHist2%>'" class="roiLightBlue" md-rowspan="1"><h4>Right</h4></md-grid-tile>

                <md-grid-tile class="roiLightBlue" md-rowspan="1" md-colspan="2"><h4>Fatigue Varience</h4></md-grid-tile>
                <md-grid-tile md-rowspan="1"><%=Data.LeftFatigueVariance%></md-grid-tile>
                <md-grid-tile md-rowspan="1"><%=Data.RightFatigueVariance%></md-grid-tile>
                <md-grid-tile ng-show="'<%=(Data.LeftHist1.FatigueVarience != 0) ? "true" : ""%>'" md-rowspan="1"><%=Data.LeftHist1.FatigueVarience%></md-grid-tile>
                <md-grid-tile ng-show="'<%=(Data.RightHist1.FatigueVarience != 0) ? "true" : ""%>'" md-rowspan="1"><%=Data.RightHist1.FatigueVarience%></md-grid-tile>
                <md-grid-tile ng-show="'<%=(Data.LeftHist2.FatigueVarience != 0) ? "true" : ""%>'" md-rowspan="1"><%=Data.LeftHist2.FatigueVarience%></md-grid-tile>
                <md-grid-tile ng-show="'<%=(Data.RightHist2.FatigueVarience != 0) ? "true" : ""%>'" md-rowspan="1"><%=Data.RightHist2.FatigueVarience%></md-grid-tile>

                <md-grid-tile class="roiLightBlue" md-rowspan="1" md-colspan="2"><h4>Strength</h4></md-grid-tile>
                <md-grid-tile md-rowspan="1"><%=Data.LeftStrength%></md-grid-tile>
                <md-grid-tile md-rowspan="1"><%=Data.RightStrength%></md-grid-tile>
                <md-grid-tile ng-show="'<%=(Data.LeftHist1.Strength != 0) ? "true" : ""%>'" md-rowspan="1"><%=Data.LeftHist1.Strength%></md-grid-tile>
                <md-grid-tile ng-show="'<%=(Data.RightHist1.Strength != 0) ? "true" : ""%>'" md-rowspan="1"><%=Data.RightHist1.Strength%></md-grid-tile>
                <md-grid-tile ng-show="'<%=(Data.LeftHist2.Strength != 0) ? "true" : ""%>'" md-rowspan="1"><%=Data.LeftHist2.Strength%></md-grid-tile>
                <md-grid-tile ng-show="'<%=(Data.RightHist2.Strength != 0) ? "true" : ""%>'" md-rowspan="1"><%=Data.RightHist2.Strength%></md-grid-tile>

                <md-grid-tile class="roiLightBlue" md-rowspan="1" md-colspan="2"><h4>Strength Ratio</h4></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftStrengthRatio%>" md-rowspan="1"><%=Data.LeftStrengthRatio%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightStrengthRatio%>" md-rowspan="1"><%=Data.RightStrengthRatio%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist1.StrengthRatio%>" md-rowspan="1"><%=Data.LeftHist1.StrengthRatio%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist1.StrengthRatio%>" md-rowspan="1"><%=Data.RightHist1.StrengthRatio%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist2.StrengthRatio%>" md-rowspan="1"><%=Data.LeftHist2.StrengthRatio%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist2.StrengthRatio%>" md-rowspan="1"><%=Data.RightHist2.StrengthRatio%></md-grid-tile>

                <md-grid-tile class="roiLightBlue" md-rowspan="1" md-colspan="2"><h4>Motor Control</h4></md-grid-tile>
                <md-grid-tile ng-style="{'background':'<%=PercentColor(Data.LeftMotorControl)%>'}" md-rowspan="1"><%=Data.LeftMotorControl.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-style="{'background':'<%=PercentColor(Data.RightMotorControl)%>'}" md-rowspan="1"><%=Data.RightMotorControl.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist1.MotorControl.Value%>" ng-style="{'background':'<%=PercentColor(Data.LeftHist1.MotorControl.Value)%>'}" md-rowspan="1"><%=Data.LeftHist1.MotorControl.Value.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist1.MotorControl.Value%>" ng-style="{'background':'<%=PercentColor(Data.RightHist1.MotorControl.Value)%>'}" md-rowspan="1"><%=Data.RightHist1.MotorControl.Value.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist2.MotorControl.Value%>" ng-style="{'background':'<%=PercentColor(Data.LeftHist2.MotorControl.Value)%>'}" md-rowspan="1"><%=Data.LeftHist2.MotorControl.Value.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist2.MotorControl.Value%>" ng-style="{'background':'<%=PercentColor(Data.RightHist2.MotorControl.Value)%>'}" md-rowspan="1"><%=Data.RightHist2.MotorControl.Value.ToString("p0")%></md-grid-tile>

                <md-grid-tile class="roiLightBlue" md-rowspan="1" md-colspan="2"><h4>Sensory Control</h4></md-grid-tile>
                <md-grid-tile ng-style="{'background':'<%=PercentColor(Data.LeftSensoryControl)%>'}" md-rowspan="1"><%=Data.LeftSensoryControl.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-style="{'background':'<%=PercentColor(Data.RightSensoryControl)%>'}" md-rowspan="1"><%=Data.RightSensoryControl.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist1.SensoryControl.Value%>" ng-style="{'background':'<%=PercentColor(Data.LeftHist1.SensoryControl.Value)%>'}" md-rowspan="1"><%=Data.LeftHist1.SensoryControl.Value.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist1.SensoryControl.Value%>" ng-style="{'background':'<%=PercentColor(Data.RightHist1.SensoryControl.Value)%>'}" md-rowspan="1"><%=Data.RightHist1.SensoryControl.Value.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist2.SensoryControl.Value%>" ng-style="{'background':'<%=PercentColor(Data.LeftHist2.SensoryControl.Value)%>'}" md-rowspan="1"><%=Data.LeftHist2.SensoryControl.Value.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist2.SensoryControl.Value%>" ng-style="{'background':'<%=PercentColor(Data.RightHist2.SensoryControl.Value)%>'}" md-rowspan="1"><%=Data.RightHist2.SensoryControl.Value.ToString("p0")%></md-grid-tile>

                <md-grid-tile class="roiLightBlue" md-rowspan="1" md-colspan="2"><h4>Reaction Time</h4></md-grid-tile>
                <md-grid-tile md-rowspan="1"><%=Data.LeftReactionTime%></md-grid-tile>
                <md-grid-tile md-rowspan="1"><%=Data.RightReactionTime%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist1.ReactionTime%>" md-rowspan="1"><%=Data.LeftHist1.ReactionTime%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist1.ReactionTime%>" md-rowspan="1"><%=Data.RightHist1.ReactionTime%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist2.ReactionTime%>" md-rowspan="1"><%=Data.LeftHist2.ReactionTime%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist2.ReactionTime%>" md-rowspan="1"><%=Data.RightHist2.ReactionTime%></md-grid-tile>

                <md-grid-tile class="roiLightBlue" md-rowspan="1" md-colspan="2"><h4>Cognitive Reaction Time</h4></md-grid-tile>
                <md-grid-tile md-rowspan="1"><%=Data.LeftCognitiveReactionTime%></md-grid-tile>
                <md-grid-tile md-rowspan="1"><%=Data.RightCognitiveReactionTime%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist1.CognitiveReactionTime%>" md-rowspan="1"><%=Data.LeftHist1.CognitiveReactionTime%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist1.CognitiveReactionTime%>" md-rowspan="1"><%=Data.RightHist1.CognitiveReactionTime%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist2.CognitiveReactionTime%>" md-rowspan="1"><%=Data.LeftHist2.CognitiveReactionTime%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist2.CognitiveReactionTime%>" md-rowspan="1"><%=Data.RightHist2.CognitiveReactionTime%></md-grid-tile>

                <md-grid-tile class="roiLightBlue" md-rowspan="1" md-colspan="2"><h4>Correlation</h4></md-grid-tile>
                <md-grid-tile ng-style="{'background':'<%=PercentColor(Data.LeftCorrelation)%>'}" md-rowspan="1"><%=Data.LeftCorrelation.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-style="{'background':'<%=PercentColor(Data.RightCorrelation)%>'}" md-rowspan="1"><%=Data.RightCorrelation.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist1.Correlation.Value%>" ng-style="{'background':'<%=PercentColor(Data.LeftHist1.Correlation.Value)%>'}" md-rowspan="1"><%=Data.LeftHist1.Correlation.Value.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist1.Correlation.Value%>" ng-style="{'background':'<%=PercentColor(Data.RightHist1.Correlation.Value)%>'}" md-rowspan="1"><%=Data.RightHist1.Correlation.Value.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.LeftHist2.Correlation.Value%>" ng-style="{'background':'<%=PercentColor(Data.LeftHist2.Correlation.Value)%>'}" md-rowspan="1"><%=Data.LeftHist2.Correlation.Value.ToString("p0")%></md-grid-tile>
                <md-grid-tile ng-show="<%=Data.RightHist2.Correlation.Value%>" ng-style="{'background':'<%=PercentColor(Data.RightHist2.Correlation.Value)%>'}" md-rowspan="1"><%=Data.RightHist2.Correlation.Value.ToString("p0")%></md-grid-tile>

            </md-grid-list>
        </div>

        <div style="font-size:14px">
            <div style="padding-top:20px">
                <div><b>Fatigue Variance: </b> Average fatigue differences between the fingers</div>
                <div style="padding-left:40px">Average is defined as 1.00 </div>
                <div style="padding-left:40px">Below 1.0 implies less fatigue differences - good shape…</div>
                <div style="padding-left:40px">Greater than 1.0 implies dominant hand is stronger than non-dominant hand</div>
            </div>
            <div><b>Strength:</b>  Strength is measured in kgf.  Should not be maximal exertion.</div>
            <div>
                <div><b>Strength Ratio:</b>  Value appears under dominant hand.  1.0 implies both hands are equal in strength.</div>
                <div style="padding-left:40px">Below 1.0 implies dominant hand is weaker than the non-dominant hand.</div>
                <div style="padding-left:40px">Greater than 1.0 implies dominant hand is stronger than non-dominant hand</div>
            </div>
            <div><b>Motor Control:</b>  Measures gross motor control factors as a probability the test was normal.  Green = good</div>
            <div><b>Sensory Control:</b>  Measures fine motor control factors as a probability the test was normal.  Green = good</div>
            <div>
                <div><b>Reaction Time:</b>  Average of the three digits reaction times during the tests.  In milliseconds (ms) based on two symbol test.</div>
            </div>
            <div>
                <div><b>Cognitive Reaction Time:</b>  Average of the three digits reaction times during the tests.  In milliseconds (ms) based on three symbol test.</div>
            </div>
            <div>
                <div><b>Correlation:</b>  Measures how well the fingers grip simultaneously, based on reaction times.</div>
                <div style="padding-left:40px">Values are probabilities that the test was normal.  Green = good</div>
            </div>
        </div>

        <div style="width:860px; padding-top:20px">
            The results of these biomedical screening tests provide a measure of the functionality capabilities of the test subject based on numerical patterns. The results 
            derived from this screen are intended as an indication of functionality performance only. No medical diagnosis is intended or implied. The higher the Normal Probability Score, 
            the greater the probability that the person exibits patterns consistant with normal hand functionality.
        </div>

    </div>
        <div style="padding-top:250px">
            <div class="footer-style1">RedOak Instruments, LLC</div>
            <div class="footer-style2">(281) 385-9951 - www.redoakinstruments.com - 21218 Kingsland Blvd, Katy, TX 77450</div>
            <div class="footer-style3">support@redoakinstruments.com</div>
        </div>

        <div class="pagebreakafter"></div>

        <div style="padding-left:20px">
            <div>
                <table>
                    <tr>
                        <td><div style="padding-left:250px"><%=Data.TestId %></div></td>
                        <td><div style="padding-left:200px"><%=Data.TestDate %></div></td>
                    </tr>
                </table>
            </div>
            <asp:table runat="server" Width="900px">
                <asp:TableRow>
                    <asp:TableCell Width="200px">
                        <div>Measured fitness is compared to the "normal", non-injured response. 100% implies patterns are consistent with a normal response.</div>
                    </asp:TableCell>
                    <asp:TableCell>
                        <div id="motor_coordination_chart" style="width: 300px; height: 300px;"></div>
                    </asp:TableCell>
                    <asp:TableCell>
                        <div id="sensory_coordination_chart" style="width: 300px; height: 300px;"></div>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="200px">
                        <div>
                            Measured reaction times are compared to expected averages (the green area: +/- one standard deviation) for the general population.  
                            Results less than the average are faster than normal reaction times.
                        </div>
                    </asp:TableCell>
                    <asp:TableCell>
                        <div style="width: 300px; height: 30px; text-align:center; padding-top:20px">Reaction Time (ms)</div>
                        <div id="rg_reaction_chart" style="width: 300px; height: 170px;"></div>
                    </asp:TableCell>
                    <asp:TableCell>
                        <div style="width: 300px; height: 30px; text-align:center; padding-top:20px">Cognitive Reaction Time (ms)</div>
                        <div id="rgy_reaction_chart" style="width: 300px; height: 170px;"></div>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="200px">
                        <div>
                            The reaction variance compares the interdigital differences between 2 symbol and 3 symbol tests.  
                            100% implies  a normal response.
                        </div>
                    </asp:TableCell>
                    <asp:TableCell>
                        <div id="reaction_variance_chart" style="width: 300px; height: 300px;"></div>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Table runat="server" Width="200px">
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="3"><div style="text-align:center; font-weight:bold; font-size:small">Cognitive Reaction</div></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="3" Height="25px"></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell><div>Left</div></asp:TableCell>
                                <asp:TableCell><div><%=Data.LeftCognitiveReaction%></div></asp:TableCell>
                                <asp:TableCell><div>ms</div></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell><div>Right</div></asp:TableCell>
                                <asp:TableCell><div><%=Data.RightCognitiveReaction%></div></asp:TableCell>
                                <asp:TableCell><div>ms</div></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="3" Height="25px"></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="3">
                                    <div>
                                        The cognitive reaction time is the time it takes to react to the third symbol in the second test.
                                    </div>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3">
                        <div style="padding-right:50px; padding-top:20px">
                            The results of these biomechanical screening tests provide a measure of the functionality capabilities of the test subject based on numerical patterns. 
                            The results derived from this screen are intended as an indication of functionality performance only. 
                            No medical diagnosis is intended or implied. 
                            The higher the Normal Probability score, the greater the probability that the person exhibits patterns consistent with normal hand functionality. 
                        </div>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>

        <div style="padding-top:120px">
            <div class="footer-style1">RedOak Instruments, LLC</div>
            <div class="footer-style2">(281) 385-9951 - www.redoakinstruments.com - 21218 Kingsland Blvd, Katy, TX 77450</div>
            <div class="footer-style3">support@redoakinstruments.com</div>
        </div>

        <div class="pagebreakafter"></div>

        <div style="padding-left:20px">
            <div>
                <asp:Table runat="server" Width="900px">
                    <asp:TableRow Height="50px" VerticalAlign="Bottom" HorizontalAlign="Center">
                        <asp:TableCell ColumnSpan="3">
                            <div style="font-weight:bold; font-size:large;">Head Impact Trauma Screen: Analysis</div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <div style="padding-right:50px">
                                The applied forces exerted by the thumb, index and small fingers are displayed below as a function of the elapsed time during the tests. 
                                The test is asymmetric, requiring the participant to squeeze and hold the sensor for two seconds, then relax for one second. 
                                This is repeated for 60 seconds, collecting 20 pulses for each hand, twice. 
                                Each finger/thumb is measured simultaneously using independent load cells.  
                            </div>
                            <div style="padding-top:20px; padding-bottom:20px">
                                <div>
                                    The first graph represents the data expected for a normal functioning hand. 
                                    Some of the traits which are tested during the analysis are:
                                </div>
                                <div style="margin-left:15px">
                                    Is the baseline steady?
                                </div>
                                <div style="margin-left:15px">
                                    Does the applied force show a uniform fatigue rate?  (poor exertion by the subject results in a flat decay rate)
                                </div>
                                <div style="margin-left:15px">
                                    Are the pulse shapes uniform?
                                </div>
                                <div style="margin-left:15px">
                                    Are the pulse onset times uniform?
                                </div>
                            </div>

                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <div><asp:Image runat="server" Width="700px" ImageUrl="~/images/SampleCharts1.png" /></div>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>
        <div style="padding-top:300px">
            <div class="footer-style1">RedOak Instruments, LLC</div>
            <div class="footer-style2">(281) 385-9951 - www.redoakinstruments.com - 21218 Kingsland Blvd, Katy, TX 77450</div>
            <div class="footer-style3">support@redoakinstruments.com</div>
        </div>

        <div class="pagebreakafter"></div>

        <div>
            <div>
                <div>
                    <table>
                        <tr>
                            <td><div style="padding-left:120px"><%=Data.TestId %></div></td>
                            <td><div style="padding-left:270px"><%=Data.TestDate %></div></td>
                        </tr>
                    </table>
                </div>
                <asp:Table runat="server" Width="920px" >
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <div id="left_pulse_chart" style="width: 880px; height: 300px;"></div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <table>
                                <tr>
                                    <td>
                                        <div id="left_pulse_3_chart" style="width: 345px; height: 300px;"></div>
                                    </td>
                                    <td>
                                        <div id="left_pulse_force_3_chart" style="width: 525px; height: 300px;"></div>
                                    </td>
                                </tr>
                            </table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <table>
                                <tr>
                                    <td>
                                        <div id="left_pulse_7_chart" style="width: 345px; height: 300px;"></div>
                                    </td>
                                    <td>
                                        <div id="left_pulse_force_7_chart" style="width: 525px; height: 300px;"></div>
                                    </td>
                                </tr>
                            </table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>
        <div style="padding-top:180px">
            <div class="footer-style1">RedOak Instruments, LLC</div>
            <div class="footer-style2">(281) 385-9951 - www.redoakinstruments.com - 21218 Kingsland Blvd, Katy, TX 77450</div>
            <div class="footer-style3">support@redoakinstruments.com</div>
        </div>

        <div class="pagebreakafter"></div>

        <div>
            <div>
            <div>
                <table>
                    <tr>
                        <td><div style="padding-left:120px"><%=Data.TestId %></div></td>
                        <td><div style="padding-left:270px"><%=Data.TestDate %></div></td>
                    </tr>
                </table>
            </div>
                <asp:Table runat="server" Width="800px" >
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <div id="right_pulse_chart" style="width: 880px; height: 300px;"></div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <table>
                                <tr>
                                    <td>
                                        <div id="right_pulse_3_chart" style="width: 345px; height: 300px;"></div>
                                    </td>
                                    <td>
                                        <div id="right_pulse_force_3_chart" style="width: 525px; height: 300px;"></div>
                                    </td>
                                </tr>
                            </table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <table>
                                <tr>
                                    <td>
                                        <div id="right_pulse_7_chart" style="width: 345px; height: 300px;"></div>
                                    </td>
                                    <td>
                                        <div id="right_pulse_force_7_chart" style="width: 525px; height: 300px;"></div>
                                    </td>
                                </tr>
                            </table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:table>
            </div>
        </div>
        <div style="padding-top:180px">
            <div class="footer-style1">RedOak Instruments, LLC</div>
            <div class="footer-style2">(281) 385-9951 - www.redoakinstruments.com - 21218 Kingsland Blvd, Katy, TX 77450</div>
            <div class="footer-style3">support@redoakinstruments.com</div>
        </div>

        <div class="pagebreakafter"></div>

        <div>
            <div>
                <table>
                    <tr>
                        <td><div style="padding-left:120px"><%=Data.TestId %></div></td>
                        <td><div style="padding-left:270px"><%=Data.TestDate %></div></td>
                    </tr>
                </table>
            </div>
            <asp:Table runat="server" Width="920px" >
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3">
                        <div id="left_pulse_chart_3s" style="width: 880px; height: 300px;"></div>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3">
                        <table>
                            <tr>
                                <td>
                                    <div id="left_pulse_3_chart_3s" style="width: 345px; height: 300px;"></div>
                                </td>
                                <td>
                                    <div id="left_pulse_force_3_chart_3s" style="width: 525px; height: 300px;"></div>
                                </td>
                            </tr>
                        </table>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3">
                        <table>
                            <tr>
                                <td>
                                    <div id="left_pulse_7_chart_3s" style="width: 345px; height: 300px;"></div>
                                </td>
                                <td>
                                    <div id="left_pulse_force_7_chart_3s" style="width: 525px; height: 300px;"></div>
                                </td>
                            </tr>
                        </table>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <div style="padding-top:180px">
                <div class="footer-style1">RedOak Instruments, LLC</div>
                <div class="footer-style2">(281) 385-9951 - www.redoakinstruments.com - 21218 Kingsland Blvd, Katy, TX 77450</div>
                <div class="footer-style3">support@redoakinstruments.com</div>
            </div>

            <div class="pagebreakafter"></div>

        </div>

        <div>
            <div>
                <table>
                    <tr>
                        <td><div style="padding-left:120px"><%=Data.TestId %></div></td>
                        <td><div style="padding-left:270px"><%=Data.TestDate %></div></td>
                    </tr>
                </table>
            </div>
            <asp:Table runat="server" Width="9200px" >
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3">
                        <div id="right_pulse_chart_3s" style="width: 880px; height: 300px;"></div>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3">
                        <table>
                            <tr>
                                <td>
                                    <div id="right_pulse_3_chart_3s" style="width: 345px; height: 300px;"></div>
                                </td>
                                <td>
                                    <div id="right_pulse_force_3_chart_3s" style="width: 525px; height: 300px;"></div>
                                </td>
                            </tr>
                        </table>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3">
                        <table>
                            <tr>
                                <td>
                                    <div id="right_pulse_7_chart_3s" style="width: 345px; height: 300px;"></div>
                                </td>
                                <td>
                                    <div id="right_pulse_force_7_chart_3s" style="width: 525px; height: 300px;"></div>
                                </td>
                            </tr>
                        </table>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:table>
        </div>

        <div style="padding-top:180px">
            <div class="footer-style1">RedOak Instruments, LLC</div>
            <div class="footer-style2">(281) 385-9951 - www.redoakinstruments.com - 21218 Kingsland Blvd, Katy, TX 77450</div>
            <div class="footer-style3">support@redoakinstruments.com</div>
        </div>
    </div>
  </body>
</html>